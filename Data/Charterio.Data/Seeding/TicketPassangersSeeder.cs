namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Data.Models;

    internal class TicketPassangersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.TicketPassangers.Any())
            {
                return;
            }

            await dbContext.TicketPassangers.AddAsync(new TicketPassanger { TicketId = 1, PaxTitleId = 1, PaxFirstName = "John", PaxLastName = "Stone", });
            await dbContext.TicketPassangers.AddAsync(new TicketPassanger { TicketId = 1, PaxTitleId = 1, PaxFirstName = "Ponnappa", PaxLastName = "Priya", });
            await dbContext.TicketPassangers.AddAsync(new TicketPassanger { TicketId = 1, PaxTitleId = 1, PaxFirstName = "Mia", PaxLastName = "Wong", });
            await dbContext.TicketPassangers.AddAsync(new TicketPassanger { TicketId = 1, PaxTitleId = 1, PaxFirstName = "Peter", PaxLastName = "Stanbrige", });
            await dbContext.TicketPassangers.AddAsync(new TicketPassanger { TicketId = 1, PaxTitleId = 1, PaxFirstName = "Natalie", PaxLastName = "Lee-Walsh", });
        }
    }
}
