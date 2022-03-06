namespace Charterio.Services.Data.SendGrid
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Charterio.Global;
    using global::SendGrid;
    using global::SendGrid.Helpers.Mail;
    using Microsoft.Extensions.Configuration;

    public class SendGrid : ISendGrid
    {
        private readonly SendGridClient client;
        private readonly IConfiguration configuration;

        public SendGrid(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.client = new SendGridClient(this.ApiKey);
        }

        private string ApiKey => this.configuration["SendGridApiKey"];

        public async Task SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            if (string.IsNullOrWhiteSpace(subject) && string.IsNullOrWhiteSpace(htmlContent))
            {
                throw new ArgumentException(GlobalConstants.SendGridNoSubjectAndMessage);
            }

            var fromAddress = new EmailAddress(from, fromName);
            var toAddress = new EmailAddress(to);
            var message = MailHelper.CreateSingleEmail(fromAddress, toAddress, subject, null, htmlContent);
            if (attachments?.Any() == true)
            {
                foreach (var attachment in attachments)
                {
                    message.AddAttachment(attachment.FileName, Convert.ToBase64String(attachment.Content), attachment.MimeType);
                }
            }

            try
            {
                var response = await this.client.SendEmailAsync(message);
                Console.WriteLine(response.StatusCode);
                Console.WriteLine(await response.Body.ReadAsStringAsync());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
