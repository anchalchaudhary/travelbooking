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

public partial class Registration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        SqlConnection connect = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
        try
        {
            string getFields = "insert into tblRegistration(Name,Email,PW,SecurityQuestion,Answer,Category_Id,Phone,DOB,City) values(@name, @email, @pw, @question, @answer, @category, @phone, @dob, @city)";
            SqlCommand cmd = new SqlCommand(getFields, connect);
            string encryptPassword = Encrypt(tbxPassword.Text.Trim());
            cmd.Parameters.AddWithValue("@name", tbxName.Text);
            cmd.Parameters.AddWithValue("@email", tbxEmail.Text);
            cmd.Parameters.AddWithValue("@pw", encryptPassword);
            cmd.Parameters.AddWithValue("@question", dropQues.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@answer", tbxAns.Text);
            cmd.Parameters.AddWithValue("@category", dropRoles.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@phone", tbxPhone.Text);
            cmd.Parameters.AddWithValue("@dob", tbxDOB.Text);
            cmd.Parameters.AddWithValue("@city", tbxCity.Text);
            connect.Open();
            cmd.ExecuteNonQuery();
            connect.Close();
            string message;
            if (dropRoles.SelectedValue.ToString() == "1")
            {
                Response.Redirect("Login.aspx");
            }
            else if (dropRoles.SelectedValue.ToString() == "2")
            {
                message = "Registration successful. Confirmation email sent.";
                SendActivationEmail();
                ClientScript.RegisterStartupScript(GetType(), "alert", "alert('" + message + "');", true);
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Write("Error: " + ex.ToString());
        }
    }
    private void SendActivationEmail()
    {
        try
        {
            using (MailMessage mm = new MailMessage("anchal.chaudhary329@gmail.com", tbxEmail.Text))
            {
                mm.Subject = "Account Activation";
                string body = "Hello " + tbxName.Text.Trim() + ", your account has been activated. Your login id is " + tbxEmail.Text + " and password is " + tbxPassword.Text;
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
        }
        catch (Exception ex)
        {
            Response.Write("Error : " + ex.ToString());
        }
    }
    private string Encrypt(string enteredPassword)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(enteredPassword);
        try
        {
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    enteredPassword = Convert.ToBase64String(ms.ToArray());
                }
            }
            
        }
        catch (Exception ex)
        {
            Response.Write("Error : " + ex.ToString());
        }
        return enteredPassword;
    }
}