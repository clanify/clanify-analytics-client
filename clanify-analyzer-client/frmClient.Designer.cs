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
            this.grpDemoInfo = new System.Windows.Forms.GroupBox();
            this.btnImportDemo = new System.Windows.Forms.Button();
            this.dtpMatchDate = new System.Windows.Forms.DateTimePicker();
            this.lblMatchDate = new System.Windows.Forms.Label();
            this.cmbEvent = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpDemoInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSelectDemo
            // 
            this.txtSelectDemo.Enabled = false;
            this.txtSelectDemo.Location = new System.Drawing.Point(12, 12);
            this.txtSelectDemo.Name = "txtSelectDemo";
            this.txtSelectDemo.ReadOnly = true;
            this.txtSelectDemo.Size = new System.Drawing.Size(364, 20);
            this.txtSelectDemo.TabIndex = 0;
            // 
            // btnSelectDemo
            // 
            this.btnSelectDemo.Location = new System.Drawing.Point(382, 10);
            this.btnSelectDemo.Name = "btnSelectDemo";
            this.btnSelectDemo.Size = new System.Drawing.Size(68, 23);
            this.btnSelectDemo.TabIndex = 1;
            this.btnSelectDemo.Text = "Demo...";
            this.btnSelectDemo.UseVisualStyleBackColor = true;
            this.btnSelectDemo.Click += new System.EventHandler(this.btnSelectDemo_Click);
            // 
            // grpDemoInfo
            // 
            this.grpDemoInfo.Controls.Add(this.label1);
            this.grpDemoInfo.Controls.Add(this.cmbEvent);
            this.grpDemoInfo.Controls.Add(this.lblMatchDate);
            this.grpDemoInfo.Controls.Add(this.dtpMatchDate);
            this.grpDemoInfo.Location = new System.Drawing.Point(12, 38);
            this.grpDemoInfo.Name = "grpDemoInfo";
            this.grpDemoInfo.Size = new System.Drawing.Size(438, 182);
            this.grpDemoInfo.TabIndex = 2;
            this.grpDemoInfo.TabStop = false;
            this.grpDemoInfo.Text = "Demo-Informationen";
            // 
            // btnImportDemo
            // 
            this.btnImportDemo.Location = new System.Drawing.Point(12, 226);
            this.btnImportDemo.Name = "btnImportDemo";
            this.btnImportDemo.Size = new System.Drawing.Size(75, 23);
            this.btnImportDemo.TabIndex = 3;
            this.btnImportDemo.Text = "Import";
            this.btnImportDemo.UseVisualStyleBackColor = true;
            this.btnImportDemo.Click += new System.EventHandler(this.btnImportDemo_Click);
            // 
            // dtpMatchDate
            // 
            this.dtpMatchDate.CustomFormat = "dd-MM-yyyy";
            this.dtpMatchDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpMatchDate.Location = new System.Drawing.Point(50, 33);
            this.dtpMatchDate.MaxDate = new System.DateTime(2020, 12, 31, 0, 0, 0, 0);
            this.dtpMatchDate.Name = "dtpMatchDate";
            this.dtpMatchDate.Size = new System.Drawing.Size(97, 20);
            this.dtpMatchDate.TabIndex = 0;
            this.dtpMatchDate.Value = new System.DateTime(2017, 7, 4, 0, 0, 0, 0);
            // 
            // lblMatchDate
            // 
            this.lblMatchDate.AutoSize = true;
            this.lblMatchDate.Location = new System.Drawing.Point(6, 37);
            this.lblMatchDate.Name = "lblMatchDate";
            this.lblMatchDate.Size = new System.Drawing.Size(38, 13);
            this.lblMatchDate.TabIndex = 1;
            this.lblMatchDate.Text = "Datum";
            // 
            // cmbEvent
            // 
            this.cmbEvent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEvent.FormattingEnabled = true;
            this.cmbEvent.Items.AddRange(new object[] {
            "ESL One New York 2016",
            "ESL One Cologne 2017"});
            this.cmbEvent.Location = new System.Drawing.Point(214, 34);
            this.cmbEvent.Name = "cmbEvent";
            this.cmbEvent.Size = new System.Drawing.Size(218, 21);
            this.cmbEvent.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(173, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Event";
            // 
            // frmClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 261);
            this.Controls.Add(this.btnImportDemo);
            this.Controls.Add(this.grpDemoInfo);
            this.Controls.Add(this.btnSelectDemo);
            this.Controls.Add(this.txtSelectDemo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmClient";
            this.ShowIcon = false;
            this.Text = "clanify - Analyzer Client";
            this.grpDemoInfo.ResumeLayout(false);
            this.grpDemoInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSelectDemo;
        private System.Windows.Forms.Button btnSelectDemo;
        private System.Windows.Forms.GroupBox grpDemoInfo;
        private System.Windows.Forms.DateTimePicker dtpMatchDate;
        private System.Windows.Forms.Button btnImportDemo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbEvent;
        private System.Windows.Forms.Label lblMatchDate;
    }
}

