using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PharmApp.DbConnector2;

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
            DbConnector2 OurConnection = new DbConnector2();

            OurConnection.DbConnect();

            Console.WriteLine(OurConnection.determineDropArray());


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new EntryForm());
            
            OurConnection.DbDisconnect();

           // conn.Close();
           // Console.WriteLine("Done.");



        }
    }
}
