namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data.Models;

    internal class TicketStatusesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.TicketStatuses.Any())
            {
                return;
            }

            await dbContext.TicketStatuses.AddAsync(new TicketStatus { Status = "Paid, confirmed", });
            await dbContext.TicketStatuses.AddAsync(new TicketStatus { Status = "Not paid, cancelled", });
            await dbContext.TicketStatuses.AddAsync(new TicketStatus { Status = "Waiting for payment", });
        }
    }
}
