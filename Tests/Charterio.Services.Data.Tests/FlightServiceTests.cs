namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Flight;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class FlightServiceTests
    {
        // Bugs found > tests created
        [Fact]
        public void IfTargetOfferIsInactiveInWeb_GetFlightById_ReturnsNull()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("Database_For_Tests_Flight").Options;
            var dbContext = new ApplicationDbContext(options);
            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);

            var offer = new Offer
            {
                Name = "TestOffer",
                FlightId = 1,
                StartAirportId = 1,
                EndAirportId = 1,
                StartTimeUtc = DateTime.UtcNow,
                EndTimeUtc = DateTime.UtcNow.AddSeconds(1),
                Price = 1,
                CurrencyId = 1,
                AllotmentCount = 1,
                IsActiveInWeb = false,
            };
            dbContext.Offers.Add(offer);
            Assert.Null(flightService.GetById(1));
        }
    }
}
