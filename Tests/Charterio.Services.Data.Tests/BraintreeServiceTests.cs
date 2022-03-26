namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Flight;
    using Charterio.Services.Data.SendGrid;
    using Charterio.Services.Data.Ticket;
    using Charterio.Services.Payment.ViaBraintree;
    using Charterio.Web;
    using Charterio.Web.ViewModels.Booking;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class BraintreeServiceTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        [Fact]
        public void ProcessPaymentReturnsCorrectUrl()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ProcessPaymentReturnsCorrectUrl").Options;
            var dbContext = new ApplicationDbContext(options);

            IConfiguration config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Production.json", false, true)
                .AddEnvironmentVariables().Build();

            var fakeConfig = new Dictionary<string, string>
            {
                { "SendGridApiKey", "3" },
            };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(fakeConfig)
                .Build();

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);
            var ticketService = new TicketService(dbContext, flightService, allotmentService, emailSender);

            var service = new BraintreeService(config, ticketService, dbContext);

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

            dbContext.Tickets.Add(new Ticket
            {
                Id = 1,
                TicketCode = "XXX",
                TicketStatusId = 3,
                TicketIssuerId = 1,
                OfferId = 1,
                UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { Id = 1, TicketId = 1, PaxTitle = "MR", PaxFirstName = "Test", PaxLastName = "Testov", } },
            });
            dbContext.SaveChanges();

            var model = new BraintreeBookingViewModel
            {
                TicketId = 1,
                Nonce = "fake-valid-nonce",
            };

            Assert.Equal("/Booking/SuccessBraintree", service.ProcessPayment(model));
        }
    }
}
