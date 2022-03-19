namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Charterio.Services.Data.SendGrid;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class SendGridServiceTests
    {
        [Fact]
        public async Task SendEmailAsyncThrowsIfSubjectAndContentAreNull()
        {
            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var service = new SendGrid(configuration);

            await Assert.ThrowsAsync<Exception>(() => service.SendEmailAsync("from@test.com", "FromName", "to@test.com", null, null));
        }

        [Fact]
        public async Task SendEmailAsyncDoesNotThrowIfDataValid()
        {
            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var service = new SendGrid(configuration);

            var ex = await Record.ExceptionAsync(() => service.SendEmailAsync("from@test.com", "FromName", "to@test.com", "subject", "content"));

            Assert.Null(ex);
        }

        [Fact]
        public async Task SendEmailAsyncWithAttachmentsDoesNotThrowIfDataValid()
        {
            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            List<EmailAttachment> listOfAttachments = new List<EmailAttachment>();
            listOfAttachments.Add(new EmailAttachment { Content = new byte[3] });

            var service = new SendGrid(configuration);

            var ex = await Record.ExceptionAsync(() => service.SendEmailAsync("from@test.com", "FromName", "to@test.com", "subject", "content", listOfAttachments));

            Assert.Null(ex);
        }
    }
}
