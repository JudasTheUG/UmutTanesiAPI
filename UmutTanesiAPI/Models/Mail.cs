using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace UmutTanesiAPI.Models
{
    public class Mail
    {
        public void sendMail(string mailAdress, string newPass)
        {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("umuttanesiinfo@gmail.com");
                mail.To.Add(mailAdress);
                mail.Subject = "Password Change";
                mail.Body = newPass;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("umuttanesiinfo@gmail.com", "umuttanesi2020");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            
        }
    }
}