using System;
using System.Net.Mail;

namespace E_Commerce.Business.Interfaces
{
	public interface ISendEmail
	{
        public void Send(string from, string displayName, string to, string messageBody, string subject);
        public void SendWithHtml(string from, string displayName, string to, string subject, AlternateView htmlView);
    }
}

