namespace LoadSaveFile
{
    partial class txConfig
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
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.grwifi = new System.Windows.Forms.GroupBox();
            this.cbwifi = new System.Windows.Forms.ComboBox();
            this.grchannel = new System.Windows.Forms.GroupBox();
            this.txtchannel = new System.Windows.Forms.TextBox();
            this.granten = new System.Windows.Forms.GroupBox();
            this.txtanten = new System.Windows.Forms.TextBox();
            this.grrate = new System.Windows.Forms.GroupBox();
            this.txtrate = new System.Windows.Forms.TextBox();
            this.grpower = new System.Windows.Forms.GroupBox();
            this.txtpower = new System.Windows.Forms.TextBox();
            this.btnsave = new System.Windows.Forms.Button();
            this.btnmodify = new System.Windows.Forms.Button();
            this.btndelete = new System.Windows.Forms.Button();
            this.ListData = new System.Windows.Forms.ListBox();
            this.menuStrip1.SuspendLayout();
            this.grwifi.SuspendLayout();
            this.grchannel.SuspendLayout();
            this.granten.SuspendLayout();
            this.grrate.SuspendLayout();
            this.grpower.SuspendLayout();
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
            this.menuStrip1.Size = new System.Drawing.Size(1133, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem4,
            this.toolStripMenuItem3});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(50, 29);
            this.toolStripMenuItem1.Text = "File";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(189, 30);
            this.toolStripMenuItem2.Text = "Load File";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(189, 30);
            this.toolStripMenuItem4.Text = "Save File";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(189, 30);
            this.toolStripMenuItem3.Text = "Save As File";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // grwifi
            // 
            this.grwifi.Controls.Add(this.cbwifi);
            this.grwifi.Location = new System.Drawing.Point(14, 231);
            this.grwifi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grwifi.Name = "grwifi";
            this.grwifi.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grwifi.Size = new System.Drawing.Size(177, 78);
            this.grwifi.TabIndex = 2;
            this.grwifi.TabStop = false;
            this.grwifi.Text = "Wifi Types";
            // 
            // cbwifi
            // 
            this.cbwifi.FormattingEnabled = true;
            this.cbwifi.Location = new System.Drawing.Point(22, 26);
            this.cbwifi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbwifi.Name = "cbwifi";
            this.cbwifi.Size = new System.Drawing.Size(136, 28);
            this.cbwifi.TabIndex = 0;
            this.cbwifi.SelectedIndexChanged += new System.EventHandler(this.cbwifi_SelectedIndexChanged);
            // 
            // grchannel
            // 
            this.grchannel.Controls.Add(this.txtchannel);
            this.grchannel.Location = new System.Drawing.Point(382, 231);
            this.grchannel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grchannel.Name = "grchannel";
            this.grchannel.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grchannel.Size = new System.Drawing.Size(238, 78);
            this.grchannel.TabIndex = 3;
            this.grchannel.TabStop = false;
            this.grchannel.Text = "Channel";
            // 
            // txtchannel
            // 
            this.txtchannel.Location = new System.Drawing.Point(7, 26);
            this.txtchannel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtchannel.Multiline = true;
            this.txtchannel.Name = "txtchannel";
            this.txtchannel.Size = new System.Drawing.Size(224, 29);
            this.txtchannel.TabIndex = 0;
            // 
            // granten
            // 
            this.granten.Controls.Add(this.txtanten);
            this.granten.Location = new System.Drawing.Point(213, 231);
            this.granten.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.granten.Name = "granten";
            this.granten.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.granten.Size = new System.Drawing.Size(140, 78);
            this.granten.TabIndex = 4;
            this.granten.TabStop = false;
            this.granten.Text = "Anten";
            // 
            // txtanten
            // 
            this.txtanten.Location = new System.Drawing.Point(7, 29);
            this.txtanten.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtanten.Multiline = true;
            this.txtanten.Name = "txtanten";
            this.txtanten.Size = new System.Drawing.Size(112, 26);
            this.txtanten.TabIndex = 8;
            // 
            // grrate
            // 
            this.grrate.Controls.Add(this.txtrate);
            this.grrate.Location = new System.Drawing.Point(652, 231);
            this.grrate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grrate.Name = "grrate";
            this.grrate.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grrate.Size = new System.Drawing.Size(244, 78);
            this.grrate.TabIndex = 5;
            this.grrate.TabStop = false;
            this.grrate.Text = "Rate";
            // 
            // txtrate
            // 
            this.txtrate.Location = new System.Drawing.Point(7, 26);
            this.txtrate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrate.Multiline = true;
            this.txtrate.Name = "txtrate";
            this.txtrate.Size = new System.Drawing.Size(224, 29);
            this.txtrate.TabIndex = 1;
            // 
            // grpower
            // 
            this.grpower.Controls.Add(this.txtpower);
            this.grpower.Location = new System.Drawing.Point(915, 231);
            this.grpower.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpower.Name = "grpower";
            this.grpower.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.grpower.Size = new System.Drawing.Size(120, 78);
            this.grpower.TabIndex = 6;
            this.grpower.TabStop = false;
            this.grpower.Text = "Power";
            // 
            // txtpower
            // 
            this.txtpower.Location = new System.Drawing.Point(7, 26);
            this.txtpower.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtpower.Multiline = true;
            this.txtpower.Name = "txtpower";
            this.txtpower.Size = new System.Drawing.Size(98, 29);
            this.txtpower.TabIndex = 7;
            // 
            // btnsave
            // 
            this.btnsave.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnsave.ForeColor = System.Drawing.Color.Black;
            this.btnsave.Location = new System.Drawing.Point(14, 354);
            this.btnsave.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnsave.Name = "btnsave";
            this.btnsave.Size = new System.Drawing.Size(124, 58);
            this.btnsave.TabIndex = 7;
            this.btnsave.Text = "Save";
            this.btnsave.UseVisualStyleBackColor = true;
            this.btnsave.Click += new System.EventHandler(this.btnsave_Click);
            // 
            // btnmodify
            // 
            this.btnmodify.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmodify.Location = new System.Drawing.Point(166, 354);
            this.btnmodify.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnmodify.Name = "btnmodify";
            this.btnmodify.Size = new System.Drawing.Size(124, 58);
            this.btnmodify.TabIndex = 8;
            this.btnmodify.Text = "Modify";
            this.btnmodify.UseVisualStyleBackColor = true;
            this.btnmodify.Click += new System.EventHandler(this.btnmodify_Click);
            // 
            // btndelete
            // 
            this.btndelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.Location = new System.Drawing.Point(317, 354);
            this.btndelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(124, 58);
            this.btndelete.TabIndex = 9;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // ListData
            // 
            this.ListData.FormattingEnabled = true;
            this.ListData.ItemHeight = 20;
            this.ListData.Location = new System.Drawing.Point(14, 39);
            this.ListData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ListData.Name = "ListData";
            this.ListData.Size = new System.Drawing.Size(1105, 164);
            this.ListData.TabIndex = 10;
            // 
            // txConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1133, 499);
            this.Controls.Add(this.ListData);
            this.Controls.Add(this.btndelete);
            this.Controls.Add(this.btnmodify);
            this.Controls.Add(this.btnsave);
            this.Controls.Add(this.grpower);
            this.Controls.Add(this.grrate);
            this.Controls.Add(this.granten);
            this.Controls.Add(this.grchannel);
            this.Controls.Add(this.grwifi);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "txConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tuychinh";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.grwifi.ResumeLayout(false);
            this.grchannel.ResumeLayout(false);
            this.grchannel.PerformLayout();
            this.granten.ResumeLayout(false);
            this.granten.PerformLayout();
            this.grrate.ResumeLayout(false);
            this.grrate.PerformLayout();
            this.grpower.ResumeLayout(false);
            this.grpower.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.GroupBox grwifi;
        private System.Windows.Forms.ComboBox cbwifi;
        private System.Windows.Forms.GroupBox grchannel;
        private System.Windows.Forms.TextBox txtchannel;
        private System.Windows.Forms.GroupBox granten;
        private System.Windows.Forms.GroupBox grrate;
        private System.Windows.Forms.TextBox txtrate;
        private System.Windows.Forms.GroupBox grpower;
        private System.Windows.Forms.TextBox txtpower;
        private System.Windows.Forms.Button btnsave;
        private System.Windows.Forms.Button btnmodify;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.TextBox txtanten;
        private System.Windows.Forms.ListBox ListData;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
    }
}