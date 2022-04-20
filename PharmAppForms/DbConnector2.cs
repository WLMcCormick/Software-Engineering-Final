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
        static private MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" +
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
        public  string getHPLCValues()
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

        public string getReport(string Reportid)
        {
            string sql = "SELECT `reports`.`ReportID`,`reports`.`status`," +
                "`reports`.`time`,`reports`.`RL`,`reports`.`QL`," +
                "`reports`.`RSum`,`reports`.`error`" +
                "FROM `pharmacydb`.`reports`WHERE `ReportID` = " + Reportid + ";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            
            while (rdr.Read())
            {
                DateTime myDateTime = DateTime.Parse(rdr["time"].ToString());
                string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine(rdr["ReportID"].ToString() + " " + rdr["status"].ToString()+
                    " " + sqlFormattedDate + " " + rdr["RL"].ToString() + " " + 
                    rdr["QL"].ToString() + " " + rdr["RSum"].ToString() + " " + rdr["error"].ToString());
            }
            rdr.Close();
            return "done";
        }
        public string getReview()
        {
            //Returns reportIDs with a status of "Needs Review"
            string sql = "SELECT `reports`.`ReportID` FROM `pharmacydb`.`reports` WHERE `status` = 'Needs Review'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr["ReportID"].ToString());
            }
            rdr.Close();
            return "done";
        }
        public string getFinal()
        {
            //Returns reportIDs with a status of "Finalized"
            string sql = "SELECT `reports`.`ReportID` FROM `pharmacydb`.`reports` WHERE `status` = 'Finalized'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr["ReportID"].ToString());
            }
            rdr.Close();
            return "done";
        }
        public string getCorrections()
        {
            //Returns reportIDs with a status of "Corrections Needed"
            string sql = "SELECT `reports`.`ReportID` FROM `pharmacydb`.`reports` WHERE `status` = 'Corrections Needed'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Console.WriteLine(rdr["ReportID"].ToString());
            }
            rdr.Close();
            return "done";
        }

        public string newReport(string rL, string qL)
        {
            //Method to add new report entry into report table
            return "done";
        }

        public string getHighlightedIDS(string rL, string qL)
        {
            //Returns IDS for hplc values between RL and QL
            string sql = "SELECT id FROM pharmacydb.hplc_values WHERE HPLC_values > " + qL + ";";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine(rdr["id"].ToString());
            }
            rdr.Close();
            return "done";
        }

        public string getReportIDbyStatus(string status)
        {
            //Returns reportIDS given a status "Finalized" , "Needs Review", "Corrections Needed"
            return "done";
        }

        public string updateReportStatus(string rID, string status)
        {
            //Updates a report status attribute from rID to the passed status
            return "done";
        }

        public string updateErrorStatus(string rID, string error)
        {
            //Updates a error attribute of the given report based off rID with the passed error
            return "done";
        }






    }
}


