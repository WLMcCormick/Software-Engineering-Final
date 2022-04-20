﻿using MySqlConnector;
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
        static private MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=password;" +
            "ConvertZeroDateTime=True;AllowZeroDateTime=True;");

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
        public  string SelectAll()
        {
            string sql = "SELECT * FROM pharmacydb.hplc_values;";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
               Console.WriteLine(rdr["id"].ToString() + " " + rdr["HPLC_values"].ToString());
            }
            rdr.Close();
            return "done";
        }

        public string DbtestQuery2(string Reportid)
        {
            string sql = "SELECT `reports`.`ReportID`,`reports`.`status`," +
                "`reports`.`time`,`reports`.`RL`,`reports`.`QL`," +
                "`reports`.`RSum`,`reports`.`error`" +
                "FROM `pharmacydb`.`reports`WHERE `ReportID` = " + Reportid+ ";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss.fff");
            while (rdr.Read())
            {
                Console.WriteLine(rdr["ReportID"].ToString() + " " + rdr["status"].ToString()+
                    " " + myDateTime.ToString(rdr["time"].ToString()) + " " + rdr["RL"].ToString() + " " + 
                    rdr["QL"].ToString() + " " + rdr["RSum"].ToString() + " " + rdr["error"].ToString());
            }
            rdr.Close();
            return "done";
        }





    }
}


