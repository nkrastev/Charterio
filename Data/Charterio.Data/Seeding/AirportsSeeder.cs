namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Charterio.Data.Models;
    using global::Data.Models;

    internal class AirportsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Airports.Any())
            {
                return;
            }

            await dbContext.Airports.AddAsync(
                new Airport { IataCode = "LHR", Name = "London Heathrow", UtcPosition = 0, Latitude = 51.4775, Longtitude = -0.461389 });
            await dbContext.Airports.AddAsync(
                new Airport { IataCode = "CDG", Name = "Paris Charles de Gaulle", UtcPosition = 1, Latitude = 49.0097, Longtitude = 2.54778 });
            await dbContext.Airports.AddAsync(
                new Airport { IataCode = "FRA", Name = "Frankfurt International", UtcPosition = 1, Latitude = 50.0331, Longtitude = 8.57056 });
            await dbContext.Airports.AddAsync(
                new Airport { IataCode = "AMS", Name = "Amsterdam", UtcPosition = 1, Latitude = 52.3081, Longtitude = 4.76417 });
            await dbContext.Airports.AddAsync(
                new Airport { IataCode = "FCO", Name = "Rome Fiumicino", UtcPosition = 1, Latitude = 41.8003, Longtitude = 12.2389 });
            await dbContext.Airports.AddAsync(
                new Airport { IataCode = "HEL", Name = "Helsinki", UtcPosition = 2, Latitude = 60.3172, Longtitude = 24.9633 });
            await dbContext.Airports.AddAsync(
                new Airport { IataCode = "MSQ", Name = "Minsk International", UtcPosition = 3, Latitude = 53.8825, Longtitude = 28.0308 });
            await dbContext.Airports.AddAsync(
                new Airport { IataCode = "SVO", Name = "Moscow Sheremetyevo International", UtcPosition = 3, Latitude = 55.9728, Longtitude = 37.4147 });
        }
    }
}
