namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data.Models;

    internal class TicketsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Tickets.Any())
            {
                return;
            }

            var firstCustomer = dbContext.Users.Where(x => x.UserName == "testuser@charterio.com").FirstOrDefault();

            await dbContext.Tickets.AddAsync(new Ticket
            {
                TicketCode = "5754-8DK8-00001",
                TicketStatusId = 1,
                TicketIssuerId = 1,
                OfferId = 6,
                PaymentId = 1,
                UserId = firstCustomer.Id,
            });
        }
    }
}
