namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Data.Models;

    internal class FlightsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Flights.Any())
            {
                return;
            }

            await dbContext.Flights.AddAsync(new Flight { Number = "EAC1853", PlaneId = 3, CompanyId = 2 });
            await dbContext.Flights.AddAsync(new Flight { Number = "EAC1854", PlaneId = 3, CompanyId = 2 });
            await dbContext.Flights.AddAsync(new Flight { Number = "FB319", PlaneId = 2, CompanyId = 2 });
        }
    }
}
