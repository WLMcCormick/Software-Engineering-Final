using MySqlConnector;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlTypes;

namespace PharmApp
{
    public class DbConnector2
    {
        MySqlDataAdapter adapt;

      // string connStr = "server=localhost;user=root;database=pharmacydb;port=3306;password=password";// to work on your machine make sure your running a
      // mysql DB and have the UID and Pass set to your credentials 
        public string getHPLCValues()
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" +
                    "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return "done";
                }

            }

        }

        public DataTable getReport(string Reportid)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" +
                "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {

                    DataTable dt = new DataTable();
                    
                    string sql = "SELECT `reports`.`ReportID`,`reports`.`status`," +
                    "`reports`.`time`,`reports`.`RL`,`reports`.`QL`," +
                    "`reports`.`RSum`,`reports`.`error`" +
                    "FROM `pharmacydb`.`reports`WHERE `ReportID` = " + Reportid + ";";
                    adapt = new MySqlDataAdapter(sql, conn);
                    adapt.Fill(dt);
                    return dt;
                    //MySqlCommand cmd = new MySqlCommand(sql, conn);
                    //MySqlDataReader rdr = cmd.ExecuteReader();

                    //while (rdr.Read())
                    //{
                    //    DateTime myDateTime = DateTime.Parse(rdr["time"].ToString());
                    //    string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    //    Console.WriteLine(rdr["ReportID"].ToString() + " " + rdr["status"].ToString() +
                    //        " " + sqlFormattedDate + " " + rdr["RL"].ToString() + " " +
                    //        rdr["QL"].ToString() + " " + rdr["RSum"].ToString() + " " + rdr["error"].ToString());
                    //}
                    //rdr.Close();
                    //return "done";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }

            }
        }
        public string[] getReview()
        {

            //Returns reportIDs with a status of "Needs Review"
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" + "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {
         
                    string[] reportIDS = new string[determineDropArray()];
                    var reportForm = Application.OpenForms["ReportForm"];
                    string sql = "SELECT `reports`.`ReportID` FROM `pharmacydb`.`reports` WHERE `status` = 'Corrections Needed'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    int count = 0;
                    while (rdr.Read())
                    {
                        if(rdr[0] != null)
                        {
                            reportIDS[count] = rdr["ReportID"].ToString();
                            count++;
                        }
                    }
                    rdr.Close();
                    return reportIDS;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }

            }
        }
        public string getFinal()
        {

            //Returns reportIDs with a status of "Finalized"
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" + "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {
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
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
        }
        public string[] getCorrections()
        {
            //Returns reportIDs with a status of "Corrections Needed"
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" + "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {

                    string[] reportIDS = new string[determineDropArrayQC()];
                    var reportForm = Application.OpenForms["ReportForm"];
                    string sql = "SELECT `reports`.`ReportID` FROM `pharmacydb`.`reports` WHERE `status` = 'Needs Review'";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    int count = 0;
                    while (rdr.Read())
                    {
                        if (rdr[0] != null)
                        {
                            reportIDS[count] = rdr["ReportID"].ToString();
                            count++;
                        }
                    }
                    rdr.Close();
                    return reportIDS;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }

            }
        }

        public string newReport(string rL, string qL)
        {
            //Method to add new report entry into report table
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" +
                "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {
                    //CALL TO FIND RSUM 
                    DateTime time = new DateTime();
                    time = DateTime.Now;
                    Console.WriteLine(time);
                    
                    string sql = "INSERT INTO pharmacydb.reports (status,time, RL, QL) VALUES (@status,@time, @RL, @QL)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@status", "Needs Review");
                    cmd.Parameters.AddWithValue("@time", time);
                    cmd.Parameters.AddWithValue("@RL", rL);
                    cmd.Parameters.AddWithValue("@QL", qL);
                    cmd.ExecuteNonQuery();
                    sql = "SELECT `ReportID` FROM `reports` ORDER BY `ReportID` DESC LIMIT 1";
                    cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    string reportID = "";
                    while (rdr.Read())
                    {
                        reportID = rdr[0].ToString();  
                    }
                    rdr.Close();
                    //Call to Calc
                    return reportID;

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
        }

        public string getHighlightedIDSAboveQL(string qL)
        {
            //Returns IDS for hplc above qL
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" +
                "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {
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
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
        }
        public string getHighlightedIDSInbetween(string qL, string rL)
        {
            //Returns IDS for hplc between qL and rL
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" +
                "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {
                    string sql = "SELECT id FROM pharmacydb.hplc_values WHERE HPLC_values < " + qL + " AND HPLC_values > " + rL + ";";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Console.WriteLine(rdr["id"].ToString());
                    }
                    rdr.Close();
                    return "done";
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
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


        public int determineDropArray()
        {
            //Returns Count for drop down length for reports
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" +
                "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {
                    string sql = "SELECT COUNT(`reports`.`ReportID`) FROM `pharmacydb`.`reports` WHERE `status` = 'Corrections Needed';";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        string total = rdr[0].ToString();
                        if (total.Equals("0"))
                        {
                            return 1;
                        }
                        else
                        {
                            int totalConvert;
                            int.TryParse(total, out totalConvert);
                            return totalConvert;
                        }
                        
                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }

        }
        public int determineDropArrayQC()
        {
            //Returns Count for drop down length for reports
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" +
                "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {
                    string sql = "SELECT COUNT(`reports`.`ReportID`) FROM `pharmacydb`.`reports` WHERE `status` = 'Needs Review';";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        string total = rdr[0].ToString();
                        if (total.Equals("0"))
                        {
                            return 1;
                        }
                        else
                        {
                            int totalConvert;
                            int.TryParse(total, out totalConvert);
                            return totalConvert;
                        }

                    }
                    return 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }

        }

        public void updateRLQL(string RL, string QL, string RID)
        {
            using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;port=3306;password=Daisy23**;" +
                "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
            {
                conn.Open();
                try
                {
                    string sql = "UPDATE reports SET `RL` = @RL, `QL` = @QL, `status` = @status WHERE `ReportID` = @RID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@RL", RL);
                    cmd.Parameters.AddWithValue("@QL", QL);
                    cmd.Parameters.AddWithValue("@status", "Needs Review");
                    cmd.Parameters.AddWithValue("@RID", RID);
                    cmd.ExecuteNonQuery();
                    

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
        }




    }
}


