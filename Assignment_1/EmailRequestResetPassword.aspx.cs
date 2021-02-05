using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Assignment_1
{
    public partial class EmailRequestResetPassword : System.Web.UI.Page
    {
        string MYDBConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MYDBConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_SendEmail_Click(object sender, EventArgs e)
        {
            string sendFromEmail = "gearlab2021@gmail.com";
            string sendToPassword = "ineedabreakfromsch";
            string guid = Guid.NewGuid().ToString();
            string email = tb_emailAddress.Text.Trim();
            Session["SSEmail"] = email;

            DateTime timeNow = DateTime.Now;
            DateTime minPasswordAge = Convert.ToDateTime(getMinPasswordAge(email));
            int minDuration = DateTime.Compare(timeNow, minPasswordAge);

            if (minDuration >= 0)
            {
                passChangeErrMsg.Text = "Email successfully sent.";
                passChangeErrMsg.ForeColor = System.Drawing.Color.Green;

                try
                {
                    MailMessage mailMsg = new MailMessage("gearlab2021@gmail.com", email);

                    mailMsg.Subject = "Reset your password";
                    mailMsg.Body = "Please click this link to reset your Account password. <br/> https://localhost:44309/ResetPassword.aspx?Uuid=" + guid;

                    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                    NetworkCredential credentials = new NetworkCredential(sendFromEmail, sendToPassword);
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.EnableSsl = true;

                    smtpClient.Credentials = credentials;
                    if (mailMsg != null)
                    {
                        smtpClient.Send(mailMsg);
                        System.Diagnostics.Debug.WriteLine("Email Successfully sent.");
                    }
                }
                catch (Exception ex)
                {
                    //regErrorMsg.Text = ex.ToString();
                }
            }

            else
            {
                passChangeErrMsg.Text = "Cannot change password within 1 mins from the last change of password or after the account has been created.";
                passChangeErrMsg.ForeColor = System.Drawing.Color.Red;           
            }
        }

        protected string getMinPasswordAge(string userid)
        {
            string s = null;
            SqlConnection connection = new SqlConnection(MYDBConnectionString);
            string sql = "Select MinPasswordAge FROM Account WHERE emailAddress=@emailAddress";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@emailAddress", userid);
            try
            {
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["MinPasswordAge"] != null)
                        {
                            if (reader["MinPasswordAge"] != DBNull.Value)
                            {
                                s = reader["MinPasswordAge"].ToString();
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