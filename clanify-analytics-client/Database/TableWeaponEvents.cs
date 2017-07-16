using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clanify_analyzer_client.Database
{
    class TableWeaponEvents
    {
        /// <summary>
        /// The database connection to work with the database.
        /// </summary>
        private MySqlConnection dbConnection = null;

        /// <summary>
        /// Method to set the database connection to the object.
        /// </summary>
        /// <param name="dbConnection">The database connection to work with the database.</param>
        public TableWeaponEvents(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        /// <summary>
        /// Method to get the table schema (DataTable) for the table 'weapon_events'.
        /// </summary>
        /// <returns>The DataTable with the table schema of the table 'weapon_events'.</returns>
        public DataTable getTableSchema()
        {
            //create the DataTable with the table schemas.
            DataTable dtWeaponEvents = new DataTable("weapon_events");
            dtWeaponEvents.Columns.Add(new DataColumn("match_id", System.Type.GetType("System.Int64")));
            dtWeaponEvents.Columns.Add(new DataColumn("round", System.Type.GetType("System.Int16")));
            dtWeaponEvents.Columns.Add(new DataColumn("tick", System.Type.GetType("System.Int32")));
            dtWeaponEvents.Columns.Add(new DataColumn("shooter_steam_id", System.Type.GetType("System.Int64")));
            dtWeaponEvents.Columns.Add(new DataColumn("shooter_position_x", System.Type.GetType("System.Decimal")));
            dtWeaponEvents.Columns.Add(new DataColumn("shooter_position_y", System.Type.GetType("System.Decimal")));
            dtWeaponEvents.Columns.Add(new DataColumn("shooter_position_z", System.Type.GetType("System.Decimal")));
            dtWeaponEvents.Columns.Add(new DataColumn("shooter_weapon", System.Type.GetType("System.String")));
            return dtWeaponEvents;
        }

        /// <summary>
        /// Method to save the table with all weapon events of the DataTable.
        /// </summary>
        /// <param name="dtFrags">The DataTable which will be saved on database.</param>
        /// <param name="matchID">The ID of the match.</param>
        /// <returns>The state if the DataTable could be saved successfully.</returns>
        public bool saveTable(DataTable dtWeaponEvents, Int64 matchID)
        {
            try
            {
                //open the connection if the connection is not open at the moment.
                if (this.dbConnection.State != ConnectionState.Open)
                {
                    this.dbConnection.Open();
                }

                //create the delete statement.
                string sqlDeleteWeaponEvents = "DELETE FROM `weapon_events` WHERE match_id = ?match_id;";

                //bind all parameters to the statement and execute.
                MySqlCommand cmdDelete = this.dbConnection.CreateCommand();
                cmdDelete.CommandText = sqlDeleteWeaponEvents;
                cmdDelete.Parameters.AddWithValue("?match_id", matchID);
                cmdDelete.ExecuteNonQuery();

                //create the insert statement.
                string sqlInsertFrag = "INSERT INTO `weapon_events` (`match_id`, `round`, `tick`, `shooter_steam_id`, " +
                    "`shooter_position_x`, `shooter_position_y`, `shooter_position_z`, `shooter_weapon`) " +
                    "VALUES (?match_id, ?round, ?tick, ?shooter_steam_id, ?shooter_position_x, ?shooter_position_y, " +
                    "?shooter_position_z, ?shooter_weapon);";

                //run through all rows to save the weapon events.
                foreach (DataRow drWeaponEvent in dtWeaponEvents.Rows)
                {
                    //bind all parameters to the statement and execute.
                    MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                    cmdInsert.CommandText = sqlInsertFrag;
                    cmdInsert.Parameters.AddWithValue("?match_id", matchID);
                    cmdInsert.Parameters.AddWithValue("?round", drWeaponEvent["round"]);
                    cmdInsert.Parameters.AddWithValue("?tick", drWeaponEvent["tick"]);
                    cmdInsert.Parameters.AddWithValue("?shooter_steam_id", drWeaponEvent["shooter_steam_id"]);
                    cmdInsert.Parameters.AddWithValue("?shooter_position_x", drWeaponEvent["shooter_position_x"]);
                    cmdInsert.Parameters.AddWithValue("?shooter_position_y", drWeaponEvent["shooter_position_y"]);
                    cmdInsert.Parameters.AddWithValue("?shooter_position_z", drWeaponEvent["shooter_position_z"]);
                    cmdInsert.Parameters.AddWithValue("?shooter_weapon", drWeaponEvent["shooter_weapon"]);
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