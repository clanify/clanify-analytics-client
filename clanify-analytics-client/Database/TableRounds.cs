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
    /// The class to organize the table 'rounds' on database.
    /// </summary>
    class TableRounds
    {
        /// <summary>
        /// The database connection to work with the database.
        /// </summary>
        private MySqlConnection dbConnection = null;

        /// <summary>
        /// Method to set the database connection to the object.
        /// </summary>
        /// <param name="dbConnection">The database connection to work with the database.</param>
        public TableRounds(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Method to get the table schema (DataTable) for the table 'rounds'.
        /// </summary>
        /// <returns>The DataTable with the table schema of the table 'rounds'.</returns>
        public DataTable getTableSchema()
        {
            //create the DataTable with the table schemas.
            DataTable dtRounds = new DataTable("rounds");
            dtRounds.Columns.Add(new DataColumn("match_id", System.Type.GetType("System.Int64")));
            dtRounds.Columns.Add(new DataColumn("round", System.Type.GetType("System.Int16")));
            dtRounds.Columns.Add(new DataColumn("tick_start", System.Type.GetType("System.Int32")));
            dtRounds.Columns.Add(new DataColumn("tick_end", System.Type.GetType("System.Int32")));
            dtRounds.Columns.Add(new DataColumn("winning_team", System.Type.GetType("System.String")));
            dtRounds.Columns.Add(new DataColumn("winning_team_id", System.Type.GetType("System.Int64")));
            return dtRounds;
        }

        /// <summary>
        /// Method to save the table with all rounds of the DataTable.
        /// </summary>
        /// <param name="dtRounds">The DataTable with all rounds which will be saved.</param>
        /// <param name="matchID">The ID of the match.</param>
        /// <returns>The state if the table was successfully saved.</returns>
        public bool saveTable(DataTable dtRounds, Int64 matchID)
        {
            try
            {
                //open the connection if the connection is not open at the moment.
                if (this.dbConnection.State != ConnectionState.Open)
                {
                    this.dbConnection.Open();
                }

                //create the DELETE statement to delete all rounds of the match.
                string sqlDeleteRounds = "DELETE FROM `rounds` WHERE match_id = ?match_id;";

                //bind all parameters to the statement and execute.
                MySqlCommand cmdDelete = this.dbConnection.CreateCommand();
                cmdDelete.CommandText = sqlDeleteRounds;
                cmdDelete.Parameters.AddWithValue("?match_id", matchID);
                cmdDelete.ExecuteNonQuery();

                //create the INSERT statement to insert all rounds of the match.
                string sqlInsertRound = "INSERT INTO `rounds` (`match_id`, `round`, `tick_start`, `tick_end`, `winning_team`, `winning_team_id`) " +
                    "VALUES (?match_id, ?round, ?tick_start, ?tick_end, ?winning_team, ?winning_team_id);";

                //run through all rows to save the match players.
                foreach (DataRow drRound in dtRounds.Rows)
                {
                    //bind all parameters to the statement and execute.
                    MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                    cmdInsert.CommandText = sqlInsertRound;
                    cmdInsert.Parameters.AddWithValue("?match_id", matchID);
                    cmdInsert.Parameters.AddWithValue("?round", drRound["round"]);
                    cmdInsert.Parameters.AddWithValue("?tick_start", drRound["tick_start"]);
                    cmdInsert.Parameters.AddWithValue("?tick_end", drRound["tick_end"]);
                    cmdInsert.Parameters.AddWithValue("?winning_team", drRound["winning_team"]);
                    cmdInsert.Parameters.AddWithValue("?winning_team_id", drRound["winning_team_id"]);
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