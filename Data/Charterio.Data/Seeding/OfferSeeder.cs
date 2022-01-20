namespace Charterio.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using global::Data.Models;

    internal class OfferSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Offers.Any())
            {
                return;
            }

            await dbContext.Offers.AddAsync(
                new Offer
                {
                    Name = "Charter > London - Amsterdam",
                    FlightId = 1,
                    StartAirportId = 1,
                    EndAirportId = 4,
                    StartTimeUtc = new DateTime(2022, 5, 27, 11, 20, 00).ToUniversalTime(),
                    EndTimeUtc = new DateTime(2022, 5, 27, 13, 05, 00).ToUniversalTime(),
                    Price = 189,
                    CurrencyId = 1,
                    AllotmentCount = 25,
                    IsActiveInWeb = true,
                    IsActiveInAdmin = true,
                    CreatedOn = new DateTime(2022, 1, 20, 13, 05, 00).ToUniversalTime(),
                    Categing = "1 bottle of water",
                    Luggage = "20 kg checked in luggage, 5 kg cabin luggage",
                });

            await dbContext.Offers.AddAsync(
                new Offer
                {
                    Name = "Charter > London - Minsk",
                    FlightId = 1,
                    StartAirportId = 1,
                    EndAirportId = 7,
                    StartTimeUtc = new DateTime(2022, 5, 27, 9, 20, 00).ToUniversalTime(),
                    EndTimeUtc = new DateTime(2022, 5, 27, 13, 55, 00).ToUniversalTime(),
                    Price = 355,
                    CurrencyId = 1,
                    AllotmentCount = 25,
                    IsActiveInWeb = true,
                    IsActiveInAdmin = true,
                    CreatedOn = new DateTime(2022, 1, 20, 13, 05, 00).ToUniversalTime(),
                    Categing = "1 bottle of water, sandwich, dessert, coffee and tea",
                    Luggage = "32 kg checked in luggage, 5 kg hand luggage",
                });

            await dbContext.Offers.AddAsync(
                new Offer
                {
                    Name = "Charter > London - Minsk",
                    FlightId = 1,
                    StartAirportId = 1,
                    EndAirportId = 7,
                    StartTimeUtc = new DateTime(2022, 5, 29, 9, 20, 00).ToUniversalTime(),
                    EndTimeUtc = new DateTime(2022, 5, 29, 12, 55, 00).ToUniversalTime(),
                    Price = 157,
                    CurrencyId = 1,
                    AllotmentCount = 30,
                    IsActiveInWeb = true,
                    IsActiveInAdmin = true,
                    CreatedOn = new DateTime(2022, 1, 20, 13, 05, 00).ToUniversalTime(),
                    Categing = "Nothing included",
                    Luggage = "5 kg hand luggage",
                });

            await dbContext.Offers.AddAsync(
                new Offer
                {
                    Name = "Charter > London - Minsk lowcost",
                    FlightId = 1,
                    StartAirportId = 1,
                    EndAirportId = 7,
                    StartTimeUtc = new DateTime(2022, 5, 29, 9, 20, 00).ToUniversalTime(),
                    EndTimeUtc = new DateTime(2022, 5, 29, 17, 10, 00).ToUniversalTime(),
                    Price = 87,
                    CurrencyId = 1,
                    AllotmentCount = 10,
                    IsActiveInWeb = true,
                    IsActiveInAdmin = true,
                    CreatedOn = new DateTime(2022, 1, 20, 13, 05, 00).ToUniversalTime(),
                    Categing = "Nothing included",
                    Luggage = "3 kg hand luggage",
                });
        }
    }
}
