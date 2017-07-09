using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace clanify_analyzer_client.Database
{
    class TablePlayers
    {
        //the property of the database connection.
        private MySqlConnection dbConnection = null;

        //constructor to initialize the object.
        public TablePlayers(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        //method to get a empty table for the damage.
        public DataTable getTableSchema()
        {
            DataTable dtPlayers = new DataTable("players");
            dtPlayers.Columns.Add(new DataColumn("steam_id", System.Type.GetType("System.Int64")));
            dtPlayers.Columns.Add(new DataColumn("name", System.Type.GetType("System.String")));
            return dtPlayers;
        }

        //function to check if a match already exists.
        public bool existsPlayer(Int64 steamID)
        {
            try
            {
                //create the select command to get the count of all found matches.
                string sqlSelect = "SELECT COUNT(steam_id) FROM `players` WHERE steam_id = ?steam_id";

                //create the command and bind all the parameters to the query.
                MySqlCommand cmdSelect = this.dbConnection.CreateCommand();
                cmdSelect.CommandText = sqlSelect;
                cmdSelect.Parameters.AddWithValue("?steam_id", steamID);
                object rtnValue = cmdSelect.ExecuteScalar();

                //try to parse the return value of the query.
                Int16 count = 0;

                //check if there is no result.
                if (rtnValue == null)
                {
                    return (count > 0);
                }

                //check if the player is already available.
                if (Int16.TryParse(rtnValue.ToString(), out count) == false)
                {
                    count = 0;
                }

                //return the state if the match already exists.
                return (count > 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        //method to save the table with all players to the database.
        public bool saveTable(DataTable dtPlayers)
        {
            try
            {
                //open the connection to insert some data.
                if (!(this.dbConnection.State == ConnectionState.Open))
                {
                    this.dbConnection.Open();
                }
               
                //run through all players to insert or update.
                foreach (DataRow drPlayer in dtPlayers.Rows)
                {
                    //check if the player exists.
                    if (existsPlayer((Int64) drPlayer["steam_id"]) == false)
                    {
                        //create the insert statement.
                        string sqlInsertPlayer = "INSERT INTO `players` (steam_id, name) VALUES (?steam_id, ?name);";

                        //bind all the parameters to the statement.
                        MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                        cmdInsert.CommandText = sqlInsertPlayer;
                        cmdInsert.Parameters.AddWithValue("?steam_id", drPlayer["steam_id"]);
                        cmdInsert.Parameters.AddWithValue("?name", drPlayer["name"]);
                        cmdInsert.ExecuteNonQuery();
                    }
                    else
                    {
                        //create the update statement.
                        string sqlUpdatePlayer = "UPDATE `players` SET name = ?name WHERE steam_id = ?steam_id";

                        //bind all the parameters to the statement.
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