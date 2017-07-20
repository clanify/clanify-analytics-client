namespace clanify_analyzer_client
{
    partial class frmClient
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSelectDemo = new System.Windows.Forms.TextBox();
            this.btnSelectDemo = new System.Windows.Forms.Button();
            this.lblInfoEventName = new System.Windows.Forms.Label();
            this.cmbInfoEventName = new System.Windows.Forms.ComboBox();
            this.lblInfoMatchDate = new System.Windows.Forms.Label();
            this.dtpInfoMatchDate = new System.Windows.Forms.DateTimePicker();
            this.btnReadDemoHeader = new System.Windows.Forms.Button();
            this.cmbInfoMapName = new System.Windows.Forms.ComboBox();
            this.lblInfoMapName = new System.Windows.Forms.Label();
            this.tcMain = new System.Windows.Forms.TabControl();
            this.tabDemoInfo = new System.Windows.Forms.TabPage();
            this.dtpInfoMatchTime = new System.Windows.Forms.DateTimePicker();
            this.lblInfoMatchTime = new System.Windows.Forms.Label();
            this.tabDemoHeader = new System.Windows.Forms.TabPage();
            this.lblHeaderSignonLength = new System.Windows.Forms.Label();
            this.txtHeaderSignonLength = new System.Windows.Forms.TextBox();
            this.lblHeaderServerName = new System.Windows.Forms.Label();
            this.txtHeaderServerName = new System.Windows.Forms.TextBox();
            this.txtHeaderProtocol = new System.Windows.Forms.TextBox();
            this.lblHeaderProtocol = new System.Windows.Forms.Label();
            this.lblHeaderMapName = new System.Windows.Forms.Label();
            this.txtHeaderMapName = new System.Windows.Forms.TextBox();
            this.lblHeaderGameDirectory = new System.Windows.Forms.Label();
            this.txtHeaderGameDirectory = new System.Windows.Forms.TextBox();
            this.lblHeaderFilestamp = new System.Windows.Forms.Label();
            this.txtHeaderFilestamp = new System.Windows.Forms.TextBox();
            this.lblHeaderClientName = new System.Windows.Forms.Label();
            this.txtHeaderClientName = new System.Windows.Forms.TextBox();
            this.lblHeaderPlaybackTime = new System.Windows.Forms.Label();
            this.txtHeaderPlaybackTime = new System.Windows.Forms.TextBox();
            this.lblHeaderPlaybackTicks = new System.Windows.Forms.Label();
            this.txtHeaderPlaybackTicks = new System.Windows.Forms.TextBox();
            this.lblHeaderPlaybackFrames = new System.Windows.Forms.Label();
            this.txtHeaderPlaybackFrames = new System.Windows.Forms.TextBox();
            this.lblHeaderNetworkProtocol = new System.Windows.Forms.Label();
            this.txtHeaderNetworkProtocol = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslblConnState = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbDemoProgress = new System.Windows.Forms.ProgressBar();
            this.tcMain.SuspendLayout();
            this.tabDemoInfo.SuspendLayout();
            this.tabDemoHeader.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSelectDemo
            // 
            this.txtSelectDemo.Enabled = false;
            this.txtSelectDemo.Location = new System.Drawing.Point(12, 12);
            this.txtSelectDemo.Name = "txtSelectDemo";
            this.txtSelectDemo.ReadOnly = true;
            this.txtSelectDemo.Size = new System.Drawing.Size(391, 20);
            this.txtSelectDemo.TabIndex = 0;
            // 
            // btnSelectDemo
            // 
            this.btnSelectDemo.Location = new System.Drawing.Point(409, 10);
            this.btnSelectDemo.Name = "btnSelectDemo";
            this.btnSelectDemo.Size = new System.Drawing.Size(68, 23);
            this.btnSelectDemo.TabIndex = 1;
            this.btnSelectDemo.Text = "Demo...";
            this.btnSelectDemo.UseVisualStyleBackColor = true;
            this.btnSelectDemo.Click += new System.EventHandler(this.btnSelectDemo_Click);
            // 
            // lblInfoEventName
            // 
            this.lblInfoEventName.AutoSize = true;
            this.lblInfoEventName.Location = new System.Drawing.Point(10, 47);
            this.lblInfoEventName.Name = "lblInfoEventName";
            this.lblInfoEventName.Size = new System.Drawing.Size(35, 13);
            this.lblInfoEventName.TabIndex = 3;
            this.lblInfoEventName.Text = "Event";
            // 
            // cmbInfoEventName
            // 
            this.cmbInfoEventName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInfoEventName.FormattingEnabled = true;
            this.cmbInfoEventName.Items.AddRange(new object[] {
            "ESL One New York 2016",
            "ESL One Cologne 2017"});
            this.cmbInfoEventName.Location = new System.Drawing.Point(51, 44);
            this.cmbInfoEventName.Name = "cmbInfoEventName";
            this.cmbInfoEventName.Size = new System.Drawing.Size(218, 21);
            this.cmbInfoEventName.TabIndex = 2;
            // 
            // lblInfoMatchDate
            // 
            this.lblInfoMatchDate.AutoSize = true;
            this.lblInfoMatchDate.Location = new System.Drawing.Point(7, 22);
            this.lblInfoMatchDate.Name = "lblInfoMatchDate";
            this.lblInfoMatchDate.Size = new System.Drawing.Size(38, 13);
            this.lblInfoMatchDate.TabIndex = 1;
            this.lblInfoMatchDate.Text = "Datum";
            // 
            // dtpInfoMatchDate
            // 
            this.dtpInfoMatchDate.CustomFormat = "";
            this.dtpInfoMatchDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpInfoMatchDate.Location = new System.Drawing.Point(51, 18);
            this.dtpInfoMatchDate.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dtpInfoMatchDate.Name = "dtpInfoMatchDate";
            this.dtpInfoMatchDate.Size = new System.Drawing.Size(97, 20);
            this.dtpInfoMatchDate.TabIndex = 0;
            this.dtpInfoMatchDate.Value = new System.DateTime(2017, 7, 5, 0, 0, 0, 0);
            // 
            // btnReadDemoHeader
            // 
            this.btnReadDemoHeader.Location = new System.Drawing.Point(12, 269);
            this.btnReadDemoHeader.Name = "btnReadDemoHeader";
            this.btnReadDemoHeader.Size = new System.Drawing.Size(75, 23);
            this.btnReadDemoHeader.TabIndex = 3;
            this.btnReadDemoHeader.Text = "Info";
            this.btnReadDemoHeader.UseVisualStyleBackColor = true;
            this.btnReadDemoHeader.Click += new System.EventHandler(this.btnReadDemoHeader_Click);
            // 
            // cmbInfoMapName
            // 
            this.cmbInfoMapName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbInfoMapName.FormattingEnabled = true;
            this.cmbInfoMapName.Location = new System.Drawing.Point(51, 71);
            this.cmbInfoMapName.Name = "cmbInfoMapName";
            this.cmbInfoMapName.Size = new System.Drawing.Size(97, 21);
            this.cmbInfoMapName.TabIndex = 4;
            // 
            // lblInfoMapName
            // 
            this.lblInfoMapName.AutoSize = true;
            this.lblInfoMapName.Location = new System.Drawing.Point(17, 74);
            this.lblInfoMapName.Name = "lblInfoMapName";
            this.lblInfoMapName.Size = new System.Drawing.Size(28, 13);
            this.lblInfoMapName.TabIndex = 5;
            this.lblInfoMapName.Text = "Map";
            // 
            // tcMain
            // 
            this.tcMain.Controls.Add(this.tabDemoInfo);
            this.tcMain.Controls.Add(this.tabDemoHeader);
            this.tcMain.Location = new System.Drawing.Point(12, 54);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedIndex = 0;
            this.tcMain.Size = new System.Drawing.Size(465, 213);
            this.tcMain.TabIndex = 4;
            // 
            // tabDemoInfo
            // 
            this.tabDemoInfo.Controls.Add(this.dtpInfoMatchTime);
            this.tabDemoInfo.Controls.Add(this.lblInfoMatchTime);
            this.tabDemoInfo.Controls.Add(this.lblInfoMapName);
            this.tabDemoInfo.Controls.Add(this.cmbInfoMapName);
            this.tabDemoInfo.Controls.Add(this.cmbInfoEventName);
            this.tabDemoInfo.Controls.Add(this.lblInfoEventName);
            this.tabDemoInfo.Controls.Add(this.dtpInfoMatchDate);
            this.tabDemoInfo.Controls.Add(this.lblInfoMatchDate);
            this.tabDemoInfo.Location = new System.Drawing.Point(4, 22);
            this.tabDemoInfo.Name = "tabDemoInfo";
            this.tabDemoInfo.Padding = new System.Windows.Forms.Padding(3);
            this.tabDemoInfo.Size = new System.Drawing.Size(457, 187);
            this.tabDemoInfo.TabIndex = 0;
            this.tabDemoInfo.Text = "Info";
            this.tabDemoInfo.UseVisualStyleBackColor = true;
            // 
            // dtpInfoMatchTime
            // 
            this.dtpInfoMatchTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpInfoMatchTime.Location = new System.Drawing.Point(185, 18);
            this.dtpInfoMatchTime.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dtpInfoMatchTime.Name = "dtpInfoMatchTime";
            this.dtpInfoMatchTime.Size = new System.Drawing.Size(84, 20);
            this.dtpInfoMatchTime.TabIndex = 7;
            // 
            // lblInfoMatchTime
            // 
            this.lblInfoMatchTime.AutoSize = true;
            this.lblInfoMatchTime.Location = new System.Drawing.Point(154, 22);
            this.lblInfoMatchTime.Name = "lblInfoMatchTime";
            this.lblInfoMatchTime.Size = new System.Drawing.Size(25, 13);
            this.lblInfoMatchTime.TabIndex = 6;
            this.lblInfoMatchTime.Text = "Zeit";
            // 
            // tabDemoHeader
            // 
            this.tabDemoHeader.Controls.Add(this.lblHeaderSignonLength);
            this.tabDemoHeader.Controls.Add(this.txtHeaderSignonLength);
            this.tabDemoHeader.Controls.Add(this.lblHeaderServerName);
            this.tabDemoHeader.Controls.Add(this.txtHeaderServerName);
            this.tabDemoHeader.Controls.Add(this.txtHeaderProtocol);
            this.tabDemoHeader.Controls.Add(this.lblHeaderProtocol);
            this.tabDemoHeader.Controls.Add(this.lblHeaderMapName);
            this.tabDemoHeader.Controls.Add(this.txtHeaderMapName);
            this.tabDemoHeader.Controls.Add(this.lblHeaderGameDirectory);
            this.tabDemoHeader.Controls.Add(this.txtHeaderGameDirectory);
            this.tabDemoHeader.Controls.Add(this.lblHeaderFilestamp);
            this.tabDemoHeader.Controls.Add(this.txtHeaderFilestamp);
            this.tabDemoHeader.Controls.Add(this.lblHeaderClientName);
            this.tabDemoHeader.Controls.Add(this.txtHeaderClientName);
            this.tabDemoHeader.Controls.Add(this.lblHeaderPlaybackTime);
            this.tabDemoHeader.Controls.Add(this.txtHeaderPlaybackTime);
            this.tabDemoHeader.Controls.Add(this.lblHeaderPlaybackTicks);
            this.tabDemoHeader.Controls.Add(this.txtHeaderPlaybackTicks);
            this.tabDemoHeader.Controls.Add(this.lblHeaderPlaybackFrames);
            this.tabDemoHeader.Controls.Add(this.txtHeaderPlaybackFrames);
            this.tabDemoHeader.Controls.Add(this.lblHeaderNetworkProtocol);
            this.tabDemoHeader.Controls.Add(this.txtHeaderNetworkProtocol);
            this.tabDemoHeader.Location = new System.Drawing.Point(4, 22);
            this.tabDemoHeader.Name = "tabDemoHeader";
            this.tabDemoHeader.Padding = new System.Windows.Forms.Padding(3);
            this.tabDemoHeader.Size = new System.Drawing.Size(457, 187);
            this.tabDemoHeader.TabIndex = 1;
            this.tabDemoHeader.Text = "Header";
            this.tabDemoHeader.UseVisualStyleBackColor = true;
            // 
            // lblHeaderSignonLength
            // 
            this.lblHeaderSignonLength.AutoSize = true;
            this.lblHeaderSignonLength.Location = new System.Drawing.Point(239, 157);
            this.lblHeaderSignonLength.Name = "lblHeaderSignonLength";
            this.lblHeaderSignonLength.Size = new System.Drawing.Size(76, 13);
            this.lblHeaderSignonLength.TabIndex = 35;
            this.lblHeaderSignonLength.Text = "Signon Length";
            // 
            // txtHeaderSignonLength
            // 
            this.txtHeaderSignonLength.Enabled = false;
            this.txtHeaderSignonLength.Location = new System.Drawing.Point(321, 154);
            this.txtHeaderSignonLength.Name = "txtHeaderSignonLength";
            this.txtHeaderSignonLength.ReadOnly = true;
            this.txtHeaderSignonLength.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderSignonLength.TabIndex = 34;
            // 
            // lblHeaderServerName
            // 
            this.lblHeaderServerName.AutoSize = true;
            this.lblHeaderServerName.Location = new System.Drawing.Point(246, 131);
            this.lblHeaderServerName.Name = "lblHeaderServerName";
            this.lblHeaderServerName.Size = new System.Drawing.Size(69, 13);
            this.lblHeaderServerName.TabIndex = 33;
            this.lblHeaderServerName.Text = "Server Name";
            // 
            // txtHeaderServerName
            // 
            this.txtHeaderServerName.Enabled = false;
            this.txtHeaderServerName.Location = new System.Drawing.Point(321, 128);
            this.txtHeaderServerName.Name = "txtHeaderServerName";
            this.txtHeaderServerName.ReadOnly = true;
            this.txtHeaderServerName.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderServerName.TabIndex = 32;
            // 
            // txtHeaderProtocol
            // 
            this.txtHeaderProtocol.Enabled = false;
            this.txtHeaderProtocol.Location = new System.Drawing.Point(321, 102);
            this.txtHeaderProtocol.Name = "txtHeaderProtocol";
            this.txtHeaderProtocol.ReadOnly = true;
            this.txtHeaderProtocol.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderProtocol.TabIndex = 31;
            // 
            // lblHeaderProtocol
            // 
            this.lblHeaderProtocol.AutoSize = true;
            this.lblHeaderProtocol.Location = new System.Drawing.Point(269, 105);
            this.lblHeaderProtocol.Name = "lblHeaderProtocol";
            this.lblHeaderProtocol.Size = new System.Drawing.Size(46, 13);
            this.lblHeaderProtocol.TabIndex = 30;
            this.lblHeaderProtocol.Text = "Protocol";
            // 
            // lblHeaderMapName
            // 
            this.lblHeaderMapName.AutoSize = true;
            this.lblHeaderMapName.Location = new System.Drawing.Point(36, 105);
            this.lblHeaderMapName.Name = "lblHeaderMapName";
            this.lblHeaderMapName.Size = new System.Drawing.Size(59, 13);
            this.lblHeaderMapName.TabIndex = 29;
            this.lblHeaderMapName.Text = "Map Name";
            // 
            // txtHeaderMapName
            // 
            this.txtHeaderMapName.Enabled = false;
            this.txtHeaderMapName.Location = new System.Drawing.Point(101, 102);
            this.txtHeaderMapName.Name = "txtHeaderMapName";
            this.txtHeaderMapName.ReadOnly = true;
            this.txtHeaderMapName.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderMapName.TabIndex = 28;
            // 
            // lblHeaderGameDirectory
            // 
            this.lblHeaderGameDirectory.AutoSize = true;
            this.lblHeaderGameDirectory.Location = new System.Drawing.Point(15, 79);
            this.lblHeaderGameDirectory.Name = "lblHeaderGameDirectory";
            this.lblHeaderGameDirectory.Size = new System.Drawing.Size(80, 13);
            this.lblHeaderGameDirectory.TabIndex = 27;
            this.lblHeaderGameDirectory.Text = "Game Directory";
            // 
            // txtHeaderGameDirectory
            // 
            this.txtHeaderGameDirectory.Enabled = false;
            this.txtHeaderGameDirectory.Location = new System.Drawing.Point(101, 76);
            this.txtHeaderGameDirectory.Name = "txtHeaderGameDirectory";
            this.txtHeaderGameDirectory.ReadOnly = true;
            this.txtHeaderGameDirectory.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderGameDirectory.TabIndex = 26;
            // 
            // lblHeaderFilestamp
            // 
            this.lblHeaderFilestamp.AutoSize = true;
            this.lblHeaderFilestamp.Location = new System.Drawing.Point(44, 53);
            this.lblHeaderFilestamp.Name = "lblHeaderFilestamp";
            this.lblHeaderFilestamp.Size = new System.Drawing.Size(51, 13);
            this.lblHeaderFilestamp.TabIndex = 25;
            this.lblHeaderFilestamp.Text = "Filestamp";
            // 
            // txtHeaderFilestamp
            // 
            this.txtHeaderFilestamp.Enabled = false;
            this.txtHeaderFilestamp.Location = new System.Drawing.Point(101, 50);
            this.txtHeaderFilestamp.Name = "txtHeaderFilestamp";
            this.txtHeaderFilestamp.ReadOnly = true;
            this.txtHeaderFilestamp.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderFilestamp.TabIndex = 24;
            // 
            // lblHeaderClientName
            // 
            this.lblHeaderClientName.AutoSize = true;
            this.lblHeaderClientName.Location = new System.Drawing.Point(31, 27);
            this.lblHeaderClientName.Name = "lblHeaderClientName";
            this.lblHeaderClientName.Size = new System.Drawing.Size(64, 13);
            this.lblHeaderClientName.TabIndex = 23;
            this.lblHeaderClientName.Text = "Client Name";
            // 
            // txtHeaderClientName
            // 
            this.txtHeaderClientName.Enabled = false;
            this.txtHeaderClientName.Location = new System.Drawing.Point(101, 24);
            this.txtHeaderClientName.Name = "txtHeaderClientName";
            this.txtHeaderClientName.ReadOnly = true;
            this.txtHeaderClientName.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderClientName.TabIndex = 22;
            // 
            // lblHeaderPlaybackTime
            // 
            this.lblHeaderPlaybackTime.AutoSize = true;
            this.lblHeaderPlaybackTime.Location = new System.Drawing.Point(238, 79);
            this.lblHeaderPlaybackTime.Name = "lblHeaderPlaybackTime";
            this.lblHeaderPlaybackTime.Size = new System.Drawing.Size(77, 13);
            this.lblHeaderPlaybackTime.TabIndex = 21;
            this.lblHeaderPlaybackTime.Text = "Playback Time";
            // 
            // txtHeaderPlaybackTime
            // 
            this.txtHeaderPlaybackTime.Enabled = false;
            this.txtHeaderPlaybackTime.Location = new System.Drawing.Point(321, 76);
            this.txtHeaderPlaybackTime.Name = "txtHeaderPlaybackTime";
            this.txtHeaderPlaybackTime.ReadOnly = true;
            this.txtHeaderPlaybackTime.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderPlaybackTime.TabIndex = 20;
            // 
            // lblHeaderPlaybackTicks
            // 
            this.lblHeaderPlaybackTicks.AutoSize = true;
            this.lblHeaderPlaybackTicks.Location = new System.Drawing.Point(235, 53);
            this.lblHeaderPlaybackTicks.Name = "lblHeaderPlaybackTicks";
            this.lblHeaderPlaybackTicks.Size = new System.Drawing.Size(80, 13);
            this.lblHeaderPlaybackTicks.TabIndex = 19;
            this.lblHeaderPlaybackTicks.Text = "Playback Ticks";
            // 
            // txtHeaderPlaybackTicks
            // 
            this.txtHeaderPlaybackTicks.Enabled = false;
            this.txtHeaderPlaybackTicks.Location = new System.Drawing.Point(321, 50);
            this.txtHeaderPlaybackTicks.Name = "txtHeaderPlaybackTicks";
            this.txtHeaderPlaybackTicks.ReadOnly = true;
            this.txtHeaderPlaybackTicks.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderPlaybackTicks.TabIndex = 18;
            // 
            // lblHeaderPlaybackFrames
            // 
            this.lblHeaderPlaybackFrames.AutoSize = true;
            this.lblHeaderPlaybackFrames.Location = new System.Drawing.Point(227, 27);
            this.lblHeaderPlaybackFrames.Name = "lblHeaderPlaybackFrames";
            this.lblHeaderPlaybackFrames.Size = new System.Drawing.Size(88, 13);
            this.lblHeaderPlaybackFrames.TabIndex = 17;
            this.lblHeaderPlaybackFrames.Text = "Playback Frames";
            // 
            // txtHeaderPlaybackFrames
            // 
            this.txtHeaderPlaybackFrames.Enabled = false;
            this.txtHeaderPlaybackFrames.Location = new System.Drawing.Point(321, 24);
            this.txtHeaderPlaybackFrames.Name = "txtHeaderPlaybackFrames";
            this.txtHeaderPlaybackFrames.ReadOnly = true;
            this.txtHeaderPlaybackFrames.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderPlaybackFrames.TabIndex = 16;
            // 
            // lblHeaderNetworkProtocol
            // 
            this.lblHeaderNetworkProtocol.AutoSize = true;
            this.lblHeaderNetworkProtocol.Location = new System.Drawing.Point(6, 131);
            this.lblHeaderNetworkProtocol.Name = "lblHeaderNetworkProtocol";
            this.lblHeaderNetworkProtocol.Size = new System.Drawing.Size(89, 13);
            this.lblHeaderNetworkProtocol.TabIndex = 15;
            this.lblHeaderNetworkProtocol.Text = "Network Protocol";
            // 
            // txtHeaderNetworkProtocol
            // 
            this.txtHeaderNetworkProtocol.Enabled = false;
            this.txtHeaderNetworkProtocol.Location = new System.Drawing.Point(101, 128);
            this.txtHeaderNetworkProtocol.Name = "txtHeaderNetworkProtocol";
            this.txtHeaderNetworkProtocol.ReadOnly = true;
            this.txtHeaderNetworkProtocol.Size = new System.Drawing.Size(110, 20);
            this.txtHeaderNetworkProtocol.TabIndex = 14;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(93, 269);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Speichern";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(392, 269);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(85, 23);
            this.btnSettings.TabIndex = 6;
            this.btnSettings.Text = "Einstellungen";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslblConnState});
            this.statusStrip1.Location = new System.Drawing.Point(0, 297);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(489, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslblConnState
            // 
            this.tslblConnState.Name = "tslblConnState";
            this.tslblConnState.Size = new System.Drawing.Size(0, 17);
            // 
            // pbDemoProgress
            // 
            this.pbDemoProgress.Location = new System.Drawing.Point(12, 38);
            this.pbDemoProgress.Name = "pbDemoProgress";
            this.pbDemoProgress.Size = new System.Drawing.Size(465, 10);
            this.pbDemoProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbDemoProgress.TabIndex = 8;
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 319);
            this.Controls.Add(this.pbDemoProgress);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.btnReadDemoHeader);
            this.Controls.Add(this.btnSelectDemo);
            this.Controls.Add(this.txtSelectDemo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmClient";
            this.ShowIcon = false;
            this.Text = "clanify - Analytics Client";
            this.Load += new System.EventHandler(this.frmClient_Load);
            this.tcMain.ResumeLayout(false);
            this.tabDemoInfo.ResumeLayout(false);
            this.tabDemoInfo.PerformLayout();
            this.tabDemoHeader.ResumeLayout(false);
            this.tabDemoHeader.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSelectDemo;
        private System.Windows.Forms.Button btnSelectDemo;
        private System.Windows.Forms.DateTimePicker dtpInfoMatchDate;
        private System.Windows.Forms.Button btnReadDemoHeader;
        private System.Windows.Forms.Label lblInfoEventName;
        private System.Windows.Forms.ComboBox cmbInfoEventName;
        private System.Windows.Forms.Label lblInfoMatchDate;
        private System.Windows.Forms.Label lblInfoMapName;
        private System.Windows.Forms.ComboBox cmbInfoMapName;
        private System.Windows.Forms.TabControl tcMain;
        private System.Windows.Forms.TabPage tabDemoInfo;
        private System.Windows.Forms.TabPage tabDemoHeader;
        private System.Windows.Forms.Label lblHeaderPlaybackTime;
        private System.Windows.Forms.TextBox txtHeaderPlaybackTime;
        private System.Windows.Forms.Label lblHeaderPlaybackTicks;
        private System.Windows.Forms.TextBox txtHeaderPlaybackTicks;
        private System.Windows.Forms.Label lblHeaderPlaybackFrames;
        private System.Windows.Forms.TextBox txtHeaderPlaybackFrames;
        private System.Windows.Forms.Label lblHeaderNetworkProtocol;
        private System.Windows.Forms.TextBox txtHeaderNetworkProtocol;
        private System.Windows.Forms.Label lblHeaderClientName;
        private System.Windows.Forms.TextBox txtHeaderClientName;
        private System.Windows.Forms.Label lblHeaderFilestamp;
        private System.Windows.Forms.TextBox txtHeaderFilestamp;
        private System.Windows.Forms.Label lblHeaderGameDirectory;
        private System.Windows.Forms.TextBox txtHeaderGameDirectory;
        private System.Windows.Forms.Label lblHeaderMapName;
        private System.Windows.Forms.TextBox txtHeaderMapName;
        private System.Windows.Forms.TextBox txtHeaderProtocol;
        private System.Windows.Forms.Label lblHeaderProtocol;
        private System.Windows.Forms.Label lblHeaderServerName;
        private System.Windows.Forms.TextBox txtHeaderServerName;
        private System.Windows.Forms.Label lblHeaderSignonLength;
        private System.Windows.Forms.TextBox txtHeaderSignonLength;
        private System.Windows.Forms.DateTimePicker dtpInfoMatchTime;
        private System.Windows.Forms.Label lblInfoMatchTime;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslblConnState;
        private System.Windows.Forms.ProgressBar pbDemoProgress;
    }
}

