using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Assignment_1
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string NewHash;
        byte[] Key;
        byte[] IV;
        string userID = null;

        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                Session["SSEmail"].ToString();
            }

        }

        private int checkPassword(string password)
        {
            int score = 0;

            // Score 0 very weak!
            // If length of password is less than 8 characters.
            if (password.Length < 8)
            {
                return 1;
            }
            else
            {
                score = 1;
            }

            // Score 2 Weak
            if (Regex.IsMatch(password, "[a-z]"))
            {
                score++;
            }

            // Score 3 Medium
            if (Regex.IsMatch(password, "[A-Z]"))
            {
                score++;
            }

            // Score 4 Medium
            if (Regex.IsMatch(password, "[0-9]"))
            {
                score++;
            }

            // Score 5 String
            if (Regex.IsMatch(password, "[!@#$%^&*()_+]"))
            {
                score++;
            }

            return score;
        }

        protected void btn_ChangePassword_Click(object sender, EventArgs e)
        {
            string email = Session["SSEmail"].ToString();

            int scores = checkPassword(tb_new_password.Text);

            string status = "";
            switch (scores)
            {
                case 1:
                    status = "Very Weak";
                    break;
                case 2:
                    status = "Weak";
                    break;
                case 3:
                    status = "Medium";
                    break;
                case 4:
                    status = "Strong";
                    break;
                case 5:
                    status = "Excellent";
                    break;
                default:
                    break;
            }
            lbl_pwdChecker.Text = "Status : " + status;
            if (scores < 4)
            {
                lbl_pwdChecker.ForeColor = Color.Red;
                return;
            }
            else
            {
                lbl_pwdChecker.ForeColor = Color.Green;
            }

            string currentPwd = tb_old_password.Text.ToString().Trim();

            string newPwd = tb_new_password.Text.ToString().Trim();

            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            //Fills array of bytes with a cryptographically strong sequence of random values.
            //rng.GetBytes(saltByte);
            //salt = Convert.ToBase64String(saltByte);

            SHA512Managed hashing = new SHA512Managed();

            //string dbHash = getDBHash(email);

            string dbSalt = getDBSalt(email);

            string currentPwdWithSalt = currentPwd + dbSalt;
            string newPwdWithSalt = newPwd + dbSalt;

            string oldPwdHash = getOldDBHash(email);

            byte[] newHashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(newPwdWithSalt));
            NewHash = Convert.ToBase64String(newHashWithSalt);

            byte[] currentHashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(currentPwdWithSalt));

            if (newPwdWithSalt == currentPwdWithSalt)
            {
                passChangeErrMsg.Text = "Cannot use the same password.";
                passChangeErrMsg.ForeColor = System.Drawing.Color.Red;
            }
            else if (NewHash.Equals(oldPwdHash))
            {
                passChangeErrMsg.Text = "Cannot use the same password as the first time or password is incorrect.";
                passChangeErrMsg.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                finalHash = Convert.ToBase64String(newHashWithSalt);
                Response.Redirect("SuccessPasswordChange.aspx", false);
                UpdateAccount(email, finalHash, dbSalt);
            }

            //finalHash = Convert.ToBase64String(newHashWithSalt);

            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;
        }

        public int UpdateAccount(string email, string finalHash, string salt)
        {
            SqlConnection connection = new SqlConnection(MYDBConnectionString);

            string sql = "UPDATE Account SET oldPasswordHash=@oldPasswordHash, passwordHash=@passwordHash, MinPasswordAge=@MinPasswordAge, MaxPasswordAge=@MaxPasswordAge, passwordSalt=@passwordSalt where emailAddress=@emailAddress";
            SqlCommand command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@emailAddress", email);
            command.Parameters.AddWithValue("@oldPasswordHash", getDBHash(email));
            command.Parameters.AddWithValue("@passwordHash", finalHash);
            command.Parameters.AddWithValue("@passwordSalt", salt);
            command.Parameters.AddWithValue("@MinPasswordAge", DateTime.Now.AddMinutes(1));
            command.Parameters.AddWithValue("@MaxPasswordAge", DateTime.Now.AddMinutes(5));

            connection.Open();
            int result = command.ExecuteNonQuery();

            connection.Close();
            return result;
        }

       
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

        protected string getOldDBHash(string userid)
        {
            string h = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "select oldPasswordHash FROM Account WHERE emailAddress=@emailAddress";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@emailAddress", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["oldPasswordHash"] != null)
                        {
                            if (reader["oldPasswordHash"] != DBNull.Value)
                            {
                                h = reader["oldPasswordHash"].ToString();
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
    }
}