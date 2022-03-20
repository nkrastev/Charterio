namespace Charterio.Services.Hosted.Tests
{
    using Charterio.Data;
    using Charterio.Services.Hosted.HostedService;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using Moq;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class CancelHostedServiceTests
    {        

        [Fact]
        public async Task StartingServiceDoesNotThrow()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("StartingServiceDoesNotThrow").Options;
            var dbContext = new ApplicationDbContext(options);

            CancellationToken cancellationToken = new CancellationToken();

            var mock = new Mock<ILogger<CancelHostedService>>();
            ILogger<CancelHostedService> logger = mock.Object;
            logger = Mock.Of<ILogger<CancelHostedService>>();            

            var service = new CancelHostedService(logger, dbContext);

            var ex = await Record.ExceptionAsync(() => service.StartAsync(cancellationToken));

            Assert.Null(ex);
        }

        [Fact]
        public async Task StoppingServiceDoesNotThrow()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("StoppingServiceDoesNotThrow").Options;
            var dbContext = new ApplicationDbContext(options);

            CancellationToken stoppingToken = new CancellationToken();

            var mock = new Mock<ILogger<CancelHostedService>>();
            ILogger<CancelHostedService> logger = mock.Object;
            logger = Mock.Of<ILogger<CancelHostedService>>();

            var service = new CancelHostedService(logger, dbContext);

            var ex = await Record.ExceptionAsync(() => service.StopAsync(stoppingToken));

            Assert.Null(ex);
        }
    }
}