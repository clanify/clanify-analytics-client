using System;
using System.Windows.Forms;

namespace clanify_analyzer_client
{
    public partial class frmSettings : Form
    {
        public frmSettings()
        {
            InitializeComponent();
        }

        //event to save the settings on the form to the application.
        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            //check if the form values are valid.
            if (isValid())
            {
                int port = 0;

                //try to parse the port of the server.
                if (int.TryParse(txtDatabasePort.Text.ToString().Trim(), out port) == true)
                {
                    Properties.Settings.Default["database_port"] = port;
                }
                else
                {
                    MessageBox.Show("Der Port konnte nicht gespeichert werden!", "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                //save the settings to the application.
                Properties.Settings.Default["database_server"] = txtDatabaseServer.Text.ToString().Trim();
                Properties.Settings.Default["database_username"] = txtDatabaseUsername.Text.ToString().Trim();
                Properties.Settings.Default["database_password"] = txtDatabasePassword.Text.ToString().Trim();
                Properties.Settings.Default["database_name"] = txtDatabaseName.Text.ToString().Trim();
                Properties.Settings.Default.Save();

                //close the window.
                this.Close();
            }
        }

        //function to check if all form values are valid to save.
        private bool isValid()
        {
            int port;

            //check if the database server is available.
            if (txtDatabaseServer.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Es wurde kein Server-Name angegeben!", "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDatabaseServer.Focus();
                return false;
            }

            //check if the database port is available.
            if (txtDatabasePort.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Es wurde kein Port angegeben!", "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDatabasePort.Focus();
                return false;
            }

            //check if the database port is numeric.
            if (int.TryParse(txtDatabasePort.Text.ToString().Trim(), out port) == false)
            {
                MessageBox.Show("Es wurde kein gültiger Port angegeben!", "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDatabasePort.Focus();
                return false;
            }

            //check if the username is available.
            if (txtDatabaseUsername.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Es wurde kein Benutzername angegeben!", "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDatabaseUsername.Focus();
                return false;
            }

            //check if the password is available.
            if (txtDatabasePassword.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Es wurde kein Passwort angegeben!", "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDatabasePassword.Focus();
                return false;
            }

            //check if the database name is available.
            if (txtDatabaseName.Text.ToString().Trim() == "")
            {
                MessageBox.Show("Es wurde kein Datenbank-Name angegeben!", "Einstellungen", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDatabaseName.Focus();
                return false;
            }

            //all seem OK.
            return true;
        }

        //event to load the settings on form load.
        private void frmSettings_Load(object sender, EventArgs e)
        {
            //load the settings of the application to the form.
            txtDatabaseServer.Text = Properties.Settings.Default["database_server"].ToString().Trim();
            txtDatabasePort.Text = Properties.Settings.Default["database_port"].ToString().Trim();
            txtDatabaseUsername.Text = Properties.Settings.Default["database_username"].ToString().Trim();
            txtDatabasePassword.Text = Properties.Settings.Default["database_password"].ToString().Trim();
            txtDatabaseName.Text = Properties.Settings.Default["database_name"].ToString().Trim();
        }
    }
}
