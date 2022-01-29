namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Data.Models;

    internal class TicketPassengersSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.TicketPassangers.Any())
            {
                return;
            }

            await dbContext.TicketPassangers.AddAsync(new TicketPassenger { TicketId = 1, PaxTitle = "MR", PaxFirstName = "John", PaxLastName = "Stone", });
            await dbContext.TicketPassangers.AddAsync(new TicketPassenger { TicketId = 1, PaxTitle = "MR", PaxFirstName = "Ponnappa", PaxLastName = "Priya", });
            await dbContext.TicketPassangers.AddAsync(new TicketPassenger { TicketId = 1, PaxTitle = "MRS", PaxFirstName = "Mia", PaxLastName = "Wong", });
            await dbContext.TicketPassangers.AddAsync(new TicketPassenger { TicketId = 1, PaxTitle = "MR", PaxFirstName = "Peter", PaxLastName = "Stanbrige", });
            await dbContext.TicketPassangers.AddAsync(new TicketPassenger { TicketId = 1, PaxTitle = "MRS", PaxFirstName = "Natalie", PaxLastName = "Lee-Walsh", });
        }
    }
}
