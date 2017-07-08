using System.Data;
using MySql.Data.MySqlClient;

namespace clanify_analyzer_client.Database
{
    class TableFrags
    {
        //the property of the database connection.
        private MySqlConnection dbConnection = null;

        //constructor to initialize the object.
        public TableFrags(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        //method to get a empty table for the frags.
        public DataTable getTableSchema()
        {
            DataTable dtFrags = new DataTable("frags");
            dtFrags.Columns.Add(new DataColumn("match_id", System.Type.GetType("System.Int64")));
            dtFrags.Columns.Add(new DataColumn("round", System.Type.GetType("System.Int16")));
            dtFrags.Columns.Add(new DataColumn("tick", System.Type.GetType("System.Int32")));
            dtFrags.Columns.Add(new DataColumn("victim_steam_id", System.Type.GetType("System.Int64")));
            dtFrags.Columns.Add(new DataColumn("victim_weapon", System.Type.GetType("System.String")));
            dtFrags.Columns.Add(new DataColumn("victim_position_x", System.Type.GetType("System.Decimal")));
            dtFrags.Columns.Add(new DataColumn("victim_position_y", System.Type.GetType("System.Decimal")));
            dtFrags.Columns.Add(new DataColumn("victim_position_z", System.Type.GetType("System.Decimal")));
            dtFrags.Columns.Add(new DataColumn("victim_hp", System.Type.GetType("System.Int16")));
            dtFrags.Columns.Add(new DataColumn("killer_steam_id", System.Type.GetType("System.Int64")));
            dtFrags.Columns.Add(new DataColumn("killer_weapon", System.Type.GetType("System.String")));
            dtFrags.Columns.Add(new DataColumn("killer_position_x", System.Type.GetType("System.Decimal")));
            dtFrags.Columns.Add(new DataColumn("killer_position_y", System.Type.GetType("System.Decimal")));
            dtFrags.Columns.Add(new DataColumn("killer_position_z", System.Type.GetType("System.Decimal")));
            dtFrags.Columns.Add(new DataColumn("killer_hp", System.Type.GetType("System.Int16")));
            dtFrags.Columns.Add(new DataColumn("assister_steam_id", System.Type.GetType("System.Int64")));
            dtFrags.Columns.Add(new DataColumn("is_headshot", System.Type.GetType("System.Boolean")));
            return dtFrags;
        }

        /// <summary>
        /// Method to save a DataTable with rows to the database.
        /// </summary>
        /// <param name="dtFrags">The DataTable which will be saved on database.</param>
        /// <returns>The state if the DataTable could be saved successfully.</returns>
        public bool saveTable(DataTable dtFrags)
        {
            try
            {
                //open the connection to insert some data.
                this.dbConnection.Open();

                //create the update statement.
                string sqlInsertFrag = "INSERT INTO `frags` (`match_id`, `round`, `tick`, `victim_steam_id`, `victim_weapon`, " +
                    "`victim_position_x`, `victim_position_y`, `victim_position_z`, `victim_hp`, `killer_steam_id`, `killer_weapon`, " +
                    "`killer_position_x`, `killer_position_y`, `killer_position_z`, `killer_hp`, `assister_steam_id`, `is_headshot`) " +
                    "VALUES (?match_id, ?round, ?tick, ?victim_steam_id, ?victim_weapon, ?victim_position_x, ?victim_position_y, " +
                    "?victim_position_z, ?victim_hp, ?killer_steam_id, ?killer_weapon, ?killer_position_x, ?killer_position_y, " +
                    "?killer_position_z, ?killer_hp, ?assister_steam_id, ?is_headshot);";

                //run through all rows to save.
                foreach (DataRow drFrag in dtFrags.Rows)
                {
                    //bind all the parameters to the statement.
                    MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                    cmdInsert.CommandText = sqlInsertFrag;
                    cmdInsert.Parameters.AddWithValue("?match_id", drFrag["match_id"]);
                    cmdInsert.Parameters.AddWithValue("?round", drFrag["round"]);
                    cmdInsert.Parameters.AddWithValue("?tick", drFrag["tick"]);
                    cmdInsert.Parameters.AddWithValue("?victim_steam_id", drFrag["victim_steam_id"]);
                    cmdInsert.Parameters.AddWithValue("?victim_weapon", drFrag["victim_weapon"]);
                    cmdInsert.Parameters.AddWithValue("?victim_position_x", drFrag["victim_position_x"]);
                    cmdInsert.Parameters.AddWithValue("?victim_position_y", drFrag["victim_position_y"]);
                    cmdInsert.Parameters.AddWithValue("?victim_position_z", drFrag["victim_position_z"]);
                    cmdInsert.Parameters.AddWithValue("?victim_hp", drFrag["victim_hp"]);
                    cmdInsert.Parameters.AddWithValue("?killer_steam_id", drFrag["killer_steam_id"]);
                    cmdInsert.Parameters.AddWithValue("?killer_weapon", drFrag["killer_weapon"]);
                    cmdInsert.Parameters.AddWithValue("?killer_position_x", drFrag["killer_position_x"]);
                    cmdInsert.Parameters.AddWithValue("?killer_position_y", drFrag["killer_position_y"]);
                    cmdInsert.Parameters.AddWithValue("?killer_position_z", drFrag["killer_position_z"]);
                    cmdInsert.Parameters.AddWithValue("?killer_hp", drFrag["killer_hp"]);
                    cmdInsert.Parameters.AddWithValue("?assister_steam_id", drFrag["assister_steam_id"]);
                    cmdInsert.Parameters.AddWithValue("?is_headshot", drFrag["is_headshot"]);
                    cmdInsert.ExecuteNonQuery();
                }

                //return the state.
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                this.dbConnection.Close();
            }
        }
    }
}
