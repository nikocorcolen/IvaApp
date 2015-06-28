using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using IvaApp.Droid;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

[assembly: Dependency(typeof(Email))]
namespace IvaApp.Droid
{
    class Email : IEmail
    {
        public void Send_Email()
        {
            //SmtpClient client = new SmtpClient();
            //string From = "";
            //string PassEmail = "";
            //string TextInFrom = "";
            //client = new SmtpClient("smtp.gmail.com", 587);//for Gmail
            //client.EnableSsl = true;//For Gmail
            //From = "nikocorcolen@gmail.com";
            //PassEmail = "nikoC0rc0l3n";
            string user = "nikocorcolen@gmail.com";
            string pass = "nikoC0rc0l3n";

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(user);
                mail.To.Add(user);
                mail.Subject = "TEST";
                mail.Body = "This is for testing SMTP mail from GMAIL";

                //***************
                var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                var filePath = Path.Combine(documentsPath, "temp.txt");
                //return System.IO.File.ReadAllText(filePath);
                //***************

                Attachment item = new Attachment(filePath);
                mail.Attachments.Add(item);
                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("nikocorcolen", pass);
                SmtpServer.EnableSsl = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                SmtpServer.Send(mail);
            }
            catch (Exception e)
            {
                Console.WriteLine("Ouch!" + e.ToString());
            }

        }
    }
}