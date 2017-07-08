using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace clanify_analyzer_client.Database
{
    class TableMatch
    {
        //the property of the database connection.
        private MySqlConnection dbConnection = null;

        //constructor to initialize the object.
        public TableMatch(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }
        
        //method to get a empty table for the matches.
        public DataTable getTableSchema()
        {
            DataTable dtMatch = new DataTable("match");
            dtMatch.Columns.Add(new DataColumn("id", System.Type.GetType("System.Int64")));
            dtMatch.Columns.Add(new DataColumn("event_id", System.Type.GetType("System.Int64")));
            dtMatch.Columns.Add(new DataColumn("match_start", System.Type.GetType("System.DateTime")));
            dtMatch.Columns.Add(new DataColumn("client_name", System.Type.GetType("System.String")));
            dtMatch.Columns.Add(new DataColumn("filestamp", System.Type.GetType("System.String")));
            dtMatch.Columns.Add(new DataColumn("game_directory", System.Type.GetType("System.String")));
            dtMatch.Columns.Add(new DataColumn("map_name", System.Type.GetType("System.String")));
            dtMatch.Columns.Add(new DataColumn("network_protocol", System.Type.GetType("System.Int32")));
            dtMatch.Columns.Add(new DataColumn("playback_frames", System.Type.GetType("System.Int32")));
            dtMatch.Columns.Add(new DataColumn("playback_ticks", System.Type.GetType("System.Int32")));
            dtMatch.Columns.Add(new DataColumn("playback_time", System.Type.GetType("System.Decimal")));
            dtMatch.Columns.Add(new DataColumn("protocol", System.Type.GetType("System.Int16")));
            dtMatch.Columns.Add(new DataColumn("server_name", System.Type.GetType("System.String")));
            dtMatch.Columns.Add(new DataColumn("signon_length", System.Type.GetType("System.Int32")));
            return dtMatch;
        }

        //method to save a row to the match table on database.
        public DataRow saveRowMatch(DataRow drMatch)
        {
            try
            {
                //check if the match already exists and get the ID.
                Int64 matchID = existsMatch(drMatch);

                //open the connection to insert some data.
                this.dbConnection.Open();

                //update the existing match if available.
                if (matchID > 0)
                {
                    //create the update statement.
                    string sqlUpdateMatch = "UPDATE `match` SET client_name = ?client_name, filestamp = ?filestamp, game_directory = ?game_directory, " +
                        "network_protocol = ?network_protocol, playback_frames = ?playback_frames, playback_ticks = ?playback_ticks, " +
                        "playback_time = ?playback_time, protocol = ?protocol, server_name = ?server_name, signon_length = ?signon_length " +
                        "WHERE id = " + matchID;

                    //bind all the parameters to the statement.
                    MySqlCommand cmdUpdate = this.dbConnection.CreateCommand();
                    cmdUpdate.CommandText = sqlUpdateMatch;
                    cmdUpdate.Parameters.AddWithValue("?client_name", drMatch["client_name"]);
                    cmdUpdate.Parameters.AddWithValue("?filestamp", drMatch["filestamp"]);
                    cmdUpdate.Parameters.AddWithValue("?game_directory", drMatch["game_directory"]);
                    cmdUpdate.Parameters.AddWithValue("?network_protocol", drMatch["network_protocol"]);
                    cmdUpdate.Parameters.AddWithValue("?playback_frames", drMatch["playback_frames"]);
                    cmdUpdate.Parameters.AddWithValue("?playback_ticks", drMatch["playback_ticks"]);
                    cmdUpdate.Parameters.AddWithValue("?playback_time", drMatch["playback_time"]);
                    cmdUpdate.Parameters.AddWithValue("?protocol", drMatch["protocol"]);
                    cmdUpdate.Parameters.AddWithValue("?server_name", drMatch["server_name"]);
                    cmdUpdate.Parameters.AddWithValue("?signon_length", drMatch["signon_length"]);
                    cmdUpdate.ExecuteNonQuery();

                    //set the ID to the row.
                    drMatch["id"] = matchID;
                }
                else 
                {
                    //create the insert statement.
                    string sqlInsertMatch = "INSERT INTO `match` (id, event_id, match_start, client_name, filestamp, game_directory, map_name, network_protocol, " +
                        "playback_frames, playback_ticks, playback_time, protocol, server_name, signon_length) VALUES (NULL, ?event_id, ?match_start, " +
                        "?client_name, ?filestamp, ?game_directory, ?map_name, ?network_protocol, ?playback_frames, ?playback_ticks, ?playback_time, " +
                        "?protocol, ?server_name, ?signon_length);";

                    //bind all the parameters to the statement.
                    MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                    cmdInsert.CommandText = sqlInsertMatch;
                    cmdInsert.Parameters.AddWithValue("?event_id", drMatch["event_id"]);
                    cmdInsert.Parameters.AddWithValue("?match_start", drMatch["match_start"]);
                    cmdInsert.Parameters.AddWithValue("?client_name", drMatch["client_name"]);
                    cmdInsert.Parameters.AddWithValue("?filestamp", drMatch["filestamp"]);
                    cmdInsert.Parameters.AddWithValue("?game_directory", drMatch["game_directory"]);
                    cmdInsert.Parameters.AddWithValue("?map_name", drMatch["map_name"]);
                    cmdInsert.Parameters.AddWithValue("?network_protocol", drMatch["network_protocol"]);
                    cmdInsert.Parameters.AddWithValue("?playback_frames", drMatch["playback_frames"]);
                    cmdInsert.Parameters.AddWithValue("?playback_ticks", drMatch["playback_ticks"]);
                    cmdInsert.Parameters.AddWithValue("?playback_time", drMatch["playback_time"]);
                    cmdInsert.Parameters.AddWithValue("?protocol", drMatch["protocol"]);
                    cmdInsert.Parameters.AddWithValue("?server_name", drMatch["server_name"]);
                    cmdInsert.Parameters.AddWithValue("?signon_length", drMatch["signon_length"]);
                    cmdInsert.ExecuteNonQuery();

                    //insert the id back to the row.
                    drMatch["id"] = cmdInsert.LastInsertedId;
                }
                
                //return the row (on insert now with ID).
                return drMatch;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
            finally
            {
                this.dbConnection.Close();
            }
        }

        //function to check if a match already exists.
        public Int64 existsMatch(DataRow drMatch)
        {
            try
            {
                //open the connection to get some data from database.
                this.dbConnection.Open();

                //create the select command to get the count of all found matches.
                string sqlSelect = "SELECT id FROM `match` WHERE event_id = ?event_id AND match_start = ?match_start AND map_name = ?map_name";

                //create the command and bind all the parameters to the query.
                MySqlCommand cmdSelect = this.dbConnection.CreateCommand();
                cmdSelect.CommandText = sqlSelect;
                cmdSelect.Parameters.AddWithValue("?event_id", drMatch["event_id"]);
                cmdSelect.Parameters.AddWithValue("?match_start", drMatch["match_start"]);
                cmdSelect.Parameters.AddWithValue("?map_name", drMatch["map_name"]);
                object rtnValue = cmdSelect.ExecuteScalar();

                //try to parse the return value of the query.
                Int64 matchID = -1;

                //check if there is no result.
                if (rtnValue == null)
                {
                    return matchID;
                }

                //check if the match id is available.
                if (Int64.TryParse(rtnValue.ToString(), out matchID) == false)
                {
                    matchID = -1;
                }

                //return the state if the match already exists.
                return matchID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
            finally
            {
                this.dbConnection.Close();
            }
        }
    }
}