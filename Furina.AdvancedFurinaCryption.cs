using System.Buffers;
using System.Security.Cryptography;
namespace Furina
{
    internal class AdvancedFurinaCryption
    {
        FurinaCryption m_method;
        int m_keylen = 256;
        long m_blocklen = 1048576;
        string m_rawfilepath;
        string m_outputpath;
        byte[]? m_key;
        byte[]? ReadBuffer;
        public AdvancedFurinaCryption(FurinaCryption fc, string rawfilepath, string outputpath)
        {
            m_method = fc;
            m_rawfilepath = rawfilepath;
            m_outputpath = outputpath;
        }
        private byte[] XORCHNK(byte[] chunk, byte[]? keys)
        {
            if(keys == null) throw new ArgumentNullException(nameof(keys));
            int j = 0;
            for (int i = 0; i < chunk.Length; i++)
            {
                chunk[i] = (byte)(chunk[i] ^ keys[j]);
                if (j >= keys.Length - 1)
                {
                    j = 0;
                }
                else
                {
                    j++;
                }
            }
            return chunk;
        }
        public void SetKeyLength(int kl)
        {
            if(m_method == FurinaCryption.Decrypt) throw new Exception("Key length cannot be set in decryption mode.");
            m_keylen = kl;
        }
        public void SetBlockLength(int bl)
        {
            if (m_method == FurinaCryption.Decrypt) throw new Exception("Block length cannot be set in decryption mode.");
            m_blocklen = bl;
        }
        public void SetHeader(byte[] header)
        {
            if (header == null) throw new Exception("Header cannot be null.");
            if (header.Length != 8) throw new Exception("Header length must be 8.");
            Furina = header;
        }
        private byte[] Furina = { 0x46, 0x75, 0x72, 0x69, 0x6e, 0x61, 0x07, 0x21 };
        private byte[] WriteAFCHeader(long rawlen, long blocklen, long keylen, byte[] guid)//blocklen not contains block header
        {
            long blknum = (int)Math.Ceiling((double)rawlen / blocklen);
            long afclen = 64 + ((32 + blocklen + keylen) * blknum);
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Write(Furina);
                memoryStream.Write(BitConverter.GetBytes(afclen));
                memoryStream.Write(BitConverter.GetBytes(rawlen));
                memoryStream.Write(BitConverter.GetBytes(blknum));
                memoryStream.Write(BitConverter.GetBytes(blocklen));
                memoryStream.Write(BitConverter.GetBytes(keylen));
                memoryStream.Write(guid);
                return memoryStream.ToArray();
            }
        }
        private Dictionary<string, long> ReadAFCHeader(Stream file)
        {
            file.Position = 8;
            if (file.Position + 56 > file.Length) throw new ArgumentOutOfRangeException();
            Dictionary<string, long> keyValuePairs = new();
            Span<byte> span = stackalloc byte[8];
            file.Read(span);
            keyValuePairs.Add("afclen", BitConverter.ToInt64(span));
            file.Read(span);
            keyValuePairs.Add("rawlen", BitConverter.ToInt64(span));
            file.Read(span);
            keyValuePairs.Add("blknum", BitConverter.ToInt64(span));
            file.Read(span);
            keyValuePairs.Add("blocklen", BitConverter.ToInt64(span));
            file.Read(span);
            keyValuePairs.Add("keylen", BitConverter.ToInt64(span));
            file.Position = 0;
            return keyValuePairs;
        }
        private byte[] WriteBlockHeader(long padlen, long blkid, long blkmd5)
        {
            using (MemoryStream memoryStream = new())
            {
                memoryStream.Write(Furina);
                memoryStream.Write(BitConverter.GetBytes(padlen));
                memoryStream.Write(BitConverter.GetBytes(blkid));
                memoryStream.Write(BitConverter.GetBytes(blkmd5));
                return memoryStream.ToArray();
            }
        }
        private Dictionary<string, long> ReadBlockHeader(Stream file, long offset)
        {
            long originpos = file.Position = offset;
            if (file.Position + 24 > file.Length) throw new ArgumentOutOfRangeException();
            Dictionary<string, long> keyValuePairs = new();
            file.Position += 8;
            Span<byte> span = stackalloc byte[8];
            file.Read(span);
            keyValuePairs.Add("padlen", BitConverter.ToInt64(span));
            file.Read(span);
            keyValuePairs.Add("blkid", BitConverter.ToInt64(span));
            file.Position = originpos;
            return keyValuePairs;

        }
        public async Task Enc(bool isNoEnc = false)
        {

            if (m_method == FurinaCryption.Decrypt) throw new Exception("Decryption mode cannot perform encryption operation.");
            //calculate
            FileInfo fileInfo = new FileInfo(m_rawfilepath);
            long size = fileInfo.Length;
            int splitcount = (int)Math.Ceiling((double)size / m_blocklen);
            long afclen = 64 + ((32 + m_blocklen + m_keylen) * splitcount);
            //generate offset list
            long[] list = new long[splitcount];
            long a = 64;
            for (int i = 0; i < splitcount; i++)
            {
                list[i] = a;
                a += 32 + m_blocklen + m_keylen;
            }
            Random.Shared.Shuffle(list);
            //write
            Stream fileStream = new FileStream(m_outputpath, FileMode.OpenOrCreate, FileAccess.Write);
            Stream rawfiles = new FileStream(m_rawfilepath, FileMode.Open, FileAccess.Read);
            fileStream.SetLength(afclen);
            await fileStream.FlushAsync();
            await fileStream.WriteAsync(WriteAFCHeader(size, m_blocklen, m_keylen, Guid.NewGuid().ToByteArray()));
            ReadBuffer = ArrayPool<byte>.Shared.Rent((int)m_blocklen);
            for (int i = 0; i < splitcount; i++)
            {

                m_key = isNoEnc ? ArrayPool<byte>.Shared.Rent(m_keylen) : RandomNumberGenerator.GetBytes(m_keylen);
                fileStream.Position = list[i];
                await fileStream.WriteAsync(WriteBlockHeader(0, i, 0));
                //enc
                await fileStream.WriteAsync(m_key);
                await rawfiles.ReadAsync(ReadBuffer);
                await fileStream.WriteAsync(XORCHNK(ReadBuffer, m_key));
                m_key = null;
            }
            ReadBuffer = null;
            rawfiles.Close();
            fileStream.Close();
            m_key = null;
        }
        public async Task Dec()
        {
            if (m_method == FurinaCryption.Encrypt) throw new Exception("Encryption mode cannot perform decryption operation.");
            Stream rawfiles = new FileStream(m_rawfilepath, FileMode.Open, FileAccess.Read);
            //calculate
            Dictionary<string, long> afc = ReadAFCHeader(rawfiles);
            Dictionary<string, long> blk;
            if (rawfiles.Length != afc["afclen"]) throw new Exception("Incorrect file size.");
            long rawlen = afc["rawlen"];
            Stream fileStream = new FileStream(m_outputpath, FileMode.OpenOrCreate, FileAccess.Write);
            fileStream.SetLength(rawlen);
            long pos = 64;
            long blen = afc["blocklen"];
            long klen = afc["keylen"];
            ReadBuffer = ArrayPool<byte>.Shared.Rent((int)blen);
            for (int i = 0; i < afc["blknum"]; i++)
            {
                rawfiles.Position = pos;
                blk = ReadBlockHeader(rawfiles, pos);
                rawfiles.Position = pos + 32;
                //read data
                await rawfiles.ReadAsync(m_key);
                await rawfiles.ReadAsync(ReadBuffer);
                ReadBuffer = XORCHNK(ReadBuffer, m_key);
                //write
                fileStream.Position = blk["blkid"] * blen;
                long towritelen = fileStream.Position + blen > fileStream.Length ? (fileStream.Length - fileStream.Position) : blen;
                await fileStream.WriteAsync(ReadBuffer, 0, (int)towritelen);
                m_key = null;
                pos += 32 + blen + klen;
            }
            m_key = null;
            ReadBuffer = null;
            fileStream.Close();
            rawfiles.Close();
        }
    }
    public enum FurinaCryption
    {
        Encrypt,
        Decrypt
    }
}