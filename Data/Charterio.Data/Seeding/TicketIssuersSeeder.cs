namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data.Models;

    internal class TicketIssuersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.TicketIssuers.Any())
            {
                return;
            }

            await dbContext.TicketIssuers.AddAsync(new TicketIssuer { Name = "Default via website", });
            await dbContext.TicketIssuers.AddAsync(new TicketIssuer { Name = "Administrator", });
        }
    }
}
