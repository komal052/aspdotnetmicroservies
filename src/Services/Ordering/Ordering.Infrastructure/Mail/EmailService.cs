using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Model;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Mail
{
    public class EmailService : IEmailService
    {
        public EmailSetting _emailSetting {get;}
        public ILogger<EmailService> _logger { get;}

        public EmailService(IOptions<EmailSetting> emailSetting, ILogger<EmailService> logger)
        {
            _emailSetting = emailSetting.Value;
            _logger = logger;
        }

        public async Task<bool> SendEmail(Email email)
        {
            var client = new SendGridClient(_emailSetting.ApiKey);
            var Subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody= email.Body;

            var from = new EmailAddress
            {
                Email = _emailSetting.FromAddress,
                Name = _emailSetting.FromName

            };

            var sendmessage=MailHelper.CreateSingleEmail(from,to,Subject,emailBody, emailBody);
            var response=await client.SendEmailAsync(sendmessage);

            _logger.LogInformation("email sent");
            if(response.StatusCode==System.Net.HttpStatusCode.Accepted || response.StatusCode==System.Net.HttpStatusCode.OK) 
                return true;
              _logger.LogError("Email sending failed");
                return false;
        }
    }
}
