using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clanify_analyzer_client.Database
{
    /// <summary>
    /// The class to organize the table 'match_players' on database.
    /// </summary>
    class TableMatchPlayers
    {
        /// <summary>
        /// The database connection to work with the database.
        /// </summary>
        private MySqlConnection dbConnection = null;

        /// <summary>
        /// Method to set the database connection to the object.
        /// </summary>
        /// <param name="dbConnection">The database connection to work with the database.</param>
        public TableMatchPlayers(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Method to get the table schema (DataTable) for the table 'match_players'.
        /// </summary>
        /// <returns>The DataTable with the table schema of the table 'match_players'.</returns>
        public DataTable getTableSchema()
        {
            //create the DataTable with the table schemas.
            DataTable dtMatchPlayers = new DataTable("match_players");
            dtMatchPlayers.Columns.Add(new DataColumn("match_id", System.Type.GetType("System.Int64")));
            dtMatchPlayers.Columns.Add(new DataColumn("steam_id", System.Type.GetType("System.Int64")));
            dtMatchPlayers.Columns.Add(new DataColumn("team_id", System.Type.GetType("System.Int64")));
            return dtMatchPlayers;
        }

        /// <summary>
        /// Method to save the table with the relationships between match and players of the DataTable.
        /// </summary>
        /// <param name="dtMatchPlayers">The DataTable with all relationships between the match and players which will be saved.</param>
        /// <returns>The state if the table was successfully saved.</returns>
        public bool saveTable(DataTable dtMatchPlayers, Int64 matchID)
        {
            try
            {
                //open the connection if the connection is not open at the moment.
                if (this.dbConnection.State != ConnectionState.Open)
                {
                    this.dbConnection.Open();
                }

                //create the DELETE statement to delete all match players of the match.
                string sqlDeleteMatchPlayers = "DELETE FROM `match_players` WHERE match_id = ?match_id;";

                //bind all parameters to the statement and execute.
                MySqlCommand cmdDelete = this.dbConnection.CreateCommand();
                cmdDelete.CommandText = sqlDeleteMatchPlayers;
                cmdDelete.Parameters.AddWithValue("?match_id", matchID);
                cmdDelete.ExecuteNonQuery();

                //create the INSERT statement to insert all match players of the match.
                string sqlInsertFrag = "INSERT INTO `match_players` (`match_id`, `steam_id`, `team_id`) " +
                    "VALUES (?match_id, ?steam_id, ?team_id);";
                
                //run through all rows to save the match players.
                foreach (DataRow drMatchPlayer in dtMatchPlayers.Rows)
                {
                    //bind all parameters to the statement and execute.
                    MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                    cmdInsert.CommandText = sqlInsertFrag;
                    cmdInsert.Parameters.AddWithValue("?match_id", matchID);
                    cmdInsert.Parameters.AddWithValue("?steam_id", drMatchPlayer["steam_id"]);
                    cmdInsert.Parameters.AddWithValue("?team_id", drMatchPlayer["team_id"]);
                    cmdInsert.ExecuteNonQuery();
                }

                //return the state.
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            finally
            {
                //close the connection if the connection is open.
                if (this.dbConnection.State == ConnectionState.Open)
                {
                    this.dbConnection.Close();
                }
            }
        }
    }
}