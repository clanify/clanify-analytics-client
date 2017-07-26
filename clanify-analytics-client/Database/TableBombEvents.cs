using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace clanify_analyzer_client.Database
{
    /// <summary>
    /// Klasse um die Verwaltung der Aktionen der Bombe zu verwalten.
    /// </summary>
    class TableBombEvents
    {
        /// <summary>
        /// Konstanten für die verschiedenen Events der Bombe.
        /// </summary>
        public const string EVENT_DEFUSE_ABORT = "DEFUSE_ABORT";
        public const string EVENT_DEFUSE_BEGIN = "DEFUSE_BEGIN";
        public const string EVENT_DEFUSE_DEFUSED = "DEFUSE_DEFUSED";
        public const string EVENT_PLANT_ABORT = "PLANT_ABORT";
        public const string EVENT_PLANT_BEGIN = "PLANT_BEGIN";
        public const string EVENT_PLANT_PLANTED = "PLANT_PLANTED";
        public const string EVENT_EXPLODED = "EXPLODED";

        /// <summary>
        /// Die Datenbank-Verbindung um mit der Datenbank zu kommunizieren.
        /// </summary>
        private MySqlConnection dbConnection = null;

        /// <summary>
        /// Konstruktor der Klasse um die Datenbank-Verbindung setzen zu können.
        /// </summary>
        /// <param name="dbConnection">Die Datenbank-Verbindung um mit der Datenbank kommunizieren zu können.</param>
        public TableBombEvents(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Methode um das Tabellen-Schema für den Speicher zu ermitteln.
        /// </summary>
        /// <returns>Das Tabellen-Schema als DataTable.</returns>
        public DataTable getTableSchema()
        {
            DataTable dtBombEvents = new DataTable("bomb_events");
            dtBombEvents.Columns.Add(new DataColumn("match_id", System.Type.GetType("System.Int64")));
            dtBombEvents.Columns.Add(new DataColumn("round", System.Type.GetType("System.Int16")));
            dtBombEvents.Columns.Add(new DataColumn("tick", System.Type.GetType("System.Int32")));
            dtBombEvents.Columns.Add(new DataColumn("player_steam_id", System.Type.GetType("System.Int64")));
            dtBombEvents.Columns.Add(new DataColumn("has_kit", System.Type.GetType("System.Boolean")));
            dtBombEvents.Columns.Add(new DataColumn("site", System.Type.GetType("System.String")));
            dtBombEvents.Columns.Add(new DataColumn("event", System.Type.GetType("System.String")));
            return dtBombEvents;
        }

        /// <summary>
        /// Methode um die Tabelle im Speicher in der Datenbank speichern zu können.
        /// </summary>
        /// <param name="dtBombEvents">Die Tabelle mit den Zeilen welche gespeichert werden sollen.</param>
        /// <param name="matchID">Die ID des Spiels für welches die Zeilen gespeichert werden sollen.</param>
        /// <returns>Der Status ob die Zeilen der Tabelle gespeichert werden konnten.</returns>
        public bool saveTable(DataTable dtBombEvents, Int64 matchID)
        {
            try
            {
                //Verbindung zur Datenbank öffnen.
                this.dbConnection.Open();

                //Initialisieren des DELETE-Befehls.
                string sqlDeleteDamage = "DELETE FROM `bomb_events` WHERE `match_id` = ?match_id;";

                //Alle Parameter binden und DELETE-Befehl ausführen. Es werden dabei alle
                //Bomben-Events aus der Datenbank gelöscht um diese neu aufbauen zu können.
                MySqlCommand cmdDelete = this.dbConnection.CreateCommand();
                cmdDelete.CommandText = sqlDeleteDamage;
                cmdDelete.Parameters.AddWithValue("?match_id", matchID);
                cmdDelete.ExecuteNonQuery();

                //Initialisieren des INSERT-Befehls.
                string sqlInsertBombEvent = "INSERT INTO `bomb_events` (`match_id`, `round`, `tick`, `player_steam_id`, `has_kit`, " +
                    "`site`, `event`) VALUES (?match_id, ?round, ?tick, ?player_steam_id, ?has_kit, ?site, ?event);";

                //Alle Zeilen der Tabelle durchlaufen um diese in der Datenbank speichern zu können.
                foreach (DataRow drBombEvent in dtBombEvents.Rows)
                {
                    //Alle Parameter des INSERT-Befehls setzen und den Befehl anschließend ausführen.
                    MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                    cmdInsert.CommandText = sqlInsertBombEvent;
                    cmdInsert.Parameters.AddWithValue("?match_id", matchID);
                    cmdInsert.Parameters.AddWithValue("?round", drBombEvent["round"]);
                    cmdInsert.Parameters.AddWithValue("?tick", drBombEvent["tick"]);
                    cmdInsert.Parameters.AddWithValue("?player_steam_id", drBombEvent["player_steam_id"]);
                    cmdInsert.Parameters.AddWithValue("?has_kit", drBombEvent["has_kit"]);
                    cmdInsert.Parameters.AddWithValue("?site", drBombEvent["site"]);
                    cmdInsert.Parameters.AddWithValue("?event", drBombEvent["event"]);
                    cmdInsert.ExecuteNonQuery();
                }

                //Status zurückgeben (es scheint alles funktioniert zu haben).
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                //Verbindung zur Datenbank schließen, falls diese noch offen ist.
                if (this.dbConnection.State == ConnectionState.Open)
                {
                    this.dbConnection.Close();
                }
            }
        }
    }
}