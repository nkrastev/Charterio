namespace Charterio.Services.Hosted.HostedService
{
    using Charterio.Data;
    using Charterio.Global;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    // Each 30 minutes this service is canceling not paid tickets.

    public class CancelHostedService : IHostedService, IDisposable, ICancelHostedService
    {
        private int executionCount = 0;
        private readonly ILogger<CancelHostedService> logger;
        private Timer timer = null!;
        private readonly ApplicationDbContext db;

        public CancelHostedService(ILogger<CancelHostedService> logger, ApplicationDbContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation(GlobalConstants.LogInformationHostedService);

            this.timer = new Timer(CancelNotPaidTickets, null, TimeSpan.Zero, TimeSpan.FromMinutes(GlobalConstants.HostedServiceLoopMinutes));

            return Task.CompletedTask;
        }

        private void CancelNotPaidTickets(object? state)
        {
            var count = Interlocked.Increment(ref executionCount);

            var tickets = this.db.Tickets.Where(x => x.TicketStatus.Id == 3).ToList();

            foreach (var item in tickets)
            {
                item.TicketStatusId = 2;
            }          
            this.db.SaveChanges();

            this.logger.LogInformation(GlobalConstants.LogInformationHostedServiceRunning);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation(GlobalConstants.LogInformationHostedServiceStopping);

            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }
    }
}