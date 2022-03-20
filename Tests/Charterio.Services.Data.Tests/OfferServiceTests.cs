namespace Charterio.Services.Data.Tests
{
    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Offer;
    using Charterio.Web.ViewModels.Administration.Offer;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Xunit;

    public class OfferServiceTests
    {
        [Fact]

        public void AddOfferIncreaseRecordsInDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AddOfferIncreaseRecordsInDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new OfferService(dbContext);

            var flight = new Flight { Id = 1, Number = "NotNull", CompanyId = 1, PlaneId = 1 };
            var start = new Airport { Id = 1, IataCode = "SOF", Name = "Sofia", Latitude = 1, Longtitude = 1, UtcPosition = 0 };
            var end = new Airport { Id = 2, IataCode = "BOJ", Name = "Burgas", Latitude = 1, Longtitude = 1, UtcPosition = 0 };

            dbContext.Flights.Add(flight);
            dbContext.Airports.Add(start);
            dbContext.Airports.Add(end);
            dbContext.SaveChanges();

            var model = new OfferAdminAddViewModel
            {
                Name = "test offer",
                FlightNumber = "1",
                StartAirportCode = "1",
                StartUTC = DateTime.UtcNow.AddDays(1),
                EndAirportCode = "2",
                EndUTC = DateTime.UtcNow.AddDays(2),
                AllotmentCount = 73,
                Price = 174.4,
                Luggage = "None",
                Catering = "None",
                IsActiveInWeb = true,
            };

            // Act
            service.Add(model);
            service.Add(model);

            // Arrange
            Assert.Equal(2, dbContext.Offers.ToList().Count);
        }

        [Fact]
        public void EditModelChangeDataInDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("EditModelChangeDataInDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new OfferService(dbContext);

            var flight = new Flight { Id = 1, Number = "NotNull", CompanyId = 1, PlaneId = 1 };
            var start = new Airport { Id = 1, IataCode = "SOF", Name = "Sofia", Latitude = 1, Longtitude = 1, UtcPosition = 0 };
            var end = new Airport { Id = 2, IataCode = "BOJ", Name = "Burgas", Latitude = 1, Longtitude = 1, UtcPosition = 0 };

            dbContext.Flights.Add(flight);
            dbContext.Airports.Add(start);
            dbContext.Airports.Add(end);
            dbContext.SaveChanges();

            dbContext.Offers.Add(new Offer
            {
                Name = "TestOffer",
                Flight = flight,
                StartAirport = start,
                EndAirport = end,
                StartTimeUtc = DateTime.UtcNow.AddDays(1),
                EndTimeUtc = DateTime.UtcNow.AddDays(2),
                Price = 150,
                CurrencyId = 1,
                AllotmentCount = 10,
                IsActiveInWeb = true,
            });
            dbContext.SaveChanges();

            var model = new OfferAdminViewModel
            {
                Id = 1,
                Name = "ChangedTestOffer",
                FlightNumber = "1",
                StartAirportCode = "1",
                StartUTC = DateTime.UtcNow.AddDays(1),
                EndAirportCode = "2",
                EndUTC = DateTime.UtcNow.AddDays(2),
                AllotmentCount = 77,
                Price = 141,
                Luggage = "None",
                Catering = "None",
                IsActiveInWeb = true,
                IsActiveInAdmin = true,
            };

            // Act
            service.Edit(model);
            var changedOffer = dbContext.Offers.Where(x => x.Id == 1).FirstOrDefault();

            // Assert
            Assert.Equal("ChangedTestOffer", changedOffer.Name);
            Assert.Equal(77, changedOffer.AllotmentCount);
        }

        [Fact]
        public void GetAllReturnsAllOffersActiveInAdmin()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllReturnsAllOffersActiveInAdmin").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new OfferService(dbContext);

            var flight = new Flight { Id = 1, Number = "NotNull", CompanyId = 1, PlaneId = 1 };
            var start = new Airport { Id = 1, IataCode = "SOF", Name = "Sofia", Latitude = 1, Longtitude = 1, UtcPosition = 0 };
            var end = new Airport { Id = 2, IataCode = "BOJ", Name = "Burgas", Latitude = 1, Longtitude = 1, UtcPosition = 0 };

            dbContext.Flights.Add(flight);
            dbContext.Airports.Add(start);
            dbContext.Airports.Add(end);
            dbContext.SaveChanges();

            dbContext.Offers.Add(new Offer
            {
                Name = "TestOffer1",
                Flight = flight,
                StartAirport = start,
                EndAirport = end,
                StartTimeUtc = DateTime.UtcNow.AddDays(1),
                EndTimeUtc = DateTime.UtcNow.AddDays(2),
                Price = 150,
                CurrencyId = 1,
                AllotmentCount = 10,
                IsActiveInWeb = true,
                IsActiveInAdmin = true,
            });

            dbContext.Offers.Add(new Offer
            {
                Name = "TestOffer2",
                Flight = flight,
                StartAirport = start,
                EndAirport = end,
                StartTimeUtc = DateTime.UtcNow.AddDays(1),
                EndTimeUtc = DateTime.UtcNow.AddDays(2),
                Price = 150,
                CurrencyId = 1,
                AllotmentCount = 10,
                IsActiveInWeb = true,
                IsActiveInAdmin = true,
            });
            dbContext.SaveChanges();

            // Act
            var offers = service.GetAll();

            // Assert
            Assert.Equal(2, offers.Count);
        }

        [Fact]
        public void GetByIdReturnsCorrectModelIfActiveInAdmin()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdReturnsCorrectModelIfActiveInAdmin").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new OfferService(dbContext);

            var flight = new Flight { Id = 1, Number = "NotNull", CompanyId = 1, PlaneId = 1 };
            var start = new Airport { Id = 1, IataCode = "SOF", Name = "Sofia", Latitude = 1, Longtitude = 1, UtcPosition = 0 };
            var end = new Airport { Id = 2, IataCode = "BOJ", Name = "Burgas", Latitude = 1, Longtitude = 1, UtcPosition = 0 };

            dbContext.Flights.Add(flight);
            dbContext.Airports.Add(start);
            dbContext.Airports.Add(end);
            dbContext.SaveChanges();

            dbContext.Offers.Add(new Offer
            {
                Id = 1,
                Name = "TestOffer1",
                Flight = flight,
                StartAirport = start,
                EndAirport = end,
                StartTimeUtc = DateTime.UtcNow.AddDays(1),
                EndTimeUtc = DateTime.UtcNow.AddDays(2),
                Price = 150,
                CurrencyId = 1,
                AllotmentCount = 10,
                IsActiveInWeb = true,
                IsActiveInAdmin = true,
            });
            dbContext.SaveChanges();

            // Act
            var target = service.GetById(1);

            // Assert
            Assert.Equal("TestOffer1", target.Name);
        }

        [Fact]
        public void GetDropdownsReturnsListOfFlightWithIdAndAirportWithId()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetDropdownsReturnsListOfFlightWithIdAndAirportWithId").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new OfferService(dbContext);

            dbContext.Flights.Add(new Flight { Id = 1, Number = "NotNull", CompanyId = 1, PlaneId = 1 });
            dbContext.Flights.Add(new Flight { Id = 2, Number = "NotNull", CompanyId = 1, PlaneId = 1 });
            dbContext.Flights.Add(new Flight { Id = 3, Number = "NotNull", CompanyId = 1, PlaneId = 1 });

            dbContext.Airports.Add(new Airport { Id = 1, IataCode = "SOF", Name = "Sofia", Latitude = 1, Longtitude = 1, UtcPosition = 0 });
            dbContext.Airports.Add(new Airport { Id = 2, IataCode = "BOJ", Name = "Burgas", Latitude = 1, Longtitude = 1, UtcPosition = 0 });

            dbContext.SaveChanges();

            // Act
            var dropdowns = service.GetDropdowns();

            // Assert
            Assert.NotNull(dropdowns);
            Assert.Equal(3, dropdowns.FlightsDropDown.Count);
            Assert.Equal(2, dropdowns.AirportsDropDown.Count);
        }
    }
}
