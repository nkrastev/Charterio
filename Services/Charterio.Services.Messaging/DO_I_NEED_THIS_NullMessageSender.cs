namespace Charterio.Services.Messaging
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class DO_I_NEED_THIS_NullMessageSender : IEmailSender
    {
        public Task SendEmailAsync(
            string from,
            string fromName,
            string to,
            string subject,
            string htmlContent,
            IEnumerable<EmailAttachment> attachments = null)
        {
            return Task.CompletedTask;
        }
    }
}
