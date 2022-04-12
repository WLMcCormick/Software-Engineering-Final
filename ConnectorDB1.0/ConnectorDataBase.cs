using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectorDataBase
{
    class ConnectorDataBase
    {
        static void Main(string[] args)
        {
            // The following code Connect to the database
            // then querys to make sure it was succesful 
            //then closes the connection
            // the Closing of connection can be moved to when the program is closed
            // and the query can be removed it is just an example
            string connStr = "server=localhost;user=root;database=pharmacydb;port=3306;password=password";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT `id` FROM `pharmacydb`.`hplc_values` where HPLC_values = 1;";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Console.WriteLine(rdr[0] );
                }
                rdr.Close();
               
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            conn.Close();
            Console.WriteLine("Done.");
        }



    }
    }

