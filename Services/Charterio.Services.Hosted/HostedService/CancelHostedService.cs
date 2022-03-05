namespace Charterio.Services.Hosted.HostedService
{
    using Charterio.Data;
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
            this.logger.LogInformation("Calcel 'Not Paid for 30 min' ticket Service running.");

            this.timer = new Timer(CancelNotPaidTickets, null, TimeSpan.Zero, TimeSpan.FromMinutes(30));

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

            this.logger.LogInformation("Hosted Service for canceling tickets is working. Count: {Count}", count);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            this.logger.LogInformation("Hosted Service for canceling tickets is stopping.");

            this.timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            this.timer?.Dispose();
        }
    }
}