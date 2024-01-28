using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;

namespace PRTest.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class MailController : BaseController
	{
		string Email = "shayoneditor@gmail.com";
		string Password = "*****";
		string Host = "smtp.gmail.com";
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

				using (MailMessage message = new MailMessage())
				{
					MailAddress fromAddress = new MailAddress(Email, "Email Validation");

					smtpClient.Host = Host;
					smtpClient.Port = 587;
					smtpClient.EnableSsl = true;
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
