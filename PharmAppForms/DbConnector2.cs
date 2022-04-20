using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmApp
{
    public class DbConnector2
    {

      // string connStr = "server=localhost;user=root;database=pharmacydb;port=3306;password=password";// to work on your machine make sure your running a
                                                                                                      // mysql DB and have the UID and Pass set to your credentials 
        static private MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=password");

        public void DbConnect()
        {

            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            


        }
        public string DbDisconnect()
        {
            conn.Close();
            return "done";

        }
        public  object DbtestQuery1()
        {
            // Lines 23 - 42 are an example query
            string sql = "SELECT `id` FROM `pharmacydb`.`hplc_values` where HPLC_values = 1;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
               return rdr[0];
            }
            rdr.Close();
            return "done";
        }





    }
}


