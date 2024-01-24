﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace PRTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MailController : BaseController
	{
		string Email = "sojibhossain@deshisysltd.com";
		string Password = "dsl@123!";
		string Host = "host22.registrar-servers.com";
		public MailController()
		{

		}

		[HttpGet("[action]")]
		public async Task<dynamic> SendEmail(string ReceiverMail, string MailbodyPath, int OTP)
		{
			using (SmtpClient smtpClient = new SmtpClient())
			{
				var basicCredential = new NetworkCredential(Email, Password);
				string FilePath = MailbodyPath;
				StreamReader str = new StreamReader(FilePath);
				string MailText = str.ReadToEnd();
				MailText = MailText.Replace("#OTP", OTP.ToString());
				MailText = MailText.Replace("#ReceiverEmail", ReceiverMail);

				using (MailMessage message = new MailMessage())
				{
					MailAddress fromAddress = new MailAddress(Email, "Email Validation");

					smtpClient.Host = Host;
					smtpClient.UseDefaultCredentials = false;
					smtpClient.Credentials = basicCredential;

					message.From = fromAddress;
					message.Subject = "Email Validation";
					message.IsBodyHtml = true;
					message.Body = MailText;
					message.To.Add(ReceiverMail);

					try
					{
						await smtpClient.SendMailAsync(message);
						//smtpClient.Send(message);
					}
					catch (Exception ex)
					{
						return ex;
					}
				}
				return ReceiverMail;
			}
		}
	}
}
