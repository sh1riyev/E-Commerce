using System;
using System.Net.Mail;
using E_Commerce.Business.Interfaces;

namespace E_Commerce.Business.Services
{
	public class SendEmail : ISendEmail
	{
		public SendEmail()
		{
		}

        public void Send(string from, string displayName, string to, string messageBody, string subject)
        {
            throw new NotImplementedException();
        }

        public void SendWithHtml(string from, string displayName, string to, string subject, AlternateView htmlView)
        {
            throw new NotImplementedException();
        }
    }
}

