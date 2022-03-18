namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Question;
    using Charterio.Services.Data.SendGrid;
    using Charterio.Web.ViewModels.Schedule;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class ScheduleServiceTests
    {
        [Fact]
        public void GetFlightsForScheduleReturnsCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetFlightsForScheduleReturnsCorrectData").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new ScheduleService(dbContext);

            var offer1 = new Offer
            {
                Name = "TestOffer1",
                Flight = new Flight { CompanyId = 1, Number = "T", PlaneId = 1 },
                StartAirport = new Airport { IataCode = "DDD", Name = "Name", UtcPosition = 0, Latitude = 1, Longtitude = 2 },
                EndAirport = new Airport { IataCode = "DDD", Name = "Name", UtcPosition = 0, Latitude = 1, Longtitude = 2 },
                StartTimeUtc = DateTime.UtcNow.AddDays(2),
                EndTimeUtc = DateTime.UtcNow.AddDays(3),
                Price = 1,
                CurrencyId = 1,
                AllotmentCount = 1,
                IsActiveInWeb = true,
            };
            var offer2 = new Offer
            {
                Name = "TestOffer1",
                Flight = new Flight { CompanyId = 1, Number = "T", PlaneId = 1 },
                StartAirport = new Airport { IataCode = "DDD", Name = "Name", UtcPosition = 0, Latitude = 1, Longtitude = 2 },
                EndAirport = new Airport { IataCode = "DDD", Name = "Name", UtcPosition = 0, Latitude = 1, Longtitude = 2 },
                StartTimeUtc = DateTime.UtcNow.AddDays(2),
                EndTimeUtc = DateTime.UtcNow.AddDays(3),
                Price = 1,
                CurrencyId = 1,
                AllotmentCount = 1,
                IsActiveInWeb = true,
            };

            dbContext.Offers.Add(offer1);
            dbContext.Offers.Add(offer2);
            dbContext.SaveChanges();

            Assert.Equal(2, service.GetFlightsForSchedule().Count);
        }

        [Fact]
        public void ServiceDoesNotReturnsFlighsFromThePast()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ServiceDoesNotReturnsFlighsFromThePast").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new ScheduleService(dbContext);

            var offer = new Offer
            {
                Name = "TestOffer1",
                Flight = new Flight { CompanyId = 1, Number = "T", PlaneId = 1 },
                StartAirport = new Airport { IataCode = "DDD", Name = "Name", UtcPosition = 0, Latitude = 1, Longtitude = 2 },
                EndAirport = new Airport { IataCode = "DDD", Name = "Name", UtcPosition = 0, Latitude = 1, Longtitude = 2 },
                StartTimeUtc = DateTime.UtcNow.AddDays(-2),
                EndTimeUtc = DateTime.UtcNow.AddDays(-3),
                Price = 1,
                CurrencyId = 1,
                AllotmentCount = 1,
                IsActiveInWeb = true,
            };

            dbContext.Offers.Add(offer);
            dbContext.SaveChanges();

            Assert.Equal(0, service.GetFlightsForSchedule().Count);
        }

        [Fact]
        public void ServiceDoesNotReturnsInactiveFlights()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ServiceDoesNotReturnsInactiveFlights").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new ScheduleService(dbContext);

            var offer = new Offer
            {
                Name = "TestOffer1",
                Flight = new Flight { CompanyId = 1, Number = "T", PlaneId = 1 },
                StartAirport = new Airport { IataCode = "DDD", Name = "Name", UtcPosition = 0, Latitude = 1, Longtitude = 2 },
                EndAirport = new Airport { IataCode = "DDD", Name = "Name", UtcPosition = 0, Latitude = 1, Longtitude = 2 },
                StartTimeUtc = DateTime.UtcNow.AddDays(2),
                EndTimeUtc = DateTime.UtcNow.AddDays(3),
                Price = 1,
                CurrencyId = 1,
                AllotmentCount = 1,
                IsActiveInWeb = false,
            };

            dbContext.Offers.Add(offer);
            dbContext.SaveChanges();

            Assert.Equal(0, service.GetFlightsForSchedule().Count);
        }
    }
}
