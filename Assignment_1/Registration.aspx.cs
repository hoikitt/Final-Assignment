using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Text.RegularExpressions;
using System.Drawing;

using System.Security.Cryptography;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Assignment_1
{

    public partial class Registration : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        static string finalHash;
        static string salt;
        byte[] Key;
        byte[] IV;

        protected void Page_Load(object sender, EventArgs e)
        {

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

        protected void btn_checkPassword_Click(object sender, EventArgs e)
        {

            // Implement codes for the button event
            // Extract data from textbox
            int scores = checkPassword(tb_password.Text);

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

            string pwd = tb_password.Text.ToString().Trim();
            string email = tb_emailAddress.Text.ToString().Trim();
            lbl_xss_email.Text = HttpUtility.HtmlEncode(email);
            //string pwd_confirm = tb_password_confirm.Text.ToString().Trim();

            //Generate random "salt"
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] saltByte = new byte[8];

            //Fills array of bytes with a cryptographically strong sequence of random values.
            rng.GetBytes(saltByte);
            salt = Convert.ToBase64String(saltByte);

            SHA512Managed hashing = new SHA512Managed();

            string pwdWithSalt = pwd + salt;
            byte[] plainHash = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            byte[] hashWithSalt = hashing.ComputeHash(Encoding.UTF8.GetBytes(pwdWithSalt));

            finalHash = Convert.ToBase64String(hashWithSalt);

            RijndaelManaged cipher = new RijndaelManaged();
            cipher.GenerateKey();
            Key = cipher.Key;
            IV = cipher.IV;

            string dbEmail = getEmail(email);

            if (dbEmail.Equals(dbEmail))
            {
                lbl_xss_email.Text = "Email existed already.";
                lbl_xss_email.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Response.Redirect("SuccessRegistration.aspx?Email=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(lbl_xss_email.Text)), false);
                createAccount();
            }

            //if (txtVerificationCode.Text.ToLower() == Session["CaptchaVerify"].ToString())
            //{
            //    Response.Redirect("SuccessCaptcha.aspx", false);
            //    createAccount();
            //}
            //else
            //{
            //    lblCaptchaMessage.Text = "You have entered Wrong Captcha. Please enter correct Captcha!";
            //    lblCaptchaMessage.ForeColor = System.Drawing.Color.Red;
            //}

            //createAccount();

            //Response.Redirect("SuccessRegistration.aspx?Email=" + HttpUtility.UrlEncode(HttpUtility.HtmlEncode(lbl_xss_email.Text)), false);
            //createAccount();

        }

        public void createAccount()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(MYDBConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Account VALUES(@adminNo, @firstName, @lastName, @creditCard, @emailAddress, @passwordHash, @oldPasswordHash, @passwordSalt, @dateTimeRegistered, @MinPasswordAge, @MaxPasswordAge, @mobileVerified, @emailVerified, @dateOfBirth, @IV, @Key, @Status)"))
                 
                    {
                        using (SqlDataAdapter sda = new SqlDataAdapter())
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@adminNo", tb_adminNo.Text.Trim());
                            cmd.Parameters.AddWithValue("@firstName", tb_firstName.Text.Trim());
                            cmd.Parameters.AddWithValue("@lastName", tb_lastName.Text.Trim());
                            //cmd.Parameters.AddWithValue("@creditCard", encryptData(tb_creditCard.Text.Trim()));
                            cmd.Parameters.AddWithValue("@creditCard", Convert.ToBase64String(encryptData(tb_creditCard.Text.Trim())));
                            cmd.Parameters.AddWithValue("@emailAddress", HttpUtility.HtmlEncode(tb_emailAddress.Text.Trim()));
                            cmd.Parameters.AddWithValue("@PasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@oldPasswordHash", finalHash);
                            cmd.Parameters.AddWithValue("@PasswordSalt", salt);
                            cmd.Parameters.AddWithValue("@MinPasswordAge", DateTime.Now.AddMinutes(1));
                            cmd.Parameters.AddWithValue("@MaxPasswordAge", DateTime.Now.AddMinutes(5));
                            cmd.Parameters.AddWithValue("@DateTimeRegistered", DateTime.Now);
                            cmd.Parameters.AddWithValue("@MobileVerified", DBNull.Value);
                            cmd.Parameters.AddWithValue("@EmailVerified", DBNull.Value);
                            cmd.Parameters.AddWithValue("@dateOfBirth", tb_dateOfBirth.Text.Trim());

                            cmd.Parameters.AddWithValue("@IV", Convert.ToBase64String(IV));
                            cmd.Parameters.AddWithValue("@Key", Convert.ToBase64String(Key));
                            cmd.Parameters.AddWithValue("@Status", 1);

                            cmd.Connection = con;
                            con.Open();
                            cmd.ExecuteNonQuery();
                            con.Close();

                            //try
                            //{
                            //    con.Open();
                            //    cmd.ExecuteNonQuery();
                            //    //con.Close();
                            //}
                            //catch (Exception ex)
                            //{
                            //    // Throw new Exeception (ex.ToString());
                            //    lbl_exceptionError.Text = ex.ToString();
                            //}
                            //finally
                            //{
                            //    con.Close();
                            //}
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        protected string getEmail(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "Select emailAddress FROM Account WHERE emailAddress='" + tb_emailAddress.Text + "'";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@emailAddress", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["emailAddress"] != null)
                        {
                            if (reader["emailAddress"] != DBNull.Value)
                            {
                                s = reader["emailAddress"].ToString();
                            }
                            else
                            {
                                s = "Email already exists in database";
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

        protected byte[] encryptData(string data)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = IV;
                cipher.Key = Key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0,
               plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }
    }
}
