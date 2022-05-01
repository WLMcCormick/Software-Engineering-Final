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
        const string pwd = "Daisy23**";
        public MySqlConnectionStringBuilder connBuilder()
        {
            MySqlConnectionStringBuilder connString = new MySqlConnectionStringBuilder();
            connString.Server = "localhost";
            connString.Port = 3306;
            connString.UserID = "root";
            connString.Password=pwd;
            connString.Database = "pharmacydb";
            return connString;
        }

       

        public void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            EntryForm.instance.Show();
        }


        public double[] getHPLCValues()
        {
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                
                conn.Open();
                try
                {

                    double[] reportIDS = new double[determineDataArrayHPLC() * 2];
                    var reportForm = Application.OpenForms["ReportForm"];
                    string sql = "SELECT * FROM pharmacydb.hplc_values;";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    int count = 0;
                    while (rdr.Read())
                    {
                        if (rdr[0] != null)
                        {
                            reportIDS[count] = Convert.ToDouble(rdr[0]);
                            count++;
                        }
                        if(rdr[1] != null)
                        {
                            reportIDS[count] = Convert.ToDouble(rdr[1]);
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
        public float[] GetRLQL(string reportId)
        {
            float rl = 0;
            float ql = 0;

            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                conn.Open();
                try
                {
                    //get rl
                    string rl_sql = "SELECT `reports`.`RL` FROM `pharmacydb`.`reports`WHERE `ReportID` = " + reportId + ";";
                    MySqlCommand cmd = new MySqlCommand(rl_sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (rdr[0] != null)
                        {
                            rl = rdr.GetFloat(0);
                        }
                    }
                    rdr.Close();

                    //get ql
                    rl_sql = "SELECT `reports`.`QL` FROM `pharmacydb`.`reports`WHERE `ReportID` = " + reportId + ";";
                    cmd = new MySqlCommand(rl_sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (rdr[0] != null)
                        {
                            ql = rdr.GetFloat(0);
                        }
                    }
                    rdr.Close();

                    //add them to array and return
                    float[] rlql = { rl, ql };
                    return rlql;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }

        public DataTable getReports()
        {
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                conn.Open();
                try
                {

                    string sql = "SELECT ReportID, status, time FROM pharmacydb.reports ORDER BY CASE " +
                        "WHEN status = 'Needs Review' THEN 1 " +
                        "WHEN status = 'Needs Corrections' THEN 2 " +
                        "ELSE 3 END" +
                        ";";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(rdr);
                    return dt;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
        }

        public DataTable getReport(string Reportid)
        {
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
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

                    //get the rl value;
                    float rl = 0;
                    string rl_sql = "SELECT `reports`.`RL` FROM `pharmacydb`.`reports`WHERE `ReportID` = " + Reportid + ";";
                    MySqlCommand cmd = new MySqlCommand(rl_sql, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (rdr[0] != null)
                        {
                            rl = rdr.GetFloat(0);
                        }
                    }
                    rdr.Close();
                    

                    //get all the hplc values that are above the rl, goes in column 3
                    float hplc_sum = 0;
                    string hplc_sql = "SELECT `hplc_values`.`HPLC_values` FROM `hplc_values` WHERE `hplc_values`.`HPLC_values` > " + rl + ";";
                    cmd = new MySqlCommand(hplc_sql, conn);
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        if (rdr[0] != null)
                        {
                   
                            hplc_sum += rdr.GetFloat(0);
                        }
                    }
                    rdr.Close();

                    //display the sum in the box
                    dt.Rows[0][5] = hplc_sum.ToString();
                    return dt;
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
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
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
        public string[] getFinal()
        {

            //Returns reportIDs with a status of "Finalized"
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                conn.Open();
                try
                {
                    string[] reportIDS = new string[determineFinalArray()];
                    var reportForm = Application.OpenForms["ReportForm"];
                    string sql = "SELECT `reports`.`ReportID` FROM `pharmacydb`.`reports` WHERE `status` = 'Finalized'";
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
        public string[] getCorrections()
        {
            //Returns reportIDs with a status of "Corrections Needed"
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
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
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                conn.Open();
                try
                {
                    //CALL TO FIND RSUM 
                    DateTime time = new DateTime();
                    time = DateTime.Now;
         
                    
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
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
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
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
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

        public void updateReportStatus(string rID, string status)
        {
            //Updates a report status attribute from rID to the passed status
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                conn.Open();
                try
                {
                    string sql = "UPDATE reports SET `status` = @status WHERE `ReportID` = @RID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@RID", rID);
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
        }

        public void updateErrorStatus(string rID, string error)
        {
            //Updates a error attribute of the given report based off rID with the passed error
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                conn.Open();
                try
                {
                    string sql = "UPDATE reports SET `error` = @error, `status` = @status WHERE `ReportID` = @RID";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@RID", rID);
                    cmd.Parameters.AddWithValue("@error", error);
                    cmd.Parameters.AddWithValue("@status", "Corrections Needed");
                    cmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
        }


        public int determineDropArray()
        {
            //Returns Count for drop down length for reports QA side
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                conn.Open();
                try
                {
                    string sql = "SELECT COUNT(`reports`.`ReportID`) FROM `reports` WHERE `status` = 'Corrections Needed';";
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
        public int determineFinalArray()
        {
            //Returns Count for drop down length for reports QA side
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                conn.Open();
                try
                {
                    string sql = "SELECT COUNT(`reports`.`ReportID`) FROM `pharmacydb`.`reports` WHERE `status` = 'Finalized';";
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
            //Returns Count for drop down length for reports QC side
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
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
        public int determineDataArrayHPLC()
        {
            //Returns Count for hplc elements in db
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
            {
                conn.Open();
                try
                {
                    string sql = "SELECT COUNT(`hplc_values`.`id`) FROM `pharmacydb`.`hplc_values`;";
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
            using (MySqlConnection conn = new MySqlConnection(connBuilder().ToString()))
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


// string connStr = "server=localhost;user=root;database=pharmacydb;password=password";// to work on your machine make sure your running a
// mysql DB and have the UID and Pass set to your credentials 

//public List<string> getReportIds()
//{
//    using (MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=pharmacydb;password=password1;" +
//        "ConvertZeroDateTime=True;AllowZeroDateTime=True;"))
//    {
//        conn.Open();
//        try
//        {

//            DataTable dt = new DataTable();

//            string sql = "SELECT reportID FROM pharmacydb.reports;";

//            MySqlCommand cmd = new MySqlCommand(sql, conn);
//            MySqlDataReader rdr = cmd.ExecuteReader();
//            List<string> ids = new List<string>();
//            while (rdr.Read())
//            {
//                ids.Add(rdr[0].ToString());
//            }
//            return ids;
//        }
//        catch (Exception ex)
//        {
//            throw new Exception(ex.ToString());

//        }

//    }
//}



