using clanify_analyzer_client.Helper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace clanify_analyzer_client
{
    public partial class frmClient : Form
    {
        public frmClient()
        {
            InitializeComponent();

            //TODO - initialize the list of events from database.

            //initialize the list of maps.
            fillCmbMapName();
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

                //bind the events to the functions.
                demo.HeaderParsed += HandleHeaderParsed;

                //parse the header of the demo to get the common information.
                demo.ParseHeader();
            }  else
            {
                MessageBox.Show("The demo file isn't available!", "clanify - Analytics Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }        
        }

        //function to fill the combobox with all the available maps.
        private void fillCmbMapName()
        {
            //create a list with all supported maps.
            Dictionary<string, string> mapList = new Dictionary<string, string>();
            mapList.Add("de_cbble", "Cobblestone");
            mapList.Add("de_inferno", "Inferno");
            mapList.Add("de_cache", "Cache");
            mapList.Add("de_mirage", "Mirage");
            mapList.Add("de_overpass", "Overpass");
            mapList.Add("de_nuke", "Nuke");
            mapList.Add("de_train", "Train");
            mapList.Add("de_dust2", "Dust2");

            //clear the list of maps to initialize.
            cmbInfoMapName.Items.Clear();

            //init a empty list for all combobox items.
            List<ComboBoxItem> items = new List<ComboBoxItem>();
            
            //run through all maps to initialize the list of maps.
            foreach (KeyValuePair<string, string> mapListItem in mapList)
            {
                ComboBoxItem cItem = new ComboBoxItem();
                cItem.Text = mapListItem.Value;
                cItem.Value = mapListItem.Key;
                items.Add(cItem);
            }

            //set the list as datasource.
            cmbInfoMapName.DataSource = items;
            cmbInfoMapName.DisplayMember = "Text";
            cmbInfoMapName.ValueMember = "Value";

            //set the first item as default.
            cmbInfoMapName.SelectedValue = "de_cbble";
        }

        //handler for the event if the header was parsed.
        private void HandleHeaderParsed(object sender, DemoInfo.HeaderParsedEventArgs e)
        {
            //set the information to the header form.
            txtHeaderClientName.Text = e.Header.ClientName.ToString();
            txtHeaderFilestamp.Text = e.Header.Filestamp.ToString();
            txtHeaderGameDirectory.Text = e.Header.GameDirectory.ToString();
            txtHeaderMapName.Text = e.Header.MapName.ToString();
            txtHeaderNetworkProtocol.Text = e.Header.NetworkProtocol.ToString();
            txtHeaderPlaybackFrames.Text = e.Header.PlaybackFrames.ToString();
            txtHeaderPlaybackTicks.Text = e.Header.PlaybackTicks.ToString();
            txtHeaderPlaybackTime.Text = e.Header.PlaybackTime.ToString();
            txtHeaderProtocol.Text = e.Header.Protocol.ToString();
            txtHeaderServerName.Text = e.Header.ServerName.ToString();
            txtHeaderSignonLength.Text = e.Header.SignonLength.ToString();

            //set the map name on the info form.
            cmbInfoMapName.SelectedValue = txtHeaderMapName.Text.ToString();
            
            //check if the map name could be set on the info form.
            if (cmbInfoMapName.SelectedValue is null)
            {
                lblHeaderMapName.ForeColor = Color.Red;
                lblInfoMapName.ForeColor = Color.Red;
            } else
            {
                lblHeaderMapName.ForeColor = Color.Black;
                lblInfoMapName.ForeColor = Color.Black;
            }
        }
    }
}