﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Security.Cryptography;

public partial class forgotPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnForgotPassword_Click(object sender, EventArgs e)
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
        string ques = "select SecurityQuestion from tblRegistration where Email='" + tbxEnterEmail.Text + "'";
        string ans = "select Answer from tblRegistration where Email='" + tbxEnterEmail.Text + "'";
        SqlCommand cmdQues = new SqlCommand(ques, connect);
        SqlCommand cmdAns = new SqlCommand(ans, connect);
        connect.Open();
        string secQues = cmdQues.ExecuteScalar().ToString().Trim();
        connect.Close();
        connect.Open();
        string secAns = cmdAns.ExecuteScalar().ToString().Trim();
        connect.Close();
        if (secQues != dropQues.SelectedValue || secAns != tbxAns.Text.ToString().Trim())
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Incorrect Security Question-Answer');", true);
        }
        /*string stringQuestion;
        if (Convert.ToInt32(secQues) == 0)
        {
            stringQuestion = "Favourite Color";
        }
        if (Convert.ToInt32(secQues) == 1)
        {
            stringQuestion = "Favourite Animal";
        }
        if (Convert.ToInt32(secQues) == 2)
        {
            stringQuestion = "Favourite Movie";
        }
        else
        {
            stringQuestion = "Favourite Actor";
        }
        int b = 1;

        if (string.Compare(stringQuestion, dropQues.Text) != 0 || string.Compare(secAns, tbxAns.Text) != 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Incorrect Question Answer Combination');", true);
        }*/
        else
        {
            try
            {
                string password = "select PW from tblRegistration where Email='" + tbxEnterEmail.Text + "'";
                SqlCommand cmd = new SqlCommand(password, connect);
                connect.Open();
                string pass = cmd.ExecuteScalar().ToString().Trim();
                connect.Close();
                string loginPassword = Decrypt(pass);
                using (MailMessage mm = new MailMessage("anchal.chaudhary329@gmail.com", tbxEnterEmail.Text))
                {
                    mm.Subject = "Account Access";
                    string body = "Hello, your password is " + loginPassword;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("anchal.chaudhary329@gmail.com", "Google.Anchal");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Check your Email to see your password');", true);
                Response.Redirect("Login.aspx");
            }
            catch (Exception ex)
            {
                Response.Write("Error : " + ex.ToString());
            }
        }
    }
    private string Decrypt(string decryptedPassword)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] encriptBytes = Convert.FromBase64String(decryptedPassword);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(encriptBytes, 0, encriptBytes.Length);
                    cs.Close();
                }
                decryptedPassword = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return decryptedPassword;
    }
}