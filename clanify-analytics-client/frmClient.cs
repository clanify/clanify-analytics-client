using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace clanify_analyzer_client
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();

            //TODO - initialize the list of events from database.
            //TODO - initialize the list of maps.
        }

        //click event to get the demo file from filesystem and set to the textbox.
        private void btnSelectDemo_Click(object sender, EventArgs e)
        {
            //create and show a OpenFileDialog to select a demo file.
            OpenFileDialog dlgDemoFile = new OpenFileDialog();
            dlgDemoFile.AddExtension = false;
            dlgDemoFile.CheckFileExists = true;
            dlgDemoFile.CheckPathExists = true;
            dlgDemoFile.DefaultExt = ".dem";
            dlgDemoFile.Filter = "demo file (*.dem)|*.dem";
            dlgDemoFile.InitialDirectory = Application.StartupPath;
            dlgDemoFile.Multiselect = false;
            dlgDemoFile.ShowHelp = false;
            dlgDemoFile.ShowReadOnly = false;
            dlgDemoFile.SupportMultiDottedExtensions = false;
            dlgDemoFile.Title = "Demo-Datei auswählen...";
            dlgDemoFile.ValidateNames = true;

            //check if the Dialog was closed by OK.
            if (dlgDemoFile.ShowDialog() == DialogResult.OK )
            {
                txtSelectDemo.Text = dlgDemoFile.FileName;
            } else
            {
                txtSelectDemo.Text = String.Empty;
            }
        }

        //click event to parse the demo to get all the information for export to database.
        private void btnImportDemo_Click(object sender, EventArgs e)
        {
            //check if the file is available to parse.
            if (File.Exists(txtSelectDemo.Text))
            {
                //open the demo and parse the header to display on application.
                DemoInfo.DemoParser demo = new DemoInfo.DemoParser(File.OpenRead(txtSelectDemo.Text));
                demo.ParseHeader();

                //TODO - parse the header and display the information on application.
            }  else
            {
                MessageBox.Show("The demo file isn't available!", "clanify - Analyzer Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }        
        }
    }
}