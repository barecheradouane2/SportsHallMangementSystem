using Microsoft.Extensions.Configuration;
using MimeKit;
using Sportshall.Core.DTO;
using Sportshall.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sportshall.infrastructure.Repositries.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration _configuration)
        {
            this._configuration = _configuration;
        }
        public async Task SendEmail(EmailDTO emailDTO)
        {

            MimeMessage message = new MimeMessage();

            message.From.Add(new MailboxAddress("my sport ", _configuration["EmailSetting:From"]));

            message.Subject = emailDTO.Subject;

            message.To.Add(new MailboxAddress(emailDTO.To, emailDTO.To));

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text =emailDTO.content
            };

            using (var smtp = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {

                    await smtp.ConnectAsync(_configuration["EmailSetting:smtp"], int.Parse(_configuration["EmailSetting:Port"]), true);

                    await smtp.AuthenticateAsync(_configuration["EmailSetting:UserName"],
                        _configuration["EmailSetting:Password"]

                        );

                    await smtp.SendAsync(message);

                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();

                }
            }



                throw new NotImplementedException("Email sending is not implemented yet. Please implement the email sending logic using an SMTP client or any other email service provider.");

        }
    }
}
