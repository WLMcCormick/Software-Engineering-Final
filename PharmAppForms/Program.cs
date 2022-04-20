using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string connStr = "server=localhost;user=root;database=pharmacydb;port=3306;password=password";// to work on your machine make sure your running a mysql DB and have the UID and Pass set to your credentials 
            MySqlConnection conn = new MySqlConnection(connStr);

           

                try
                {
                    Console.WriteLine("Connecting to MySQL...");
                    conn.Open();
                    // Lines 23 - 42 are an example query it you wont see the cmd pop up since we are using winforms but the connection is good 
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







            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EntryForm());
           
            conn.Close();
            Console.WriteLine("Done.");



        }
    }
}
