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
    using Charterio.Web.ViewModels.Search;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class FlightServiceTests
    {
        // Bug found > test written
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

        // Bug found > test written
        [Fact]
        public void IfFlightIsInThePast_GetFlightById_ReturnsNull()
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
                StartTimeUtc = DateTime.UtcNow.AddDays(-2),
                EndTimeUtc = DateTime.UtcNow.AddDays(-1),
                Price = 1,
                CurrencyId = 1,
                AllotmentCount = 1,
                IsActiveInWeb = false,
            };
            dbContext.Offers.Add(offer);
            Assert.Null(flightService.GetById(1));
        }

        // Bug found > test written
        [Fact]
        public void IfFlightIsInThePast_GetFlightsBySearchTerms_ReturnsEmptyList()
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
                StartTimeUtc = DateTime.UtcNow.AddDays(-2),
                EndTimeUtc = DateTime.UtcNow.AddDays(-1),
                Price = 1,
                CurrencyId = 1,
                AllotmentCount = 1,
                IsActiveInWeb = false,
            };

            dbContext.Offers.Add(offer);
            var terms = new SearchViewModel
            {
                StartFlightDate = DateTime.UtcNow.AddDays(-1),
                EndFlightDate = DateTime.UtcNow,
                PaxCount = 1,
            };
            Assert.Equal(0, flightService.GetFlightsBySearchTerms(terms).Count);
        }
    }
}
