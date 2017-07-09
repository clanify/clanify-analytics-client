using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace clanify_analyzer_client.Database
{
    /// <summary>
    /// The class to organize the table 'teams' on database.
    /// </summary>
    class TableTeams
    {
        /// <summary>
        /// The database connection to work with the database.
        /// </summary>
        private MySqlConnection dbConnection = null;

        /// <summary>
        /// Method to set the database connection to the object.
        /// </summary>
        /// <param name="dbConnection">The database connection to work with the database.</param>
        public TableTeams(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Method to get the table schema (DataTable) for the table 'teams'.
        /// </summary>
        /// <returns>The DataTable with the table schema of the table 'teams'.</returns>
        public DataTable getTableSchema()
        {
            //create the DataTable with the table schemas.
            DataTable dtTeams = new DataTable("teams");
            dtTeams.Columns.Add(new DataColumn("id", System.Type.GetType("System.Int64")));
            dtTeams.Columns.Add(new DataColumn("name", System.Type.GetType("System.String")));
            return dtTeams;
        }

        /// <summary>
        /// Method to get the ID of a existing team.
        /// </summary>
        /// <param name="teamName">The name of the team to get the ID if the team exists.</param>
        /// <returns>The ID of the existing team or -1 if the team doesn't exists.</returns>
        public Int64 getTeamID(string teamName)
        {
            try
            {
                //create the select command to get the ID of the team if it exists.
                string sqlSelect = "SELECT id FROM `teams` WHERE name = ?name";

                //bind all parameters to the statement and execute.
                MySqlCommand cmdSelect = this.dbConnection.CreateCommand();
                cmdSelect.CommandText = sqlSelect;
                cmdSelect.Parameters.AddWithValue("?name", teamName);
                object rtnValue = cmdSelect.ExecuteScalar();

                //reset the team ID to get the ID on the following steps.
                Int64 teamID = -1;

                //check if there is no result.
                if (rtnValue == null)
                {
                    return teamID;
                }

                //try to get the ID of the team.
                if (Int64.TryParse(rtnValue.ToString(), out teamID) == false)
                {
                    return -1;
                }

                //return the ID of the team or -1 if the team doesn't exists.
                return teamID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        /// <summary>
        /// Method to save the table with all teams of the DataTable.
        /// </summary>
        /// <param name="dtTeams">The DataTable with all teams which will be saved.</param>
        /// <returns>The DataTable with the teams (after INSERT with the ID).</returns>
        public DataTable saveTable(DataTable dtTeams)
        {
            try
            {
                //open the connection if the connection is not open at the moment.
                if (this.dbConnection.State != ConnectionState.Open)
                {
                    this.dbConnection.Open();
                }

                //run through all teams to INSERT them to database if not exists.
                foreach (DataRow drTeam in dtTeams.Rows)
                {
                    //try to get the ID of the team if the team already exists.
                    Int64 teamID = getTeamID((string) drTeam["name"]);

                    //check if a positive number is available (so the team exists).
                    if (teamID > 0)
                    {
                        //set the available ID to the current row.
                        drTeam["id"] = teamID;
                    }
                    else
                    {
                        //create the INSERT statement to create a new team.
                        string sqlInsertTeam = "INSERT INTO `teams` (id, name) VALUES (NULL, ?name);";

                        //bind all parameters to the statement and execute.
                        MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                        cmdInsert.CommandText = sqlInsertTeam;
                        cmdInsert.Parameters.AddWithValue("?name", drTeam["name"]);
                        cmdInsert.ExecuteNonQuery();

                        //set the ID of the inserted team back to the DataTable.
                        drTeam["id"] = cmdInsert.LastInsertedId;
                    }
                }

                //return the DataTable with the teams and their ID.
                return dtTeams;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
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
