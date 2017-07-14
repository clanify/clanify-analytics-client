using clanify_analyzer_client.Database;
using clanify_analyzer_client.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace clanify_analyzer_client
{
    public partial class frmClient : Form
    {
        private MySqlConnection dbConnection = null;

        private DataRow drMatch = null;
        private DataTable dtFrags = null;
        private DataTable dtDamage = null;
        private DataTable dtPlayers = null;
        private DataTable dtTeams = null;
        private DataTable dtMatchPlayers = null;

        public frmClient()
        {
            InitializeComponent();
        }

        //function to intialize the database connection.
        private void initDatabaseConnection()
        {
            string dbServer = "";
            int dbPort = 3306;
            string dbUsername = "";
            string dbPassword = "";
            string dbName = "";

            //check if the database server is available.
            if (Properties.Settings.Default["database_server"].ToString().Trim() == "")
            {
                dbConnection = null;
                return;
            }
            else
            {
                dbServer = Properties.Settings.Default["database_server"].ToString().Trim();
            }

            //check if the database port is available.
            if (Properties.Settings.Default["database_port"].ToString().Trim() == "")
            {
                dbConnection = null;
                return;
            }
            else
            {
                int port = 0;

                if (int.TryParse(Properties.Settings.Default["database_port"].ToString().Trim(), out port))
                {
                    dbPort = port;
                }
                else
                {
                    dbConnection = null;
                    return;
                }
            }


            //check if the username is available.
            if (Properties.Settings.Default["database_username"].ToString().Trim() == "")
            {
                dbConnection = null;
                return;
            }
            else
            {
                dbUsername = Properties.Settings.Default["database_username"].ToString().Trim();
            }

            //check if the password is available.
            if (Properties.Settings.Default["database_password"].ToString().Trim() == "")
            {
                dbConnection = null;
                return;
            }
            else
            {
                dbPassword = Properties.Settings.Default["database_password"].ToString().Trim();
            }

            //check if the database name is available.
            if (Properties.Settings.Default["database_name"].ToString().Trim() == "")
            {
                dbConnection = null;
                return;
            }
            else
            {
                dbName = Properties.Settings.Default["database_name"].ToString().Trim();
            }

            string connStr = "server=" + dbServer + ";uid=" + dbUsername + ";pwd=" + dbPassword + ";database=" + dbName;

            try
            {
                this.dbConnection = new MySqlConnection(connStr);
                this.dbConnection.Open();
            }
            catch
            {
                dbConnection = null;
            }
            finally
            {
                //check  a database connection is available.
                if (this.dbConnection != null)
                {
                    this.dbConnection.Close();
                }
            }
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
            }
            else
            {
                txtSelectDemo.Text = String.Empty;
            }
        }

        //function to fill the combobox with all the available events.
        private void fillCmbEventName()
        {
            //create a list with all supported events.
            Dictionary<int, string> eventList = new Dictionary<int, string>();
            eventList.Add(1, "ESL One Cologne 2017");
            eventList.Add(2, "ESL One New York 2016");

            //clear the list of events to initialize.
            cmbInfoEventName.Items.Clear();

            //init a empty list for all combobox items.
            List<ComboBoxItem> items = new List<ComboBoxItem>();

            //run through all events to initialize the list of events.
            foreach (KeyValuePair<int, string> eventListItem in eventList )
            {
                ComboBoxItem cItem = new ComboBoxItem();
                cItem.Text = eventListItem.Value;
                cItem.Value = eventListItem.Key;
                items.Add(cItem);
            }

            //set the list as datasource.
            cmbInfoEventName.DataSource = items;
            cmbInfoEventName.DisplayMember = "Text";
            cmbInfoEventName.ValueMember = "Value";

            //set the first item as default.
            cmbInfoEventName.SelectedValue = 1;
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

        //handler for the event if a player was killed.
        private void HandlePlayerKilled(object sender, DemoInfo.PlayerKilledEventArgs e)
        {
            //get the demo information.
            DemoInfo.DemoParser demo = (DemoInfo.DemoParser)sender;

            //set the information about the frag to a new row.
            DataRow drNewFrag = this.dtFrags.NewRow();
            drNewFrag["match_id"] = drMatch["id"];
            drNewFrag["round"] = demo.TScore + demo.CTScore + 1;
            drNewFrag["tick"] = demo.CurrentTick;
            drNewFrag["victim_steam_id"] = e.Victim.SteamID;
            drNewFrag["victim_weapon"] = e.Victim.ActiveWeapon.Weapon;
            drNewFrag["victim_position_x"] = e.Victim.Position.X;
            drNewFrag["victim_position_y"] = e.Victim.Position.Y;
            drNewFrag["victim_position_z"] = e.Victim.Position.Z;
            drNewFrag["victim_hp"] = e.Victim.HP;
            drNewFrag["killer_steam_id"] = e.Killer.SteamID;
            drNewFrag["killer_weapon"] = e.Weapon.Weapon;
            drNewFrag["killer_position_x"] = e.Killer.Position.X;
            drNewFrag["killer_position_y"] = e.Killer.Position.Y;
            drNewFrag["killer_position_z"] = e.Killer.Position.Z;
            drNewFrag["killer_hp"] = e.Killer.HP;
            drNewFrag["is_headshot"] = e.Headshot;

            //check if a assister is available.
            if (e.Assister != null)
            {
                drNewFrag["assister_steam_id"] = e.Assister.SteamID;
            }
            
            //set the new row to the table.
            this.dtFrags.Rows.Add(drNewFrag);
        }

        private void HandleRoundEnd(object sender, DemoInfo.RoundEndedEventArgs e)
        {
            //get the demo information.
            DemoInfo.DemoParser demo = (DemoInfo.DemoParser)sender;

            //only the first round should be checked to bind the players.
            if ((demo.CTScore + demo.TScore + 1) != 1)
            {
                return;
            }

            //set the information about the two teams.
            DataRow drNewTeamCT = this.dtTeams.NewRow();
            drNewTeamCT["name"] = demo.CTClanName.ToString();
            this.dtTeams.Rows.Add(drNewTeamCT);
            DataRow drNewTeamT = this.dtTeams.NewRow();
            drNewTeamT["name"] = demo.TClanName;
            this.dtTeams.Rows.Add(drNewTeamT);

            //save the players to the database.
            TableTeams clsTableTeams = new TableTeams(this.dbConnection);
            clsTableTeams.saveTable(this.dtTeams);

            foreach (DemoInfo.Player player in demo.PlayingParticipants)
            {
                //check if the player is a CT or T player.
                if (player.Team == DemoInfo.Team.CounterTerrorist || player.Team == DemoInfo.Team.Terrorist )
                {
                    //set the player information to a new row.
                    DataRow drNewPlayer = this.dtPlayers.NewRow();
                    drNewPlayer["steam_id"] = player.SteamID;
                    drNewPlayer["name"] = player.Name.ToString();

                    //check if the player doesn't exists on the table.
                    if (this.dtPlayers.Select("steam_id = " + player.SteamID).Length == 0)
                    {
                        this.dtPlayers.Rows.Add(drNewPlayer);
                    }

                    //set the player to the match and team.
                    DataRow drMatchPlayer = this.dtMatchPlayers.NewRow();
                    drMatchPlayer["match_id"] = this.drMatch["id"];
                    drMatchPlayer["steam_id"] = player.SteamID;
                    
                    //get the team id of the player to create the relationship between the player and team.
                    if (player.Team == DemoInfo.Team.Terrorist )
                    {
                        drMatchPlayer["team_id"] = this.dtTeams.Select("name = '" + demo.TClanName + "'")[0]["id"];
                    }
                    else
                    {
                        drMatchPlayer["team_id"] = this.dtTeams.Select("name = '" + demo.CTClanName + "'")[0]["id"];
                    }

                    //add the player to the table saving the relationship between match, team and player.
                    this.dtMatchPlayers.Rows.Add(drMatchPlayer);
                }
            }
        }

        //handler for the event if a player was hurt.
        private void HandlePlayerHurt(object sender, DemoInfo.PlayerHurtEventArgs e)
        {
            //get the demo information.
            DemoInfo.DemoParser demo = (DemoInfo.DemoParser)sender;

            //set the information about the damage to a new row.
            DataRow drNewDamage = this.dtDamage.NewRow();
            drNewDamage["match_id"] = drMatch["id"];
            drNewDamage["round"] = demo.TScore + demo.CTScore + 1;
            drNewDamage["tick"] = demo.CurrentTick;
            drNewDamage["victim_steam_id"] = e.Player.SteamID;
            drNewDamage["victim_weapon"] = e.Player.ActiveWeapon.Weapon;
            drNewDamage["victim_position_x"] = e.Player.Position.X;
            drNewDamage["victim_position_y"] = e.Player.Position.Y;
            drNewDamage["victim_position_z"] = e.Player.Position.Z;
            drNewDamage["victim_health"] = e.Health;
            drNewDamage["victim_health_damage"] = e.HealthDamage;
            drNewDamage["victim_armor"] = e.Armor;
            drNewDamage["victim_armor_damage"] = e.ArmorDamage;
            drNewDamage["victim_hp"] = e.Player.HP;
            drNewDamage["hitgroup"] = e.Hitgroup;

            //check if a attacker is available (can be null on own damage).
            if (e.Attacker != null)
            {
                drNewDamage["attacker_steam_id"] = e.Attacker.SteamID;
                drNewDamage["attacker_weapon"] = e.Weapon.Weapon;
                drNewDamage["attacker_position_x"] = e.Attacker.Position.X;
                drNewDamage["attacker_position_y"] = e.Attacker.Position.Y;
                drNewDamage["attacker_position_z"] = e.Attacker.Position.Z;
                drNewDamage["attacker_hp"] = e.Attacker.HP;
            }
            else
            {
                //but we should know if the player hurt himself.
                drNewDamage["attacker_steam_id"] = e.Player.SteamID;
                drNewDamage["attacker_weapon"] = e.Weapon.Weapon;
                drNewDamage["attacker_position_x"] = e.Player.Position.X;
                drNewDamage["attacker_position_y"] = e.Player.Position.Y;
                drNewDamage["attacker_position_z"] = e.Player.Position.Z;
                drNewDamage["attacker_hp"] = e.Player.HP;
            }
            
            //set the new row to the table.
            this.dtDamage.Rows.Add(drNewDamage);
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
            }
            else
            {
                lblHeaderMapName.ForeColor = Color.Black;
                lblInfoMapName.ForeColor = Color.Black;
            }
        }

        //handler for the event if the match started.
        private void HandleMatchStarted(object sender, DemoInfo.MatchStartedEventArgs e)
        {
            //reset the tables for frags and damage.
            //HLTV demos has not match start event, the match starting directly with first tick.
            this.dtDamage.Rows.Clear();
            this.dtFrags.Rows.Clear();
        }

        //click event to parse the demo to get all the information of the demo.
        private void btnReadDemoHeader_Click(object sender, EventArgs e)
        {
            //check if the file is available to parse.
            if (File.Exists(txtSelectDemo.Text))
            {
                //open the demo and parse the header to display on application.
                DemoInfo.DemoParser demo = new DemoInfo.DemoParser(File.OpenRead(txtSelectDemo.Text));

                //bind the header events to their handler.
                demo.HeaderParsed += HandleHeaderParsed;
                
                //parse the header of the demo to get the common information.
                demo.ParseHeader();

                //get the emtpy datatable to save the match information.
                TableMatch clsTableMatch = new TableMatch(this.dbConnection);
                DataTable dtMatch = clsTableMatch.getTableSchema();

                //get the date and time value from the picker and set the merged one.
                DateTime date = dtpInfoMatchDate.Value;
                DateTime time = dtpInfoMatchTime.Value;
                DateTime matchStart = new DateTime(date.Year, date.Month, date.Day, time.Hour, date.Minute, 0);

                //set the information from form to new row.
                DataRow drNewMatch = dtMatch.NewRow();
                drNewMatch["id"] = DBNull.Value;
                drNewMatch["event_id"] = cmbInfoEventName.SelectedValue;
                drNewMatch["match_start"] = matchStart;
                drNewMatch["client_name"] = txtHeaderClientName.Text.ToString();
                drNewMatch["filestamp"] = txtHeaderFilestamp.Text.ToString();
                drNewMatch["game_directory"] = txtHeaderGameDirectory.Text.ToString();
                drNewMatch["map_name"] = cmbInfoMapName.SelectedValue.ToString();
                drNewMatch["network_protocol"] = txtHeaderNetworkProtocol.Text.ToString();
                drNewMatch["playback_frames"] = txtHeaderPlaybackFrames.Text.ToString();
                drNewMatch["playback_ticks"] = txtHeaderPlaybackTicks.Text.ToString();
                drNewMatch["playback_time"] = txtHeaderPlaybackTime.Text.ToString();
                drNewMatch["protocol"] = txtHeaderProtocol.Text.ToString();
                drNewMatch["server_name"] = txtHeaderServerName.Text.ToString();
                drNewMatch["signon_length"] = txtHeaderSignonLength.Text.ToString();

                //save the information to the database.
                drNewMatch = clsTableMatch.saveRowMatch(drNewMatch);
                this.drMatch = drNewMatch;

                //init the tables for the frags information.
                TableFrags clsFrags = new TableFrags(this.dbConnection);
                this.dtFrags = clsFrags.getTableSchema();

                //init the tables for the damage information.
                TableDamage clsDamage = new TableDamage(this.dbConnection);
                this.dtDamage = clsDamage.getTableSchema();

                //init the tables for the player information.
                TablePlayers clsPlayers = new TablePlayers(this.dbConnection);
                this.dtPlayers = clsPlayers.getTableSchema();

                //init the tables for the team information.
                TableTeams clsTeams = new TableTeams(this.dbConnection);
                this.dtTeams = clsTeams.getTableSchema();

                //init the tables for the match players information.
                TableMatchPlayers clsMatchPlayers = new TableMatchPlayers(this.dbConnection);
                this.dtMatchPlayers = clsMatchPlayers.getTableSchema();

                //bind the main demo events to their handler.
                demo.PlayerKilled += HandlePlayerKilled;
                demo.PlayerHurt += HandlePlayerHurt;
                demo.RoundEnd += HandleRoundEnd;
                demo.MatchStarted += HandleMatchStarted;
                                
                //now we can start parsing the whole demo.
                demo.ParseToEnd();
            }
            else
            {
                MessageBox.Show("The demo file isn't available!", "clanify - Analytics Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        //load event to load the controls of the form.
        private void frmClient_Load(object sender, EventArgs e)
        {
            //initialize the list of events and maps.
            this.fillCmbEventName();
            this.fillCmbMapName();

            //set the default date and time to the current day and hour.
            DateTime currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);

            //initialize the date and time picker.
            dtpInfoMatchDate.Value = currentDateTime;
            dtpInfoMatchTime.Value = currentDateTime;

            //init the database connection.
            this.initDatabaseConnection();

            //check and show the connection state.
            checkConnectionDB();
        }

        //event to save the match information in database.
        private void btnSave_Click(object sender, EventArgs e)
        {
            //save the frags to the database.
            TableFrags clsTableFrags = new TableFrags(this.dbConnection);
            clsTableFrags.saveTable(this.dtFrags, (Int64) drMatch["id"]);

            //save the damage to the database.
            TableDamage clsTableDamage = new TableDamage(this.dbConnection);
            clsTableDamage.saveTable(this.dtDamage, (Int64) drMatch["id"]);

            //save the players to the database.
            TablePlayers clsTablePlayers = new TablePlayers(this.dbConnection);
            clsTablePlayers.saveTable(this.dtPlayers);

            //save the match players to the database.
            TableMatchPlayers clsTableMatchPlayers = new TableMatchPlayers(this.dbConnection);
            clsTableMatchPlayers.saveTable(this.dtMatchPlayers, (Int64) drMatch["id"]);
        }

        //event to open the settings.
        private void btnSettings_Click(object sender, EventArgs e)
        {
            //open the settings to configure the application.
            frmSettings frmSettings = new frmSettings();
            frmSettings.ShowDialog();

            //init the database connection.
            this.initDatabaseConnection();

            //check and show the connection state.
            checkConnectionDB();
        }

        //check the database connection and show state.
        private void checkConnectionDB()
        {
            //check if the connection is successfull.
            if (this.dbConnection != null)
            {
                tslblConnState.BackColor = ControlPaint.Light(Color.Green);
                tslblConnState.Text = this.dbConnection.Database.ToString();
            }
            else
            {
                tslblConnState.BackColor = ControlPaint.Light(Color.Red);
                tslblConnState.Text = " --- ";
            }
        }
    }
}