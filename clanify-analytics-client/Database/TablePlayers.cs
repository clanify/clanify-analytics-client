using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace clanify_analyzer_client.Database
{
    /// <summary>
    /// The class to organize the table 'players' on database.
    /// </summary>
    class TablePlayers
    {
        /// <summary>
        /// The database connection to work with the database.
        /// </summary>
        private MySqlConnection dbConnection = null;

        /// <summary>
        /// Method to set the database connection to the object.
        /// </summary>
        /// <param name="dbConnection">The database connection to work with the database.</param>
        public TablePlayers(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Method to get the table schema (DataTable) for the table 'players'.
        /// </summary>
        /// <returns>The DataTable with the table schema of the table 'players'.</returns>
        public DataTable getTableSchema()
        {
            //create the DataTable with the table schemas.
            DataTable dtPlayers = new DataTable("players");
            dtPlayers.Columns.Add(new DataColumn("steam_id", System.Type.GetType("System.Int64")));
            dtPlayers.Columns.Add(new DataColumn("name", System.Type.GetType("System.String")));
            return dtPlayers;
        }
        
        /// <summary>
        /// Method to check if a player already exists on table 'players'.
        /// </summary>
        /// <param name="steamID">The steam ID of the player to check if the player exists.</param>
        /// <returns>The state if the player exists or not.</returns>
        public bool existsPlayer(Int64 steamID)
        {
            try
            {
                //create the SELECT command to get the count of the found players.
                string sqlSelect = "SELECT COUNT(steam_id) FROM `players` WHERE steam_id = ?steam_id";

                //bind all parameters to the statement and execute.
                MySqlCommand cmdSelect = this.dbConnection.CreateCommand();
                cmdSelect.CommandText = sqlSelect;
                cmdSelect.Parameters.AddWithValue("?steam_id", steamID);
                object rtnValue = cmdSelect.ExecuteScalar();

                //reset the count to get the count on the following steps.
                Int16 count = 0;

                //check if there is no result.
                if (rtnValue == null)
                {
                    return false;
                }

                //try to get the count of the players.
                if (Int16.TryParse(rtnValue.ToString(), out count) == false)
                {
                    return false;
                }

                //return the state if the player exists or not.
                return (count > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        
        /// <summary>
        /// Method to save the table with all players of the DataTable.
        /// </summary>
        /// <param name="dtTeams">The DataTable with all players which will be saved.</param>
        /// <returns>The state if the table was successfully saved.</returns>
        public bool saveTable(DataTable dtPlayers)
        {
            try
            {
                //open the connection if the connection is not open at the moment.
                if (this.dbConnection.State != ConnectionState.Open)
                {
                    this.dbConnection.Open();
                }

                //run through all players to INSERT them to database if not exists.
                foreach (DataRow drPlayer in dtPlayers.Rows)
                {
                    //check if the player already exists.
                    if (existsPlayer((Int64) drPlayer["steam_id"]) == false)
                    {
                        //create the INSERT statement to create a new player.
                        string sqlInsertPlayer = "INSERT INTO `players` (steam_id, name) VALUES (?steam_id, ?name);";

                        //bind all parameters to the statement and execute.
                        MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                        cmdInsert.CommandText = sqlInsertPlayer;
                        cmdInsert.Parameters.AddWithValue("?steam_id", drPlayer["steam_id"]);
                        cmdInsert.Parameters.AddWithValue("?name", drPlayer["name"]);
                        cmdInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        //create the UPDATE statement to update a existing player.
                        string sqlUpdatePlayer = "UPDATE `players` SET name = ?name WHERE steam_id = ?steam_id";

                        //bind all parameters to the statement and execute.
                        MySqlCommand cmdUpdate = this.dbConnection.CreateCommand();
                        cmdUpdate.CommandText = sqlUpdatePlayer;
                        cmdUpdate.Parameters.AddWithValue("?steam_id", drPlayer["steam_id"]);
                        cmdUpdate.Parameters.AddWithValue("?name", drPlayer["name"]);
                        cmdUpdate.ExecuteNonQuery();
                    }
                }

                //return the state.
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                this.dbConnection.Close();
            }
        }
    }
}