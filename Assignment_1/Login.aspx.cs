using System;
using System.Collections.Generic;
using System.Web;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace Assignment_1
{
    public partial class Login : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public class MyObject
        {
            public string success { get; set; }
            public List<string> ErrorMessage { get; set; }
        }

        protected void btn_login_Click(object sender, EventArgs e)
        {
            string pwd = tb_password.Text.ToString().Trim();
            string userid = tb_emailAddress.Text.ToString().Trim();


            DateTime dateTimeNow = DateTime.Now;
            DateTime maxPasswordAge = Convert.ToDateTime(getMaxPasswordAge(userid));
            int maxDuration = DateTime.Compare(dateTimeNow, maxPasswordAge);

            SHA512Managed hashing = new SHA512Managed();
            string dbHash = getDBHash(userid);
            string dbSalt = getDBSalt(userid);

            if (maxDuration <= 0)
            {
                try
                {
                    if (ValidateCaptcha())
                    {
                        if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0 && tb_emailAddress.Text.Trim().Equals(userid) && tb_password.Text.Trim().Equals(pwd))
                        {
                            string pwdWithSalt = pwd + dbSalt;
                            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
                            string userHash = Convert.ToBase64String(hashWithSalt);
                            if (userHash.Equals(dbHash))
                            {
                                Session["LoggedIn"] = tb_emailAddress.Text.Trim();

                                // Create a new GUID and save into the session
                                string guid = Guid.NewGuid().ToString();
                                Session["AuthToken"] = guid;

                                // Now create a new cookie with this guid value
                                Response.Cookies.Add(new HttpCookie("AuthToken", guid));

                                Session["UserID"] = userid;
                                Response.Redirect("Success.aspx", false);
                            }
                            else
                            {
                                lbl_errorMsg.ForeColor = System.Drawing.Color.Red;

                                Session["LoginCount"] = Convert.ToInt32(Session["LoginCount"]) + 1;
                                if (Convert.ToInt32(Session["LoginCount"]) >= 3)
                                {
                                    lbl_errorMsg.Text = DeactivateLoginAccount();
                                    btn_login.Enabled = false;
                                }

                                else
                                {
                                    lbl_errorMsg.Text = "Userid or password is not valid. Please try again.";
                                    lbl_errorMsg.ForeColor = System.Drawing.Color.Red;
                                }
                                //Response.Redirect("Login.aspx", false);
                            }
                        }

                        else
                        {
                            lbl_error_message.Text = "Wrong username or password.";
                            lbl_error_message.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }

            }

            else
            {
                lbl_error_message.Text = "Must change password after 5 mins.";
                lbl_error_message.ForeColor = System.Drawing.Color.Red;
                btn_resetPassword.Visible = true;
            }
        }

        protected void btn_reset_password_Click(object sender, EventArgs e)
        {
            string email = tb_emailAddress.Text.Trim();
            Session["SSEmail"] = email;
            Response.Redirect("EmailRequestResetPassword.aspx", false);
        }

        protected string DeactivateLoginAccount()
        {
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(MYDBConnectionString);
            string query = "select * from Account Where emailAddress='" + tb_emailAddress.Text + "';Update Account set Status=0 Where emailAddress='" + tb_emailAddress.Text + "';";
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return "Your Account is Locked. Please contact to admin.";
            }
            else
            {
                return "Enter email address does not exist in the application.";
            }
            con.Close();
        }

        //protected void btn_login_Click(object sender, EventArgs e)
        //{
        //    string pwd = tb_password.Text.ToString().Trim();
        //    string userid = tb_emailAddress.Text.ToString().Trim();

        //    SHA512Managed hashing = new SHA512Managed();
        //    string dbHash = getDBHash(userid);
        //    string dbSalt = getDBSalt(userid);

        //    try
        //    {
        //        if (dbSalt != null && dbSalt.Length > 0 && dbHash != null && dbHash.Length > 0)
        //        {
        //            string pwdWithSalt = pwd + dbSalt;
        //            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));
        //            string userHash = Convert.ToBase64String(hashWithSalt);
        //            if (userHash.Equals(dbHash))
        //            {
        //                Session["UserID"] = userid;
        //                Response.Redirect("Success.aspx", false);
        //            }
        //            else
        //            {
        //                lbl_errorMsg.Text = "Userid or password is not valid. Please try again.";
        //                //Response.Redirect("Login.aspx", false);
        //            }
        //        }

        //        else
        //        {
        //            lbl_error_message.Text = "Wrong username or password12221";
        //            lbl_error_message.ForeColor = System.Drawing.Color.Red;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.ToString());
        //    }

        //    finally
        //    {
        //        //if (ValidateCaptcha())
        //        //{
        //        // Check for username and password
        //        //if (tb_emailAddress.Text.Trim().Equals("test1") && tb_password.Text.Trim().Equals("test2"))

        //        if (tb_emailAddress.Text.Trim().Equals(userid) && tb_password.Text.Trim().Equals(pwd))
        //        {
        //            Session["LoggedIn"] = tb_emailAddress.Text.Trim();

        //            // Create a new GUID and save into the session
        //            string guid = Guid.NewGuid().ToString();
        //            Session["AuthToken"] = guid;

        //            // Now create a new cookie with this guid value
        //            Response.Cookies.Add(new HttpCookie("AuthToken", guid));

        //            Response.Redirect("Success.aspx", false);
        //        }
        //        else
        //        {
        //            lbl_error_message.Text = "Wrong username or password111";
        //            lbl_error_message.ForeColor = System.Drawing.Color.Red;
        //        }

        //        //}

        //    }
        //}

        protected string getDBHash(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PasswordHash FROM Account WHERE emailAddress=@emailAddress";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@emailAddress", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["PasswordHash"] != null)
                        {
                            if (reader["PasswordHash"] != DBNull.Value)
                            {
                                h = reader["PasswordHash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return h;
        }

        protected string getDBSalt(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select PASSWORDSALT FROM ACCOUNT WHERE emailAddress=@emailAddress";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@emailAddress", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["PASSWORDSALT"] != null)
                        {
                            if (reader["PASSWORDSALT"] != DBNull.Value)
                            {
                                s = reader["PASSWORDSALT"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;
        }

        protected string getMaxPasswordAge(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "Select MaxPasswordAge FROM Account WHERE emailAddress=@emailAddress";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@emailAddress", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["MaxPasswordAge"] != null)
                        {
                            if (reader["MaxPasswordAge"] != DBNull.Value)
                            {
                                s = reader["MaxPasswordAge"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { connection.Close(); }
            return s;

        }

        public bool ValidateCaptcha()
        {
            bool result = true;

            //When user submits recaptcha form, the user gets a response POST parameter.
            //captchaResponse consist of the user click pattern. Behaviour analytics! AI :)
            string captchaResponse = Request.Form["g-recaptcha-response"];

            //To send a GET request to Google along with the response and Secret Key.
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://www.google.com/recaptcha/api/siteverify?secret=6LfOZRcaAAAAALd7evSqK-VTIA5IGywYPM3QGn99 &response=" + captchaResponse);

            try
            {
                //Codes to receive the Reponse in JSON format from Google Server
                using (WebResponse wResponse = req.GetResponse())
                {
                    using (StreamReader readStream = new StreamReader(wResponse.GetResponseStream()))
                    {
                        //The response in JSON format
                        string jsonResponse = readStream.ReadToEnd();

                        //To show the JSON response string for learning purpose
                        //lbl_gScore.Text = jsonResponse.ToString();

                        JavaScriptSerializer js = new JavaScriptSerializer();

                        //Create jsonObject to handle the response e.g success or Error
                        //Deserialise Json
                        MyObject jsonObject = js.Deserialize<MyObject>(jsonResponse);

                        //Convert the string "False" to bool false or "True" to bool true
                        result = Convert.ToBoolean(jsonObject.success);
                    }
                }
                return result;
            }
            catch (WebException ex)
            {
                throw ex;
            }
        }       
    }
}
