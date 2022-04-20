using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmApp
{
    static class DbConnector
    {

        string connStr = "server=localhost;user=root;database=pharmacydb;port=3306;password=password";// to work on your machine make sure your running a mysql DB and have the UID and Pass set to your credentials 
        MySqlConnection conn = new MySqlConnection(connStr);
        
        public static string DbConnect()
        {
           
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                // Lines 23 - 42 are an example query
                string sql = "SELECT `id` FROM `pharmacydb`.`hplc_values` where HPLC_values = 1;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0]);
                }
                rdr.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

          
      
        }
        public static string DbDisconnect()
        {
            conn.Close();
            Console.WriteLine("Done.");

        }
        
    }


            
    