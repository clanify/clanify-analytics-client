using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clanify_analyzer_client.Database
{
    class TableTeams
    {
        //the property of the database connection.
        private MySqlConnection dbConnection = null;

        //constructor to initialize the object.
        public TableTeams(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        //method to get a empty table for the damage.
        public DataTable getTableSchema()
        {
            DataTable dtTeams = new DataTable("teams");
            dtTeams.Columns.Add(new DataColumn("id", System.Type.GetType("System.Int64")));
            dtTeams.Columns.Add(new DataColumn("name", System.Type.GetType("System.String")));
            return dtTeams;
        }

        //function to check if the team already exists.
        public Int64 existsTeam(string teamName)
        {
            try
            {
                //create the select command to get the count of all found matches.
                string sqlSelect = "SELECT id FROM `teams` WHERE name = ?name";

                //create the command and bind all the parameters to the query.
                MySqlCommand cmdSelect = this.dbConnection.CreateCommand();
                cmdSelect.CommandText = sqlSelect;
                cmdSelect.Parameters.AddWithValue("?name", teamName);
                object rtnValue = cmdSelect.ExecuteScalar();

                //try to parse the return value of the query.
                Int16 teamID = -1;

                //check if there is no result.
                if (rtnValue == null)
                {
                    return teamID;
                }

                //check if the player is already available.
                if (Int16.TryParse(rtnValue.ToString(), out teamID) == false)
                {
                    return teamID;
                }

                //return the state if the match already exists.
                return teamID;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        //method to save the table with all players to the database.
        public DataTable saveTable(DataTable dtTeams)
        {
            try
            {
                //open the connection to insert some data.
                if (!(this.dbConnection.State == ConnectionState.Open))
                {
                    this.dbConnection.Open();
                }

                //run through all players to insert or update.
                foreach (DataRow drTeam in dtTeams.Rows)
                {
                    //check if the player exists.
                    Int64 teamID = existsTeam((string)drTeam["name"]);

                    if (teamID > 0)
                    {
                        drTeam["id"] = teamID;
                    }
                    else
                    {
                        //create the insert statement.
                        string sqlInsertTeam = "INSERT INTO `teams` (id, name) VALUES (NULL, ?name);";

                        //bind all the parameters to the statement.
                        MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                        cmdInsert.CommandText = sqlInsertTeam;
                        cmdInsert.Parameters.AddWithValue("?name", drTeam["name"]);
                        cmdInsert.ExecuteNonQuery();

                        //set the ID of the new row to the DataTable.
                        drTeam["id"] = cmdInsert.LastInsertedId;
                    }
                }

                //return the state.
                return dtTeams;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
            finally
            {
                this.dbConnection.Close();
            }
        }
    }
}
