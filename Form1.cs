using System.Diagnostics;
using Furina;

namespace AdvancedFurinaCryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void ControlPanel(bool i)
        {
            groupBox1.Enabled = FileList.Enabled = i;
        }
        private void SelectOutput_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                Output.Text = folderDialog.SelectedPath; // 用户选择的文件夹路径
            }
        }
        
        private async void RunEnc_Click(object sender, EventArgs e)
        {
            try
            {
                ControlPanel(false);
                if (Output.Text == String.Empty || FileList.Items.Count <= 0 || !InitialEnc())
                {
                    ControlPanel(true);
                    return;
                }
                TotalPgs.Value = TotalPgs.Minimum = 0;
                TotalPgs.Maximum = FileList.Items.Count;
                List<string> files = new List<string>();
                foreach (string file in FileList.Items)
                {
                    if (File.Exists(file)) files.Add(file);
                }
                for (int i = 0; i < FileList.Items.Count; i++)
                {
                    FilePgs.Style = ProgressBarStyle.Marquee;
                    Application.DoEvents();
                    string of = Path.Combine(Output.Text, Path.GetFileName(files[i]) + ".afc");
                    if (File.Exists(of))
                    {
                        of += Guid.NewGuid().ToString() + ".afc";
                    }
                    Furina.AdvancedFurinaCryption afc = new(FurinaCryption.Encrypt, files[i], of);
                    afc.SetBlockLength(int.Parse(BlkSize.Text) * int.Parse(KeySize.Text));
                    afc.SetKeyLength(int.Parse(KeySize.Text));
                    await afc.Enc(isNoEnc.Checked);
                    TotalPgs.Value++;
                    FilePgs.Style = ProgressBarStyle.Blocks;
                    Application.DoEvents();
                }
                ControlPanel(true);
                MessageBox.Show("加密完毕！");
            }
            catch (Exception ex)
            {
                ControlPanel(true);
                MessageBox.Show(ex.Message);
            }
        }

        private void Addfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ValidateNames = true;
            openFileDialog.Multiselect = true;
            //openFileDialog.InitialDirectory = "c:\\";//注意这里写路径时要用c:\\而不是c:\
            openFileDialog.Filter = "所有文件|*.*";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (string file in openFileDialog.FileNames)
                {
                    if (!FileList.Items.Contains(file)) FileList.Items.Add(file);
                }
            }
        }

        private async void RunDec_Click(object sender, EventArgs e)
        {
            try
            {
                ControlPanel(false);
                if (Output.Text == String.Empty || FileList.Items.Count <= 0)
                {
                    ControlPanel(true);
                    return;
                }
                List<string> files = new List<string>();
                foreach (string file in FileList.Items)
                {
                    if (Path.GetExtension(file) == ".afc" && File.Exists(file)) files.Add(file);
                }
                if (files.Count <= 0)
                {
                    ControlPanel(true);
                    return;
                }
                TotalPgs.Value = TotalPgs.Minimum = 0;
                TotalPgs.Maximum = files.Count;
                for (int i = 0; i < files.Count; i++)
                {
                    FilePgs.Style = ProgressBarStyle.Marquee;
                    Application.DoEvents();
                    string of = Path.Combine(Output.Text, Path.GetFileName(files[i].ToString()).Remove(Path.GetFileName(files[i].ToString()).Length - 4));
                    if (File.Exists(of))
                    {
                        of += Guid.NewGuid().ToString() + Path.GetExtension(of);
                    }
                    Furina.AdvancedFurinaCryption afc = new(FurinaCryption.Decrypt, files[i], of);
                    await afc.Dec();
                    TotalPgs.Value++;
                    FilePgs.Style = ProgressBarStyle.Blocks;
                    Application.DoEvents();
                }
                ControlPanel(true);
                MessageBox.Show("解密完毕！");
            }
            catch (Exception ex)
            {
                ControlPanel(true);
                MessageBox.Show(ex.Message);
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileList.SelectedItem != null)
            {
                FileList.Items.Remove(FileList.SelectedItem);
            }
        }

        private void FileList_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int posindex = FileList.IndexFromPoint(new Point(e.X, e.Y));
                FileList.ContextMenuStrip = null;
                if (posindex >= 0 && posindex < FileList.Items.Count)
                {
                    FileList.SelectedIndex = posindex;
                    contextMenuStrip1.Show(FileList, new Point(e.X, e.Y));
                }

            }

        }

        private void FileList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && FileList.SelectedItem != null)
            {
                FileList.Items.Remove(FileList.SelectedItem);
            }


        }

        private void FileList_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                String[] files = (String[])e.Data.GetData(DataFormats.FileDrop);
                foreach (String s in files)
                {
                    if (File.Exists(s) && !FileList.Items.Contains(s)) FileList.Items.Add(s);
                }
            }
        }

        private void FileList_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }
        public bool InitialEnc()
        {
            int KLen,Bs;
            if (!int.TryParse(KeySize.Text, out KLen) || !int.TryParse(BlkSize.Text, out Bs))
            {
                return false;
            }
            else
            {
                if (Bs <= 0 || KLen <= 0)
                {
                    return false;
                }
                else if (Bs * KLen > int.MaxValue || KLen % 16 != 0 || KLen > int.MaxValue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private void 清空列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FileList.Items.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (GDEF.args.Length > 0)
                {
                    foreach (string s in GDEF.args)
                    {
                        if(File.Exists(s)) FileList.Items.Add(s);
                    }
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
