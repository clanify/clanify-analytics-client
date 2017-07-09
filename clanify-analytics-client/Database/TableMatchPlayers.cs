using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clanify_analyzer_client.Database
{
    class TableMatchPlayers
    {
        //the property of the database connection.
        private MySqlConnection dbConnection = null;

        //constructor to initialize the object.
        public TableMatchPlayers(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        //method to get a empty table for the damage.
        public DataTable getTableSchema()
        {
            DataTable dtMatchPlayers = new DataTable("match_players");
            dtMatchPlayers.Columns.Add(new DataColumn("match_id", System.Type.GetType("System.Int64")));
            dtMatchPlayers.Columns.Add(new DataColumn("steam_id", System.Type.GetType("System.Int64")));
            dtMatchPlayers.Columns.Add(new DataColumn("team_id", System.Type.GetType("System.Int64")));
            return dtMatchPlayers;
        }

        public bool saveTable(DataTable dtMatchPlayers, Int64 matchID)
        {
            try
            {
                //open the connection to insert some data.
                this.dbConnection.Open();

                //create the delete statement.
                string sqlDeleteMatchPlayers = "DELETE FROM `match_players` WHERE match_id = ?match_id;";

                //bind all the parameters to the statement.
                MySqlCommand cmdDelete = this.dbConnection.CreateCommand();
                cmdDelete.CommandText = sqlDeleteMatchPlayers;
                cmdDelete.Parameters.AddWithValue("?match_id", matchID);
                cmdDelete.ExecuteNonQuery();

                //create the update statement.
                string sqlInsertFrag = "INSERT INTO `match_players` (`match_id`, `steam_id`, `team_id`) " +
                    "VALUES (?match_id, ?steam_id, ?team_id);";

                //run through all rows to save.
                foreach (DataRow drMatchPlayer in dtMatchPlayers.Rows)
                {
                    //bind all the parameters to the statement.
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
