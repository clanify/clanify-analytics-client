namespace clanify_analyzer_client
{
    partial class frmMain
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
            this.SuspendLayout();
            // 
            // txtSelectDemo
            // 
            this.txtSelectDemo.Location = new System.Drawing.Point(12, 12);
            this.txtSelectDemo.Name = "txtSelectDemo";
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
            // 
            // grpDemoInfo
            // 
            this.grpDemoInfo.Location = new System.Drawing.Point(12, 38);
            this.grpDemoInfo.Name = "grpDemoInfo";
            this.grpDemoInfo.Size = new System.Drawing.Size(438, 211);
            this.grpDemoInfo.TabIndex = 2;
            this.grpDemoInfo.TabStop = false;
            this.grpDemoInfo.Text = "Demo-Informationen";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 261);
            this.Controls.Add(this.grpDemoInfo);
            this.Controls.Add(this.btnSelectDemo);
            this.Controls.Add(this.txtSelectDemo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "clanify - Analyzer Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSelectDemo;
        private System.Windows.Forms.Button btnSelectDemo;
        private System.Windows.Forms.GroupBox grpDemoInfo;
    }
}

