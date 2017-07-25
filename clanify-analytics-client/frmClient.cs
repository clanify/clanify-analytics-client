using clanify_analyzer_client.Database;
using clanify_analyzer_client.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace clanify_analyzer_client
{
    public partial class frmClient : Form
    {
        /// <summary>
        /// The database connection to work with the database.
        /// </summary>
        private MySqlConnection dbConnection = null;

        /// <summary>
        /// some tables and rows to store the data of the demo file.
        /// </summary>
        private DataRow drMatch = null;
        private DataRow drRound = null;
        private DataTable dtFrags = null;
        private DataTable dtDamage = null;
        private DataTable dtPlayers = null;
        private DataTable dtTeams = null;
        private DataTable dtMatchPlayers = null;
        private DataTable dtWeaponEvents = null;
        private DataTable dtRounds = null;

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
                //check if the database port is available.
                if (int.TryParse(Properties.Settings.Default["database_port"].ToString().Trim(), out int port))
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
            OpenFileDialog dlgDemoFile = new OpenFileDialog()
            {
                AddExtension = false,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".dem",
                Filter = "demo file (*.dem)|*.dem",
                InitialDirectory = Application.StartupPath,
                Multiselect = false,
                ShowHelp = false,
                ShowReadOnly = false,
                SupportMultiDottedExtensions = false,
                Title = "Demo-Datei auswählen...",
                ValidateNames = true
            };

            //check if the Dialog was closed by OK.
            if (dlgDemoFile.ShowDialog() == DialogResult.OK )
            {
                txtSelectDemo.Text = dlgDemoFile.FileName;
                lblLoadedInfo.BackColor = ControlPaint.Light(Color.Red);
                lblSavedInfo.BackColor = ControlPaint.Light(Color.Red);
                btnSave.Enabled = false;
            }
            else
            {
                txtSelectDemo.Text = String.Empty;
            }
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
            drNewFrag["killer_weapon"] = e.Weapon.Weapon;

            //check if a killer is available (some players jumps to deep or can't handle weapons :D).
            if (e.Killer != null)
            {
                drNewFrag["killer_steam_id"] = e.Killer.SteamID;            
                drNewFrag["killer_position_x"] = e.Killer.Position.X;
                drNewFrag["killer_position_y"] = e.Killer.Position.Y;
                drNewFrag["killer_position_z"] = e.Killer.Position.Z;
                drNewFrag["killer_hp"] = e.Killer.HP;
                drNewFrag["is_headshot"] = e.Headshot;
            }
            
            //check if a assister is available.
            if (e.Assister != null)
            {
                drNewFrag["assister_steam_id"] = e.Assister.SteamID;
            }
            
            //set the new row to the table.
            this.dtFrags.Rows.Add(drNewFrag);
        }


        /// ----------------------------------------------------------------------------------------------------
        /// -- Funktionen um die Oberfläche und die Kontrollen initialisieren zu können.
        /// ----------------------------------------------------------------------------------------------------

        /// <summary>
        /// Funktion um die ComboBox mit den Events aus der Datenbank zu füllen.
        /// </summary>
        private void FillCmbEventName()
        {
            //Die Liste der Events leeren um durch die Datenbank zu initialisieren.
            cmbInfoEventName.Items.Clear();

            //Elemente für die ComboBox leeren und initialisieren.
            List<ComboBoxItem> items = new List<ComboBoxItem>();

            //Prüfen ob eine Datenbank-Verbindung vorhanden ist.
            if (this.dbConnection != null)
            {
                //Prüfen ob die Verbindung zur Datenbank offen ist.
                if (this.dbConnection.State != ConnectionState.Open)
                {
                    this.dbConnection.Open();
                }

                //Erstellen der SELECT-Abfrage um die Events aus der Datenbank zu ermitteln.
                string sqlSelect = "SELECT `id`, `name` FROM `events` ORDER BY `name`";

                //Die SELECT-Abfrage als Befehl setzen und ausführen.
                MySqlCommand cmdSelect = this.dbConnection.CreateCommand();
                cmdSelect.CommandText = sqlSelect;
                MySqlDataReader reader = cmdSelect.ExecuteReader();

                //Initialisieren der Event-Liste (um das Egebnis der Abfrage zu speichern).
                Dictionary<int, string> eventList = new Dictionary<int, string>();

                //Alle Einträge aus der Datenbank lesen.
                while (reader.Read())
                {
                    eventList.Add(Convert.ToInt32(reader["id"]), Convert.ToString(reader["name"]));
                }

                //Ergebnis der Abfrage zurücksetzen und Speicher wieder freigeben.
                reader.Close();
                reader.Dispose();
                reader = null;

                //Falls die Verbindung noch offen ist schließen wir diese wieder.
                if (this.dbConnection.State == ConnectionState.Open)
                {
                    this.dbConnection.Close();
                }

                //Alle Events in die Liste der ComboBox-Elemente setzen.
                foreach (KeyValuePair<int, string> eventListItem in eventList)
                {
                    //Das neue ComboBox-Element erstellen.
                    ComboBoxItem cItem = new ComboBoxItem()
                    {
                        Text = eventListItem.Value,
                        Value = eventListItem.Key
                    };

                    //Das neu erstellte ComboBox-Element in die Liste setzen.
                    items.Add(cItem);
                }
            }

            //Liste in die ComboBox setzen (falls keine Verbindung bleibt diese leer).
            cmbInfoEventName.DataSource = items;
            cmbInfoEventName.DisplayMember = "Text";
            cmbInfoEventName.ValueMember = "Value";
            cmbInfoEventName.SelectedValue = 1;
        }

        /// <summary>
        /// Funktion um die ComboBox mit den Karten / Maps aus der Datenbank zu füllen.
        /// </summary>
        private void FillCmbMapName()
        {
            //Die Liste der Karten / Maps leeren um durch die Datenbank zu initialisieren.
            cmbInfoMapName.Items.Clear();

            //Elemente für die ComboBox leeren und initialisieren.
            List<ComboBoxItem> items = new List<ComboBoxItem>();

            //Prüfen ob eine Datenbank-Verbindung vorhanden ist.
            if (this.dbConnection != null)
            {
                //Prüfen ob die Verbindung zur Datenbank offen ist.
                if (this.dbConnection.State != ConnectionState.Open)
                {
                    this.dbConnection.Open();
                }

                //Erstellen der SELECT-Abfrage um die Karten / Maps aus der Datenbank zu ermitteln.
                string sqlSelect = "SELECT `name`, `title` FROM `maps` ORDER BY `title`";

                //Die SELECT-Abfrage als Befehl setzen und ausführen.
                MySqlCommand cmdSelect = this.dbConnection.CreateCommand();
                cmdSelect.CommandText = sqlSelect;
                MySqlDataReader reader = cmdSelect.ExecuteReader();

                //Initialisieren der Karten-Liste (um das Egebnis der Abfrage zu speichern).
                Dictionary<string, string> mapList = new Dictionary<string, string>();

                //Alle Einträge aus der Datenbank lesen.
                while (reader.Read())
                {
                    mapList.Add(Convert.ToString(reader["name"]), Convert.ToString(reader["title"]));
                }

                //Ergebnis der Abfrage zurücksetzen und Speicher wieder freigeben.
                reader.Close();
                reader.Dispose();
                reader = null;

                //Falls die Verbindung noch offen ist schließen wir diese wieder.
                if (this.dbConnection.State == ConnectionState.Open)
                {
                    this.dbConnection.Close();
                }

                //Alle Karten / Maps in die Liste der ComboBox-Elemente setzen.
                foreach (KeyValuePair<string, string> mapListItem in mapList)
                {
                    //Das neue ComboBox-Element erstellen.
                    ComboBoxItem cItem = new ComboBoxItem()
                    {
                        Text = mapListItem.Value,
                        Value = mapListItem.Key
                    };

                    //Das neu erstellte ComboBox-Element in die Liste setzen.
                    items.Add(cItem);
                }
            }

            //Liste in die ComboBox setzen (falls keine Verbindung bleibt diese leer).
            cmbInfoMapName.DataSource = items;
            cmbInfoMapName.DisplayMember = "Text";
            cmbInfoMapName.ValueMember = "Value";
            cmbInfoMapName.SelectedValue = "de_cbble";
        }

        /// ----------------------------------------------------------------------------------------------------
        /// -- Events und Funktionen welche den Runden-Beginn verarbeiten.
        /// ----------------------------------------------------------------------------------------------------

        /// <summary>
        /// Event welches aufgerufen wird, wenn die Runde gestartet wurde.
        /// </summary>
        private void HandleRoundStart(object sender, DemoInfo.RoundStartedEventArgs e)
        {
            //Informationen der Demo ermitteln.
            DemoInfo.DemoParser demo = (DemoInfo.DemoParser)sender;

            //Die Informationen der Runde in eine neue Zeile setzen und bis zum Ende der Runde merken.
            this.drRound = this.dtRounds.NewRow();
            this.drRound["match_id"] = drMatch["id"];
            this.drRound["tick_start"] = demo.CurrentTick;
        }

        /// ----------------------------------------------------------------------------------------------------
        /// -- Events und Funktionen welche das Runden-Ende verarbeiten.
        /// ----------------------------------------------------------------------------------------------------

        /// <summary>
        /// Event welches aufgerufen wird, wenn die Runde zu Ende ist. Nach diesem Event
        /// wird auch das Event "HandleRoundOfficialyEnd" aufgerufen. Dort machen wir dann den Abschluss.
        /// </summary>
        private void HandleRoundEnd(object sender, DemoInfo.RoundEndedEventArgs e)
        {
            //Informationen der Demo ermitteln.
            DemoInfo.DemoParser demo = (DemoInfo.DemoParser)sender;

            //Status ermitteln ob wir uns in der ersten Runde befinden. Es
            //muss sich dabei um die erste Runde des gesamten Spiels handeln.
            //Durch mehrteilige Demos können auch mehrere erste Runden auftreten.
            bool overallFirstRound = (!chkAppend.Checked) && ((demo.CTScore + demo.TScore + 1) == 1);

            //In der ersten Runde des Spiels kümmern wir uns um die Teams.
            //In der Regel sollten hier alle am Start sein und keiner fehlen. Falls doch muss
            //dass hier noch intelligenter werden. Evtl. auch beim Bind-Event allerdings
            //könnte dort auch ein ungebetener Gast auf den Server kommen.
            if (overallFirstRound)
            {
                //Informationen der zwei Teams setzen.
                DataRow drNewTeamCT = this.dtTeams.NewRow();
                drNewTeamCT["name"] = demo.CTClanName.ToString();
                this.dtTeams.Rows.Add(drNewTeamCT);
                DataRow drNewTeamT = this.dtTeams.NewRow();
                drNewTeamT["name"] = demo.TClanName;
                this.dtTeams.Rows.Add(drNewTeamT);

                //Wir speichern die Teams direkt in der Datenbank. Diese Informationen
                //kann man immer brauchen, daher müssen wir hier nicht sparen.
                TableTeams clsTableTeams = new TableTeams(this.dbConnection);
                clsTableTeams.saveTable(this.dtTeams);

                //Nun ermitteln wir auch die Spieler und ordnen diese den
                //Teams zu. Hier gilt dass Gleiche wie bei den Teams (mehr Info). 
                foreach (DemoInfo.Player player in demo.PlayingParticipants)
                {
                    //Der Spectator interessiert uns nicht. (OK, er könnte gewinnen - interessiert nur keinen).
                    if (player.Team == DemoInfo.Team.CounterTerrorist || player.Team == DemoInfo.Team.Terrorist)
                    {
                        //Informationen des Spielers in eine neue Zeile setzen.
                        DataRow drNewPlayer = this.dtPlayers.NewRow();
                        drNewPlayer["steam_id"] = player.SteamID;
                        drNewPlayer["name"] = player.Name.ToString();

                        //Den Spieler nur hinzufügen, wenn dieser noch nicht existiert.
                        if (this.dtPlayers.Select("steam_id = " + (Int64) player.SteamID).Length == 0)
                        {
                            this.dtPlayers.Rows.Add(drNewPlayer);
                        }

                        //Den Spieler nun auch als Teilnehmer dieses Spiels speichern.
                        DataRow drMatchPlayer = this.dtMatchPlayers.NewRow();
                        drMatchPlayer["match_id"] = this.drMatch["id"];
                        drMatchPlayer["steam_id"] = player.SteamID;

                        //Die Verbindung zwischen dem Spieler und Team wird nachfolgend gesetzt.
                        if (player.Team == DemoInfo.Team.Terrorist)
                        {
                            drMatchPlayer["team_id"] = this.dtTeams.Select("name = '" + demo.TClanName + "'")[0]["id"];
                            this.dtMatchPlayers.Rows.Add(drMatchPlayer);
                        }
                        else
                        {
                            drMatchPlayer["team_id"] = this.dtTeams.Select("name = '" + demo.CTClanName + "'")[0]["id"];
                            this.dtMatchPlayers.Rows.Add(drMatchPlayer);
                        }
                    }
                }
            }

            //Da die Runde evtl. zurückgesetzt werden könnte brechen wir hier ab falls dies der Fall ist.
            if (this.drRound == null)
            {
                return;
            }

            //Prüfen wer die Runde gewonnen hat. Normalerweise kann das nur T und CT sein. Aber aus
            //irgendeinem Grund kann auch der Spectator die Runde gewinnen. In einem solchen Fall verwerfen wir die Runde.
            //Wir ermitteln die Nummer der Runde auf Basis der bereits vorhandenen, das offizielle
            //Ende überschreibt die Runde nochmals. Am Ende des Spiels muss dies aber nicht der Fall sein!
            if (e.Winner == DemoInfo.Team.CounterTerrorist)
            {
                this.drRound["winning_team"] = "CT";
                this.drRound["winning_team_id"] = (this.dtTeams.Select("name = '" + demo.CTClanName + "'"))[0]["id"];
                this.drRound["tick_end"] = demo.CurrentTick;
                this.drRound["round"] = maxRoundFromRounds() + 1;
            }
            else if (e.Winner == DemoInfo.Team.Terrorist)
            {
                this.drRound["winning_team"] = "T";
                this.drRound["winning_team_id"] = (this.dtTeams.Select("name = '" + demo.TClanName + "'"))[0]["id"];
                this.drRound["tick_end"] = demo.CurrentTick;
                this.drRound["round"] = maxRoundFromRounds() + 1;
            }
            else if (e.Winner == DemoInfo.Team.Spectate)
            {
                //Der Spectator gewinnt die Runde, also verwerfen.
                this.drRound = null;
            }
        }

        /// <summary>
        /// Event welches aufgerufen wird, wenn die Runde offiziell beendet ist.
        /// </summary>
        private void HandleRoundOfficialEnd(object sender, DemoInfo.RoundOfficiallyEndedEventArgs e)
        {
            //Da auch der Spectator die Runde gewinnen kann und wir dann die Runde zurücksetzen
            //könnte es hier keine Runde geben! Auch andere Abbrüche könnten noch folgen so dass
            //wir hier einfach auf Nummer sicher gehen, ob die Runde vorhanden ist.
            if (this.drRound == null)
            {
                return;
            }

            //Informationen der Demo ermitteln.
            DemoInfo.DemoParser demo = (DemoInfo.DemoParser)sender;

            //Wir ermitteln ob die Runde bereits vorhanden ist, falls diese vorhanden ist,
            //wird diese entfernt.
            DataRow[] drRoundsExist = this.dtRounds.Select("round = " + (int) (demo.CTScore + demo.TScore));

            //Alle Runden durchlaufen und in der Tabelle löschen. Da wir hier wissen welche Runde
            //zu Ende ist, können wir diese löschen falls diese nochmal auftaucht. So können wir
            //Spielabbrüche und Wiederholungsrunden erkennen und fehlerhafte und ungültige Runden ignorieren.
            //Die Erkennung muss definitiv besser werden, aber für's erste sollte das reichen da die DemoInfo
            //momentan einfach nicht mehr hergibt (oder ich dass nicht weiß :)).
            if (drRoundsExist.Length == 1)
            {
                this.dtRounds.Rows.Remove(drRoundsExist[0]);
            }
            
            //Wir setzen hier nochmals die Runde da nun der Score richtig dargestellt ist.
            //Zusätzlich setzen wir den Tick an welchem die Runde endet und setzen die Zeile in die Tabelle.
            //Die Ticks unterscheiden sich leicht von der Demo (Shift + F2 oder demoui). Da
            //die Aktionen (frags, damage) jedoch nur danach passieren können diese Ticks schon verwendet werden.
            drRound["tick_end"] = demo.CurrentTick;
            drRound["round"] = demo.CTScore + demo.TScore;
            this.dtRounds.Rows.Add(drRound);

            //Zur Sicherheit setzen wir hier die Runde wieder zurück. Nach diesem Punkt 
            //brauchen wir die Zeile nicht mehr. Der Rundenbeginn sollte die Runde neu
            //aufbauen so dass keine alten Daten erhalten bleiben sollten.
            this.drRound = null;
        }
       
        /// <summary>
        /// Funktion um die letzte vorhandene Runde aus der Tabelle der Runden zu ermitteln.
        /// </summary>
        /// <returns>Die letzte vorhandene Runde in der Tabelle der Runden.</returns>
        private int maxRoundFromRounds()
        {
            //Zurücksetzen der Runde und der temporären Runde.
            int round = 0;
            int tmp_round = 0;

            //Alle Runden durchlaufen um die höchste Runden-Nummer zu ermitteln.
            foreach (DataRow drRound in this.dtRounds.Rows)
            {
                //Aus der Zeile die Runden-Nummer ermitteln.
                int.TryParse(drRound["round"].ToString(), out tmp_round);

                //Falls wir eine höhere Runden-Nummer finden merken wir uns diese.
                if (tmp_round > round)
                {
                    round = tmp_round;
                }
            }

            //Höchste Runden-Nummer wurde gefunden.
            return round;
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
            //but not on the append mode, the match is starting again in multipart demos.
            if (chkAppend.Checked == false)
            {
                this.dtDamage.Rows.Clear();
                this.dtFrags.Rows.Clear();
                this.dtWeaponEvents.Rows.Clear();
                this.dtRounds.Rows.Clear();
            }
        }
        
        //handler for the event if a weapon is fired.
        private void HandleWeaponFired(object sender, DemoInfo.WeaponFiredEventArgs e)
        {
            //get the demo information.
            DemoInfo.DemoParser demo = (DemoInfo.DemoParser)sender;

            //set the states which type of weapon is fired.
            bool isHE = (e.Weapon.Weapon == DemoInfo.EquipmentElement.HE);
            bool isFlash = (e.Weapon.Weapon == DemoInfo.EquipmentElement.Flash);
            bool isDecoy = (e.Weapon.Weapon == DemoInfo.EquipmentElement.Decoy);
            bool isIncendiary = (e.Weapon.Weapon == DemoInfo.EquipmentElement.Incendiary);
            bool isMolotov = (e.Weapon.Weapon == DemoInfo.EquipmentElement.Molotov);
            bool isSmoke = (e.Weapon.Weapon == DemoInfo.EquipmentElement.Smoke);

            //actually we only want to know the "HE" events (later more).
            if (isHE || isFlash || isDecoy || isIncendiary || isMolotov || isSmoke)
            {
                //set the information about the weapon event to a new row.
                DataRow drNewWeaponEvent = this.dtWeaponEvents.NewRow();
                drNewWeaponEvent["match_id"] = drMatch["id"];
                drNewWeaponEvent["round"] = demo.TScore + demo.CTScore + 1;
                drNewWeaponEvent["tick"] = demo.CurrentTick;
                drNewWeaponEvent["shooter_steam_id"] = e.Shooter.SteamID;
                drNewWeaponEvent["shooter_position_x"] = e.Shooter.Position.X;
                drNewWeaponEvent["shooter_position_y"] = e.Shooter.Position.Y;
                drNewWeaponEvent["shooter_position_z"] = e.Shooter.Position.Z;
                drNewWeaponEvent["shooter_weapon"] = e.Weapon.Weapon;

                //set the new row to the table.
                this.dtWeaponEvents.Rows.Add(drNewWeaponEvent);
            }
        }

        //click event to parse the demo to get all the information of the demo.
        private void btnReadDemoHeader_Click(object sender, EventArgs e)
        {
            //check if the file is available to parse.
            if (File.Exists(txtSelectDemo.Text))
            {
                //set the default for the unsafe mode (rendering the single ticks for broken demos).
                bool unsafeMode = false;

                //create the file stream to read the demo.
                FileStream demoFileStream = File.OpenRead(txtSelectDemo.Text);

                //open the demo and parse the header to display on application.
                DemoInfo.DemoParser demo = new DemoInfo.DemoParser(demoFileStream);

                //bind the header events to their handler.
                demo.HeaderParsed += HandleHeaderParsed;
                
                //parse the header of the demo to get the common information.
                demo.ParseHeader();

                //check if the demo is valid and available.
                if (demo.Header.PlaybackTicks < 1)
                {
                    //check  if the user want ot parse in unsafe mode.
                    DialogResult dlgState = MessageBox.Show("No ticks detected! Parse single ticks from demo (maybe uncomplete)?", "clanify - Analytics Client", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    //check if the unsafe mode should be used.
                    if (dlgState == DialogResult.No )
                    {
                        unsafeMode = false;
                        return;
                    } 
                    else
                    {
                        unsafeMode = true;
                    }
                }

                //check if the demo information should be added.
                if (chkAppend.Checked == false)
                {
                    //get the emtpy datatable to save the match information.
                    TableMatch clsTableMatch = new TableMatch(this.dbConnection);
                    DataTable dtMatch = clsTableMatch.getTableSchema();

                    //get the date and time value from the picker and set the merged one.
                    DateTime date = dtpInfoMatchDate.Value;
                    DateTime time = dtpInfoMatchTime.Value;
                    DateTime matchStart = new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);

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

                    //create the checksum of the file and set to the new row.
                    MD5 md5 = MD5.Create();
                    Stream demoStream = File.OpenRead(txtSelectDemo.Text);
                    Regex notAlpha = new Regex("[^a-zA-Z0-9]");
                    drNewMatch["file_checksum"] = notAlpha.Replace(BitConverter.ToString(md5.ComputeHash(demoStream)).Replace("-", "‌​").ToLower(), "");

                    //save the information to the database.
                    drNewMatch = clsTableMatch.saveRowMatch(drNewMatch);
                    this.drMatch = drNewMatch;

                    //init the table for the frags information.
                    TableFrags clsFrags = new TableFrags(this.dbConnection);
                    this.dtFrags = clsFrags.getTableSchema();

                    //init the table for the damage information.
                    TableDamage clsDamage = new TableDamage(this.dbConnection);
                    this.dtDamage = clsDamage.getTableSchema();

                    //init the table for the weapon events.
                    TableWeaponEvents clsWeaponEvents = new TableWeaponEvents(this.dbConnection);
                    this.dtWeaponEvents = clsWeaponEvents.getTableSchema();

                    //init the table for the player information.
                    TablePlayers clsPlayers = new TablePlayers(this.dbConnection);
                    this.dtPlayers = clsPlayers.getTableSchema();

                    //init the table for the team information.
                    TableTeams clsTeams = new TableTeams(this.dbConnection);
                    this.dtTeams = clsTeams.getTableSchema();

                    //init the table for the match players information.
                    TableMatchPlayers clsMatchPlayers = new TableMatchPlayers(this.dbConnection);
                    this.dtMatchPlayers = clsMatchPlayers.getTableSchema();

                    //init the table for the rounds information.
                    TableRounds clsRounds = new TableRounds(this.dbConnection);
                    this.dtRounds = clsRounds.getTableSchema();
                }
                
                //bind the main demo events to their handler.
                demo.PlayerKilled += HandlePlayerKilled;
                demo.PlayerHurt += HandlePlayerHurt;
                demo.RoundStart += HandleRoundStart;
                demo.RoundEnd += HandleRoundEnd;
                demo.RoundOfficiallyEnd += HandleRoundOfficialEnd;
                demo.MatchStarted += HandleMatchStarted;
                demo.WeaponFired += HandleWeaponFired;

                //check which mode shoud be used.
                if (unsafeMode == true)
                {
                    try
                    {
                        //now we can start parsing the whole demo.
                        while (demo.ParseNextTick())
                        {
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                else
                {
                    //parse the demo to the end.
                    demo.ParseToEnd();
                }
                
                //close the demo file stream.
                demoFileStream.Close();
                demoFileStream.Dispose();
                demoFileStream = null;

                //the file was parsed to the end, show some feedback.
                lblLoadedInfo.BackColor = ControlPaint.Light(Color.Green);
                btnSave.Enabled = true;
            }
            else
            {
                MessageBox.Show("The demo file isn't available!", "clanify - Analytics Client", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// event to initialize the form of the client.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmClient_Load(object sender, EventArgs e)
        {
            //init the state for loading and saving the data.
            lblLoadedInfo.BackColor = ControlPaint.Light(Color.Red);
            lblSavedInfo.BackColor = ControlPaint.Light(Color.Red);
            btnSave.Enabled = false;

            //init the database connection.
            this.initDatabaseConnection();

            //check and show the connection state.
            checkConnectionDB();

            //initialize the list of events and maps.
            this.FillCmbEventName();
            this.FillCmbMapName();

            //set the default date and time to the current day and hour.
            DateTime currentDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, 0, 0);

            //initialize the date and time picker.
            dtpInfoMatchDate.Value = currentDateTime;
            dtpInfoMatchTime.Value = currentDateTime;
        }

        /// <summary>
        /// event to store the demo information to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //check if there is a open round.
            if (this.drRound != null)
            {
                //add the round to the table.
                this.dtRounds.Rows.Add(this.drRound);
                this.drRound = null;
            }

            //save the frags to the database.
            TableFrags clsTableFrags = new TableFrags(this.dbConnection);
            clsTableFrags.saveTable(this.dtFrags, (Int64) drMatch["id"]);

            //save the damage to the database.
            TableDamage clsTableDamage = new TableDamage(this.dbConnection);
            clsTableDamage.saveTable(this.dtDamage, (Int64) drMatch["id"]);

            //save the weapon events to the database.
            TableWeaponEvents clsTableWeaponEvents = new TableWeaponEvents(this.dbConnection);
            clsTableWeaponEvents.saveTable(this.dtWeaponEvents, (Int64) drMatch["id"]);

            //save the players to the database.
            TablePlayers clsTablePlayers = new TablePlayers(this.dbConnection);
            clsTablePlayers.saveTable(this.dtPlayers);

            //save the match players to the database.
            TableMatchPlayers clsTableMatchPlayers = new TableMatchPlayers(this.dbConnection);
            clsTableMatchPlayers.saveTable(this.dtMatchPlayers, (Int64) drMatch["id"]);

            //save the rounds to the database.
            TableRounds clsTableRounds = new TableRounds(this.dbConnection);
            clsTableRounds.saveTable(this.dtRounds, (Int64)drMatch["id"]);

            //set the feedback for the user.
            lblSavedInfo.BackColor = ControlPaint.Light(Color.Green);
        }

        /// <summary>
        /// event to show the settings form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// method to check if the database connection is available and show the current state.
        /// </summary>
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

        /// <summary>
        /// event to switch the demo information tab to read only. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAppend_CheckedChanged(object sender, EventArgs e)
        {
            //cast the object to a CheckBox.
            CheckBox chkSender = (CheckBox)sender;

            //check if the CheckBox is checked.
            if (chkSender.Checked == true)
            {
                dtpInfoMatchDate.Enabled = false;
                dtpInfoMatchTime.Enabled = false;
                cmbInfoEventName.Enabled = false;
                cmbInfoMapName.Enabled = false;
            } 
            else
            {
                dtpInfoMatchDate.Enabled = true;
                dtpInfoMatchTime.Enabled = true;
                cmbInfoEventName.Enabled = true;
                cmbInfoMapName.Enabled = true;
            }
        }
    }
}