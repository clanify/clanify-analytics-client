using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace clanify_analyzer_client.Database
{
    class TableDamage
    {
        //the property of the database connection.
        private MySqlConnection dbConnection = null;

        //constructor to initialize the object.
        public TableDamage(MySqlConnection dbConnection)
        {
            this.dbConnection = dbConnection;
        }

        //method to get a empty table for the damage.
        public DataTable getTableSchema()
        {
            DataTable dtDamage = new DataTable("frags");
            dtDamage.Columns.Add(new DataColumn("match_id", System.Type.GetType("System.Int64")));
            dtDamage.Columns.Add(new DataColumn("round", System.Type.GetType("System.Int16")));
            dtDamage.Columns.Add(new DataColumn("tick", System.Type.GetType("System.Int32")));
            dtDamage.Columns.Add(new DataColumn("victim_steam_id", System.Type.GetType("System.Int64")));
            dtDamage.Columns.Add(new DataColumn("victim_weapon", System.Type.GetType("System.String")));
            dtDamage.Columns.Add(new DataColumn("victim_position_x", System.Type.GetType("System.Decimal")));
            dtDamage.Columns.Add(new DataColumn("victim_position_y", System.Type.GetType("System.Decimal")));
            dtDamage.Columns.Add(new DataColumn("victim_position_z", System.Type.GetType("System.Decimal")));
            dtDamage.Columns.Add(new DataColumn("victim_hp", System.Type.GetType("System.Int16")));
            dtDamage.Columns.Add(new DataColumn("victim_health", System.Type.GetType("System.Int16")));
            dtDamage.Columns.Add(new DataColumn("victim_health_damage", System.Type.GetType("System.Int16")));
            dtDamage.Columns.Add(new DataColumn("victim_armor", System.Type.GetType("System.Int16")));
            dtDamage.Columns.Add(new DataColumn("victim_armor_damage", System.Type.GetType("System.Int16")));
            dtDamage.Columns.Add(new DataColumn("attacker_steam_id", System.Type.GetType("System.Int64")));
            dtDamage.Columns.Add(new DataColumn("attacker_weapon", System.Type.GetType("System.String")));
            dtDamage.Columns.Add(new DataColumn("attacker_position_x", System.Type.GetType("System.Decimal")));
            dtDamage.Columns.Add(new DataColumn("attacker_position_y", System.Type.GetType("System.Decimal")));
            dtDamage.Columns.Add(new DataColumn("attacker_position_z", System.Type.GetType("System.Decimal")));
            dtDamage.Columns.Add(new DataColumn("attacker_hp", System.Type.GetType("System.Int16")));
            dtDamage.Columns.Add(new DataColumn("hitgroup", System.Type.GetType("System.String")));
            return dtDamage;
        }

        //method to save a DataTable with rows to the database.
        public bool saveTable(DataTable dtDamage, Int64 matchID)
        {
            try
            {
                //open the connection to insert some data.
                this.dbConnection.Open();

                //create the delete statement.
                string sqlDeleteDamage = "DELETE FROM `damage` WHERE match_id = ?match_id;";

                //bind all the parameters to the statement.
                MySqlCommand cmdDelete = this.dbConnection.CreateCommand();
                cmdDelete.CommandText = sqlDeleteDamage;
                cmdDelete.Parameters.AddWithValue("?match_id", matchID);
                cmdDelete.ExecuteNonQuery();

                //create the update statement.
                string sqlInsertDamage = "INSERT INTO `damage` (`match_id`, `round`, `tick`, `victim_steam_id`, `victim_weapon`, " +
                    "`victim_position_x`, `victim_position_y`, `victim_position_z`, `victim_hp`, `victim_health`, `victim_health_damage`, " +
                    "`victim_armor`, `victim_armor_damage`, `attacker_steam_id`, `attacker_weapon`, `attacker_position_x`, " +
                    "`attacker_position_y`, `attacker_position_z`, `attacker_hp`, `hitgroup`) VALUES (?match_id, ?round, ?tick, " +
                    "?victim_steam_id, ?victim_weapon, ?victim_position_x, ?victim_position_y, ?victim_position_z, ?victim_hp, " +
                    "?victim_health, ?victim_health_damage, ?victim_armor, ?victim_armor_damage, ?attacker_steam_id, ?attacker_weapon, " + 
                    "?attacker_position_x, ?attacker_position_y, ?attacker_position_z, ?attacker_hp, ?hitgroup);";

                //run through all rows to save.
                foreach (DataRow drDamage in dtDamage.Rows)
                {
                    //bind all the parameters to the statement.
                    MySqlCommand cmdInsert = this.dbConnection.CreateCommand();
                    cmdInsert.CommandText = sqlInsertDamage;
                    cmdInsert.Parameters.AddWithValue("?match_id", matchID);
                    cmdInsert.Parameters.AddWithValue("?round", drDamage["round"]);
                    cmdInsert.Parameters.AddWithValue("?tick", drDamage["tick"]);
                    cmdInsert.Parameters.AddWithValue("?victim_steam_id", drDamage["victim_steam_id"]);
                    cmdInsert.Parameters.AddWithValue("?victim_weapon", drDamage["victim_weapon"]);
                    cmdInsert.Parameters.AddWithValue("?victim_position_x", drDamage["victim_position_x"]);
                    cmdInsert.Parameters.AddWithValue("?victim_position_y", drDamage["victim_position_y"]);
                    cmdInsert.Parameters.AddWithValue("?victim_position_z", drDamage["victim_position_z"]);
                    cmdInsert.Parameters.AddWithValue("?victim_hp", drDamage["victim_hp"]);
                    cmdInsert.Parameters.AddWithValue("?victim_health", drDamage["victim_health"]);
                    cmdInsert.Parameters.AddWithValue("?victim_health_damage", drDamage["victim_health_damage"]);
                    cmdInsert.Parameters.AddWithValue("?victim_armor", drDamage["victim_armor"]);
                    cmdInsert.Parameters.AddWithValue("?victim_armor_damage", drDamage["victim_armor_damage"]);
                    cmdInsert.Parameters.AddWithValue("?attacker_steam_id", drDamage["attacker_steam_id"]);
                    cmdInsert.Parameters.AddWithValue("?attacker_weapon", drDamage["attacker_weapon"]);
                    cmdInsert.Parameters.AddWithValue("?attacker_position_x", drDamage["attacker_position_x"]);
                    cmdInsert.Parameters.AddWithValue("?attacker_position_y", drDamage["attacker_position_y"]);
                    cmdInsert.Parameters.AddWithValue("?attacker_position_z", drDamage["attacker_position_z"]);
                    cmdInsert.Parameters.AddWithValue("?attacker_hp", drDamage["attacker_hp"]);
                    cmdInsert.Parameters.AddWithValue("?hitgroup", drDamage["hitgroup"]);
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