namespace LoadSaveFile
{
    partial class rxConfig {
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
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.txtpacket = new System.Windows.Forms.TextBox();
            this.btndelete = new System.Windows.Forms.Button();
            this.btnmodify = new System.Windows.Forms.Button();
            this.Save = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.txtpower = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtrate = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtchannel = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtanten = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbwifi = new System.Windows.Forms.ComboBox();
            this.ListData = new System.Windows.Forms.ListBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.setRxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.txtdelay = new System.Windows.Forms.TextBox();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.txtpacket);
            this.groupBox6.Location = new System.Drawing.Point(955, 316);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox6.Size = new System.Drawing.Size(132, 84);
            this.groupBox6.TabIndex = 30;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Packet Count";
            // 
            // txtpacket
            // 
            this.txtpacket.Location = new System.Drawing.Point(7, 39);
            this.txtpacket.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtpacket.Multiline = true;
            this.txtpacket.Name = "txtpacket";
            this.txtpacket.Size = new System.Drawing.Size(118, 29);
            this.txtpacket.TabIndex = 0;
            // 
            // btndelete
            // 
            this.btndelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndelete.Location = new System.Drawing.Point(397, 461);
            this.btndelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btndelete.Name = "btndelete";
            this.btndelete.Size = new System.Drawing.Size(145, 65);
            this.btndelete.TabIndex = 29;
            this.btndelete.Text = "Delete";
            this.btndelete.UseVisualStyleBackColor = true;
            this.btndelete.Click += new System.EventHandler(this.btndelete_Click);
            // 
            // btnmodify
            // 
            this.btnmodify.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnmodify.Location = new System.Drawing.Point(209, 461);
            this.btnmodify.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnmodify.Name = "btnmodify";
            this.btnmodify.Size = new System.Drawing.Size(145, 65);
            this.btnmodify.TabIndex = 28;
            this.btnmodify.Text = "Modify";
            this.btnmodify.UseVisualStyleBackColor = true;
            this.btnmodify.Click += new System.EventHandler(this.btnmodify_Click);
            // 
            // Save
            // 
            this.Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Save.Location = new System.Drawing.Point(20, 461);
            this.Save.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(145, 65);
            this.Save.TabIndex = 27;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click_1);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.txtpower);
            this.groupBox5.Location = new System.Drawing.Point(827, 316);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox5.Size = new System.Drawing.Size(102, 84);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Power";
            // 
            // txtpower
            // 
            this.txtpower.Location = new System.Drawing.Point(7, 39);
            this.txtpower.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtpower.Multiline = true;
            this.txtpower.Name = "txtpower";
            this.txtpower.Size = new System.Drawing.Size(88, 29);
            this.txtpower.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtrate);
            this.groupBox4.Location = new System.Drawing.Point(585, 316);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox4.Size = new System.Drawing.Size(215, 84);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Rate";
            // 
            // txtrate
            // 
            this.txtrate.Location = new System.Drawing.Point(7, 39);
            this.txtrate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtrate.Multiline = true;
            this.txtrate.Name = "txtrate";
            this.txtrate.Size = new System.Drawing.Size(194, 29);
            this.txtrate.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtchannel);
            this.groupBox3.Location = new System.Drawing.Point(341, 316);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(215, 84);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Channel";
            // 
            // txtchannel
            // 
            this.txtchannel.Location = new System.Drawing.Point(7, 39);
            this.txtchannel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtchannel.Multiline = true;
            this.txtchannel.Name = "txtchannel";
            this.txtchannel.Size = new System.Drawing.Size(194, 29);
            this.txtchannel.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtanten);
            this.groupBox2.Location = new System.Drawing.Point(209, 316);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(106, 84);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Anten";
            // 
            // txtanten
            // 
            this.txtanten.Location = new System.Drawing.Point(7, 39);
            this.txtanten.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtanten.Multiline = true;
            this.txtanten.Name = "txtanten";
            this.txtanten.Size = new System.Drawing.Size(80, 29);
            this.txtanten.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbwifi);
            this.groupBox1.Location = new System.Drawing.Point(14, 316);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(161, 84);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Wifi";
            // 
            // cbwifi
            // 
            this.cbwifi.FormattingEnabled = true;
            this.cbwifi.Location = new System.Drawing.Point(7, 39);
            this.cbwifi.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cbwifi.Name = "cbwifi";
            this.cbwifi.Size = new System.Drawing.Size(145, 28);
            this.cbwifi.TabIndex = 0;
            this.cbwifi.SelectedIndexChanged += new System.EventHandler(this.cbwifi_SelectedIndexChanged_1);
            // 
            // ListData
            // 
            this.ListData.FormattingEnabled = true;
            this.ListData.ItemHeight = 20;
            this.ListData.Location = new System.Drawing.Point(14, 48);
            this.ListData.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ListData.Name = "ListData";
            this.ListData.Size = new System.Drawing.Size(1209, 224);
            this.ListData.TabIndex = 21;
            this.ListData.Click += new System.EventHandler(this.ListData_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setRxToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1236, 33);
            this.menuStrip1.TabIndex = 31;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // setRxToolStripMenuItem
            // 
            this.setRxToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadFileToolStripMenuItem,
            this.saveFileToolStripMenuItem,
            this.saveAsFileToolStripMenuItem});
            this.setRxToolStripMenuItem.Name = "setRxToolStripMenuItem";
            this.setRxToolStripMenuItem.Size = new System.Drawing.Size(73, 29);
            this.setRxToolStripMenuItem.Text = "Set Rx";
            // 
            // loadFileToolStripMenuItem
            // 
            this.loadFileToolStripMenuItem.Name = "loadFileToolStripMenuItem";
            this.loadFileToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
            this.loadFileToolStripMenuItem.Text = "Load File";
            this.loadFileToolStripMenuItem.Click += new System.EventHandler(this.loadFileToolStripMenuItem_Click);
            // 
            // saveFileToolStripMenuItem
            // 
            this.saveFileToolStripMenuItem.Name = "saveFileToolStripMenuItem";
            this.saveFileToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
            this.saveFileToolStripMenuItem.Text = "Save File";
            this.saveFileToolStripMenuItem.Click += new System.EventHandler(this.saveFileToolStripMenuItem_Click);
            // 
            // saveAsFileToolStripMenuItem
            // 
            this.saveAsFileToolStripMenuItem.Name = "saveAsFileToolStripMenuItem";
            this.saveAsFileToolStripMenuItem.Size = new System.Drawing.Size(189, 30);
            this.saveAsFileToolStripMenuItem.Text = "Save As File";
            this.saveAsFileToolStripMenuItem.Click += new System.EventHandler(this.saveAsFileToolStripMenuItem_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.txtdelay);
            this.groupBox7.Location = new System.Drawing.Point(1118, 316);
            this.groupBox7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox7.Size = new System.Drawing.Size(105, 84);
            this.groupBox7.TabIndex = 32;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Delay";
            // 
            // txtdelay
            // 
            this.txtdelay.Location = new System.Drawing.Point(7, 39);
            this.txtdelay.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtdelay.Multiline = true;
            this.txtdelay.Name = "txtdelay";
            this.txtdelay.Size = new System.Drawing.Size(91, 29);
            this.txtdelay.TabIndex = 0;
            // 
            // rxConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1236, 543);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.btndelete);
            this.Controls.Add(this.btnmodify);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ListData);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "rxConfig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RX CONFIG";
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox txtpacket;
        private System.Windows.Forms.Button btndelete;
        private System.Windows.Forms.Button btnmodify;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.TextBox txtpower;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox txtrate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtchannel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtanten;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbwifi;
        private System.Windows.Forms.ListBox ListData;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem setRxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsFileToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox txtdelay;
    }
}

