namespace AdvancedFurinaCryption
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            groupBox1 = new GroupBox();
            isNoEnc = new CheckBox();
            label6 = new Label();
            BlkSize = new TextBox();
            KeySize = new TextBox();
            label5 = new Label();
            label4 = new Label();
            Addfile = new Button();
            RunEnc = new Button();
            RunDec = new Button();
            SelectOutput = new Button();
            Output = new TextBox();
            label1 = new Label();
            FileList = new ListBox();
            contextMenuStrip1 = new ContextMenuStrip(components);
            删除ToolStripMenuItem = new ToolStripMenuItem();
            清空列表ToolStripMenuItem = new ToolStripMenuItem();
            FilePgs = new ProgressBar();
            label2 = new Label();
            label3 = new Label();
            TotalPgs = new ProgressBar();
            groupBox1.SuspendLayout();
            contextMenuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(isNoEnc);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(BlkSize);
            groupBox1.Controls.Add(KeySize);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(Addfile);
            groupBox1.Controls.Add(RunEnc);
            groupBox1.Controls.Add(RunDec);
            groupBox1.Controls.Add(SelectOutput);
            groupBox1.Controls.Add(Output);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(776, 87);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "控制台";
            // 
            // isNoEnc
            // 
            isNoEnc.AutoSize = true;
            isNoEnc.Location = new Point(464, 60);
            isNoEnc.Name = "isNoEnc";
            isNoEnc.Size = new Size(63, 21);
            isNoEnc.TabIndex = 13;
            isNoEnc.Text = "不加密";
            isNoEnc.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(371, 61);
            label6.Name = "label6";
            label6.Size = new Size(75, 17);
            label6.TabIndex = 12;
            label6.Text = "x Key大小）";
            // 
            // BlkSize
            // 
            BlkSize.Location = new Point(265, 55);
            BlkSize.Name = "BlkSize";
            BlkSize.Size = new Size(100, 23);
            BlkSize.TabIndex = 11;
            BlkSize.Text = "4096";
            // 
            // KeySize
            // 
            KeySize.Location = new Point(92, 55);
            KeySize.Name = "KeySize";
            KeySize.Size = new Size(73, 23);
            KeySize.TabIndex = 10;
            KeySize.Text = "256";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(171, 61);
            label5.Name = "label5";
            label5.Size = new Size(88, 17);
            label5.TabIndex = 9;
            label5.Text = "Block大小：（";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 61);
            label4.Name = "label4";
            label4.Size = new Size(65, 17);
            label4.TabIndex = 8;
            label4.Text = "Key大小：";
            // 
            // Addfile
            // 
            Addfile.Location = new Point(533, 55);
            Addfile.Name = "Addfile";
            Addfile.Size = new Size(75, 23);
            Addfile.TabIndex = 7;
            Addfile.Text = "添加文件";
            Addfile.UseVisualStyleBackColor = true;
            Addfile.Click += Addfile_Click;
            // 
            // RunEnc
            // 
            RunEnc.Location = new Point(614, 55);
            RunEnc.Name = "RunEnc";
            RunEnc.Size = new Size(75, 23);
            RunEnc.TabIndex = 4;
            RunEnc.Text = "加密";
            RunEnc.UseVisualStyleBackColor = true;
            RunEnc.Click += RunEnc_Click;
            // 
            // RunDec
            // 
            RunDec.Location = new Point(695, 55);
            RunDec.Name = "RunDec";
            RunDec.Size = new Size(75, 23);
            RunDec.TabIndex = 3;
            RunDec.Text = "解密";
            RunDec.UseVisualStyleBackColor = true;
            RunDec.Click += RunDec_Click;
            // 
            // SelectOutput
            // 
            SelectOutput.Location = new Point(695, 26);
            SelectOutput.Name = "SelectOutput";
            SelectOutput.Size = new Size(75, 23);
            SelectOutput.TabIndex = 2;
            SelectOutput.Text = "选择";
            SelectOutput.UseVisualStyleBackColor = true;
            SelectOutput.Click += SelectOutput_Click;
            // 
            // Output
            // 
            Output.Location = new Point(92, 26);
            Output.Name = "Output";
            Output.Size = new Size(597, 23);
            Output.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 32);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 0;
            label1.Text = "输出文件夹：";
            // 
            // FileList
            // 
            FileList.AllowDrop = true;
            FileList.ContextMenuStrip = contextMenuStrip1;
            FileList.FormattingEnabled = true;
            FileList.ItemHeight = 17;
            FileList.Location = new Point(12, 106);
            FileList.Name = "FileList";
            FileList.Size = new Size(776, 293);
            FileList.TabIndex = 1;
            FileList.DragDrop += FileList_DragDrop;
            FileList.DragEnter += FileList_DragEnter;
            FileList.KeyDown += FileList_KeyDown;
            FileList.MouseDown += FileList_MouseDown;
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { 删除ToolStripMenuItem, 清空列表ToolStripMenuItem });
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(125, 48);
            // 
            // 删除ToolStripMenuItem
            // 
            删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            删除ToolStripMenuItem.Size = new Size(124, 22);
            删除ToolStripMenuItem.Text = "删除";
            删除ToolStripMenuItem.Click += 删除ToolStripMenuItem_Click;
            // 
            // 清空列表ToolStripMenuItem
            // 
            清空列表ToolStripMenuItem.Name = "清空列表ToolStripMenuItem";
            清空列表ToolStripMenuItem.Size = new Size(124, 22);
            清空列表ToolStripMenuItem.Text = "清空列表";
            清空列表ToolStripMenuItem.Click += 清空列表ToolStripMenuItem_Click;
            // 
            // FilePgs
            // 
            FilePgs.Location = new Point(86, 405);
            FilePgs.Name = "FilePgs";
            FilePgs.Size = new Size(702, 23);
            FilePgs.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 411);
            label2.Name = "label2";
            label2.Size = new Size(68, 17);
            label2.TabIndex = 3;
            label2.Text = "文件进度：";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 440);
            label3.Name = "label3";
            label3.Size = new Size(68, 17);
            label3.TabIndex = 4;
            label3.Text = "整体进度：";
            // 
            // TotalPgs
            // 
            TotalPgs.Location = new Point(86, 434);
            TotalPgs.Name = "TotalPgs";
            TotalPgs.Size = new Size(702, 23);
            TotalPgs.TabIndex = 5;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 466);
            Controls.Add(TotalPgs);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(FilePgs);
            Controls.Add(FileList);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "FurinaCryption2 文件（伪）加密";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            contextMenuStrip1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox groupBox1;
        private Button SelectOutput;
        private TextBox Output;
        private Label label1;
        private Button RunEnc;
        private Button RunDec;
        private ListBox FileList;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 删除ToolStripMenuItem;
        private ProgressBar FilePgs;
        private Label label2;
        private Label label3;
        private ProgressBar TotalPgs;
        private Button Addfile;
        private TextBox BlkSize;
        private TextBox KeySize;
        private Label label5;
        private Label label4;
        private Label label6;
        private ToolStripMenuItem 清空列表ToolStripMenuItem;
        private CheckBox isNoEnc;
    }
}
