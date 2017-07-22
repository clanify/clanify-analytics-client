namespace clanify_analyzer_client
{
    partial class frmSettings
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
            this.txtDatabaseServer = new System.Windows.Forms.TextBox();
            this.lblDatabaseServer = new System.Windows.Forms.Label();
            this.grpDatabaseSettings = new System.Windows.Forms.GroupBox();
            this.lblDatabaseName = new System.Windows.Forms.Label();
            this.txtDatabaseName = new System.Windows.Forms.TextBox();
            this.lblDatabasePassword = new System.Windows.Forms.Label();
            this.txtDatabasePassword = new System.Windows.Forms.TextBox();
            this.lblDatabaseUsername = new System.Windows.Forms.Label();
            this.txtDatabaseUsername = new System.Windows.Forms.TextBox();
            this.lblDatabasePort = new System.Windows.Forms.Label();
            this.txtDatabasePort = new System.Windows.Forms.TextBox();
            this.btnSaveSettings = new System.Windows.Forms.Button();
            this.grpDatabaseSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtDatabaseServer
            // 
            this.txtDatabaseServer.Location = new System.Drawing.Point(103, 26);
            this.txtDatabaseServer.Name = "txtDatabaseServer";
            this.txtDatabaseServer.Size = new System.Drawing.Size(112, 20);
            this.txtDatabaseServer.TabIndex = 0;
            // 
            // lblDatabaseServer
            // 
            this.lblDatabaseServer.AutoSize = true;
            this.lblDatabaseServer.Location = new System.Drawing.Point(28, 29);
            this.lblDatabaseServer.Name = "lblDatabaseServer";
            this.lblDatabaseServer.Size = new System.Drawing.Size(69, 13);
            this.lblDatabaseServer.TabIndex = 1;
            this.lblDatabaseServer.Text = "Server Name";
            // 
            // grpDatabaseSettings
            // 
            this.grpDatabaseSettings.Controls.Add(this.lblDatabaseName);
            this.grpDatabaseSettings.Controls.Add(this.txtDatabaseName);
            this.grpDatabaseSettings.Controls.Add(this.lblDatabasePassword);
            this.grpDatabaseSettings.Controls.Add(this.txtDatabasePassword);
            this.grpDatabaseSettings.Controls.Add(this.lblDatabaseUsername);
            this.grpDatabaseSettings.Controls.Add(this.txtDatabaseUsername);
            this.grpDatabaseSettings.Controls.Add(this.lblDatabasePort);
            this.grpDatabaseSettings.Controls.Add(this.txtDatabasePort);
            this.grpDatabaseSettings.Controls.Add(this.lblDatabaseServer);
            this.grpDatabaseSettings.Controls.Add(this.txtDatabaseServer);
            this.grpDatabaseSettings.Location = new System.Drawing.Point(12, 12);
            this.grpDatabaseSettings.Name = "grpDatabaseSettings";
            this.grpDatabaseSettings.Size = new System.Drawing.Size(221, 163);
            this.grpDatabaseSettings.TabIndex = 2;
            this.grpDatabaseSettings.TabStop = false;
            this.grpDatabaseSettings.Text = "Datenbank";
            // 
            // lblDatabaseName
            // 
            this.lblDatabaseName.AutoSize = true;
            this.lblDatabaseName.Location = new System.Drawing.Point(6, 133);
            this.lblDatabaseName.Name = "lblDatabaseName";
            this.lblDatabaseName.Size = new System.Drawing.Size(91, 13);
            this.lblDatabaseName.TabIndex = 9;
            this.lblDatabaseName.Text = "Datenbank Name";
            // 
            // txtDatabaseName
            // 
            this.txtDatabaseName.Location = new System.Drawing.Point(103, 130);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(112, 20);
            this.txtDatabaseName.TabIndex = 8;
            // 
            // lblDatabasePassword
            // 
            this.lblDatabasePassword.AutoSize = true;
            this.lblDatabasePassword.Location = new System.Drawing.Point(47, 107);
            this.lblDatabasePassword.Name = "lblDatabasePassword";
            this.lblDatabasePassword.Size = new System.Drawing.Size(50, 13);
            this.lblDatabasePassword.TabIndex = 7;
            this.lblDatabasePassword.Text = "Passwort";
            // 
            // txtDatabasePassword
            // 
            this.txtDatabasePassword.Location = new System.Drawing.Point(103, 104);
            this.txtDatabasePassword.Name = "txtDatabasePassword";
            this.txtDatabasePassword.Size = new System.Drawing.Size(112, 20);
            this.txtDatabasePassword.TabIndex = 6;
            this.txtDatabasePassword.UseSystemPasswordChar = true;
            // 
            // lblDatabaseUsername
            // 
            this.lblDatabaseUsername.AutoSize = true;
            this.lblDatabaseUsername.Location = new System.Drawing.Point(22, 81);
            this.lblDatabaseUsername.Name = "lblDatabaseUsername";
            this.lblDatabaseUsername.Size = new System.Drawing.Size(75, 13);
            this.lblDatabaseUsername.TabIndex = 5;
            this.lblDatabaseUsername.Text = "Benutzername";
            // 
            // txtDatabaseUsername
            // 
            this.txtDatabaseUsername.Location = new System.Drawing.Point(103, 78);
            this.txtDatabaseUsername.Name = "txtDatabaseUsername";
            this.txtDatabaseUsername.Size = new System.Drawing.Size(112, 20);
            this.txtDatabaseUsername.TabIndex = 4;
            // 
            // lblDatabasePort
            // 
            this.lblDatabasePort.AutoSize = true;
            this.lblDatabasePort.Location = new System.Drawing.Point(71, 55);
            this.lblDatabasePort.Name = "lblDatabasePort";
            this.lblDatabasePort.Size = new System.Drawing.Size(26, 13);
            this.lblDatabasePort.TabIndex = 3;
            this.lblDatabasePort.Text = "Port";
            // 
            // txtDatabasePort
            // 
            this.txtDatabasePort.Location = new System.Drawing.Point(103, 52);
            this.txtDatabasePort.Name = "txtDatabasePort";
            this.txtDatabasePort.Size = new System.Drawing.Size(112, 20);
            this.txtDatabasePort.TabIndex = 2;
            // 
            // btnSaveSettings
            // 
            this.btnSaveSettings.Image = global::clanify_analyzer_client.Properties.Resources.icon_save;
            this.btnSaveSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSaveSettings.Location = new System.Drawing.Point(12, 181);
            this.btnSaveSettings.Name = "btnSaveSettings";
            this.btnSaveSettings.Size = new System.Drawing.Size(82, 23);
            this.btnSaveSettings.TabIndex = 3;
            this.btnSaveSettings.Text = "Speichern";
            this.btnSaveSettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSaveSettings.UseVisualStyleBackColor = true;
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(242, 215);
            this.Controls.Add(this.btnSaveSettings);
            this.Controls.Add(this.grpDatabaseSettings);
            this.Name = "frmSettings";
            this.ShowIcon = false;
            this.Text = "Einstellungen";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.grpDatabaseSettings.ResumeLayout(false);
            this.grpDatabaseSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtDatabaseServer;
        private System.Windows.Forms.Label lblDatabaseServer;
        private System.Windows.Forms.GroupBox grpDatabaseSettings;
        private System.Windows.Forms.Label lblDatabasePassword;
        private System.Windows.Forms.TextBox txtDatabasePassword;
        private System.Windows.Forms.Label lblDatabaseUsername;
        private System.Windows.Forms.TextBox txtDatabaseUsername;
        private System.Windows.Forms.Label lblDatabasePort;
        private System.Windows.Forms.TextBox txtDatabasePort;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.Label lblDatabaseName;
        private System.Windows.Forms.TextBox txtDatabaseName;
    }
}