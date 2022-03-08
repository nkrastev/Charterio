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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("Database_For_Tests_FlightById").Options;
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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("Database_For_Tests_FlightTestOffer").Options;
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
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("Database_For_Tests_FlightPastFlight").Options;
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

        [Fact]
        public void GetAllAirportsReturnsListOfAirports()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("Database_For_Tests_Airports").Options;
            var dbContext = new ApplicationDbContext(options);
            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);

            var airport = new Airport
            {
                IataCode = "SOF",
                Name = "Sofia Airport",
                UtcPosition = 0,
                Latitude = 1,
                Longtitude = 2,
            };

            dbContext.Airports.Add(airport);
            dbContext.SaveChanges();

            var list = flightService.GetAllAirports();
            Assert.Equal(1, list.Count);
            Assert.Equal("SOF", list.FirstOrDefault().IataCode);
        }

        [Fact]
        public void GetAllAirportsReturnsZeroIfNoAirportInDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("Database_For_Tests_NoAirports").Options;
            var dbContext = new ApplicationDbContext(options);
            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);

            var list = flightService.GetAllAirports();
            Assert.Equal(0, list.Count);
        }

        [Fact]

        public void GetFlightsBySearchTermsReturnsCorrectFlightOffers()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("Database_For_Tests_CorrectOffer").Options;
            var dbContext = new ApplicationDbContext(options);
            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);

            var startAirport = new Airport
            {
                IataCode = "LON",
                Name = "London Airport",
                UtcPosition = 0,
                Latitude = 1,
                Longtitude = 2,
            };
            var endAirport = new Airport
            {
                IataCode = "AMS",
                Name = "Amsterdam Airport",
                UtcPosition = 0,
                Latitude = 1,
                Longtitude = 2,
            };

            dbContext.Airports.Add(startAirport);
            dbContext.Airports.Add(endAirport);

            dbContext.SaveChanges();

            var offerFirst = new Offer
            {
                Name = "Charter > London - Amsterdam",
                FlightId = 1,
                StartAirportId = 1,
                EndAirportId = 2,
                StartTimeUtc = new DateTime(2023, 5, 26, 11, 20, 00).ToUniversalTime(),
                EndTimeUtc = new DateTime(2023, 5, 26, 13, 05, 00).ToUniversalTime(),
                Price = 189,
                CurrencyId = 1,
                AllotmentCount = 25,
                IsActiveInWeb = true,
                IsActiveInAdmin = true,
                CreatedOn = new DateTime(2022, 1, 20, 13, 05, 00).ToUniversalTime(),
                Categing = "1 bottle of water",
                Luggage = "20 kg checked in luggage, 5 kg cabin luggage",
            };
            dbContext.Offers.Add(offerFirst);
            dbContext.SaveChanges();

            var offers = dbContext.Offers.ToArray();

            var termsCorrect = new SearchViewModel
            {
                StartApt = "LON",
                EndApt = "AMS",
                StartFlightDate = new DateTime(2023, 5, 20, 12, 20, 00).ToUniversalTime(),
                EndFlightDate = new DateTime(2023, 5, 30, 12, 05, 00).ToUniversalTime(),
                PaxCount = 1,
            };

            var list = flightService.GetFlightsBySearchTerms(termsCorrect);

            Assert.Equal(1, list.Count);
        }
    }
}
