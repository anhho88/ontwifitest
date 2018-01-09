namespace LoadSaveFile
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.ListDataTest = new System.Windows.Forms.ListBox();
            this.btnstart = new System.Windows.Forms.Button();
            this.lbdisplay = new System.Windows.Forms.Label();
            this.rtxbConsole = new System.Windows.Forms.RichTextBox();
            this.cbONT = new System.Windows.Forms.ComboBox();
            this.btnlogin = new System.Windows.Forms.Button();
            this.dgv = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1677, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(96, 29);
            this.toolStripMenuItem1.Text = "Tùy chọn";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(201, 30);
            this.toolStripMenuItem2.Text = "Tùy chỉnh";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(201, 30);
            this.toolStripMenuItem3.Text = "Chọn bài test";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // ListDataTest
            // 
            this.ListDataTest.FormattingEnabled = true;
            this.ListDataTest.ItemHeight = 20;
            this.ListDataTest.Location = new System.Drawing.Point(14, 140);
            this.ListDataTest.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ListDataTest.Name = "ListDataTest";
            this.ListDataTest.Size = new System.Drawing.Size(484, 644);
            this.ListDataTest.TabIndex = 1;
            // 
            // btnstart
            // 
            this.btnstart.Location = new System.Drawing.Point(14, 50);
            this.btnstart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnstart.Name = "btnstart";
            this.btnstart.Size = new System.Drawing.Size(136, 71);
            this.btnstart.TabIndex = 2;
            this.btnstart.Text = "Start";
            this.btnstart.UseVisualStyleBackColor = true;
            this.btnstart.Click += new System.EventHandler(this.btnstart_Click);
            // 
            // lbdisplay
            // 
            this.lbdisplay.AutoSize = true;
            this.lbdisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbdisplay.Location = new System.Drawing.Point(171, 62);
            this.lbdisplay.Name = "lbdisplay";
            this.lbdisplay.Size = new System.Drawing.Size(43, 40);
            this.lbdisplay.TabIndex = 3;
            this.lbdisplay.Text = "--";
            // 
            // rtxbConsole
            // 
            this.rtxbConsole.Location = new System.Drawing.Point(518, 140);
            this.rtxbConsole.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.rtxbConsole.Name = "rtxbConsole";
            this.rtxbConsole.Size = new System.Drawing.Size(541, 644);
            this.rtxbConsole.TabIndex = 4;
            this.rtxbConsole.Text = "";
            // 
            // cbONT
            // 
            this.cbONT.FormattingEnabled = true;
            this.cbONT.Location = new System.Drawing.Point(254, 71);
            this.cbONT.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbONT.Name = "cbONT";
            this.cbONT.Size = new System.Drawing.Size(136, 28);
            this.cbONT.TabIndex = 5;
            // 
            // btnlogin
            // 
            this.btnlogin.Location = new System.Drawing.Point(428, 50);
            this.btnlogin.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnlogin.Name = "btnlogin";
            this.btnlogin.Size = new System.Drawing.Size(136, 71);
            this.btnlogin.TabIndex = 6;
            this.btnlogin.Text = "Login";
            this.btnlogin.UseVisualStyleBackColor = true;
            this.btnlogin.Click += new System.EventHandler(this.btnlogin_Click);
            // 
            // dgv
            // 
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dgv.Location = new System.Drawing.Point(1065, 140);
            this.dgv.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dgv.Name = "dgv";
            this.dgv.RowTemplate.Height = 24;
            this.dgv.Size = new System.Drawing.Size(598, 645);
            this.dgv.TabIndex = 7;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Power";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Freq Error";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Symbol Clock Error";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "EVM";
            this.Column4.Name = "Column4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1677, 806);
            this.Controls.Add(this.dgv);
            this.Controls.Add(this.btnlogin);
            this.Controls.Add(this.cbONT);
            this.Controls.Add(this.rtxbConsole);
            this.Controls.Add(this.lbdisplay);
            this.Controls.Add(this.btnstart);
            this.Controls.Add(this.ListDataTest);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ListBox ListDataTest;
        private System.Windows.Forms.Button btnstart;
        private System.Windows.Forms.Label lbdisplay;
        private System.Windows.Forms.RichTextBox rtxbConsole;
        private System.Windows.Forms.ComboBox cbONT;
        private System.Windows.Forms.Button btnlogin;
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
    }
}

