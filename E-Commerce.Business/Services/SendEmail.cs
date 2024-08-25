using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using E_Commerce.Business.Interfaces;

namespace E_Commerce.Business.Services
{
    public class SendEmail : ISendEmail
    {
        public void Send(string from, string displayName, string to, string messageBody, string subject)
        {
            MailMessage mailMessage = new();
            mailMessage.From = new MailAddress(from, displayName);
            mailMessage.To.Add(new MailAddress(to));
            mailMessage.Subject = subject;
            mailMessage.Body = messageBody;
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("ilgarchsh@code.edu.az", "rdqs tkgi bntt lpyg");
            smtpClient.Send(mailMessage);
        }
        public void SendWithHtml(string from, string displayName, string to, string subject, AlternateView htmlView)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(from, displayName);
            mailMessage.To.Add(new MailAddress(to));
            mailMessage.Subject = subject;

            // Set the HTML view with inline images
            mailMessage.AlternateViews.Add(htmlView);

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("ilgarchsh@code.edu.az", "rdqs tkgi bntt lpyg");
            smtpClient.Send(mailMessage);
        }
    }
}

