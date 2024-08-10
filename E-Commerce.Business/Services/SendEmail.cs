using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using E_Commerce.Business.Interfaces;

namespace Web_Api.Business.Services
{
	public class SendEmail: ISendEmail
    {
        public void Send(string from, string displayName, string to, string messageBody, string subject)
        {
            try
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
                smtpClient.Credentials = new NetworkCredential("ilgarchsh@code.edu.az", "pjbw wcmp zhes wqtp");
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("something went wrong");
            }
        }
        public void SendWithHtml(string from, string displayName, string to, string subject, AlternateView htmlView)
        {
            try
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
                smtpClient.Credentials = new NetworkCredential("ilgarchsh@code.edu.az", "pjbw wcmp zhes wqtp");
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("Something went wrong", ex);
            }
        }
    }
}

