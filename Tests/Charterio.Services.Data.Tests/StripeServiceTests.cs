namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Flight;
    using Charterio.Services.Data.SendGrid;
    using Charterio.Services.Data.Ticket;
    using Charterio.Services.Payment.ViaBraintree;
    using Charterio.Services.Payment.ViaStripe;
    using Charterio.Web;
    using Charterio.Web.ViewModels.Booking;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Stripe;
    using Xunit;

    public class StripeServiceTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        [Fact]
        public void ProcessPaymentForwardUserToStripeGate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ProcessPaymentForwardUserToStripeGate").Options;
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

            StripeConfiguration.ApiKey = config["StripeApiKey"];

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);
            var ticketService = new TicketService(dbContext, flightService, allotmentService, emailSender);

            var service = new StripeService(dbContext, ticketService, emailSender);

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
            Assert.Contains("https://checkout.stripe.com/pay/cs_test_", service.ProcessPayment("tok_visa", "test@charterio.com", 1));
        }

        [Fact]
        public async Task MarkTicketAsPaidChangesDbAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MarkTicketAsPaidChangesDb").Options;
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

            StripeConfiguration.ApiKey = config["StripeApiKey"];

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);
            var ticketService = new TicketService(dbContext, flightService, allotmentService, emailSender);

            var service = new StripeService(dbContext, ticketService, emailSender);

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

            var ticket = dbContext.Tickets.Where(x => x.Id == 1).FirstOrDefault();
            var stringResult = await service.MarkTicketAsPaid(1, "transactionId", 150);

            Assert.Equal("OK", stringResult);
        }

        [Fact]
        public async Task MarkTicketAsPaidReturnFailsIfTicketIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MarkTicketAsPaidReturnFailsIfTicketIsInvalid").Options;
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

            StripeConfiguration.ApiKey = config["StripeApiKey"];

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);
            var ticketService = new TicketService(dbContext, flightService, allotmentService, emailSender);

            var service = new StripeService(dbContext, ticketService, emailSender);

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

            var ticket = dbContext.Tickets.Where(x => x.Id == 1).FirstOrDefault();
            var stringResult = await service.MarkTicketAsPaid(2, "transactionId", 150);

            Assert.Equal("Fail", stringResult);
        }

        [Fact]
        public async Task MarkTicketAsPaidReturnOKIfTicketIsMarkedBefore()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MarkTicketAsPaidReturnOKIfTicketIsMarkedBefore").Options;
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

            StripeConfiguration.ApiKey = config["StripeApiKey"];

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);
            var ticketService = new TicketService(dbContext, flightService, allotmentService, emailSender);

            var service = new StripeService(dbContext, ticketService, emailSender);

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

            var ticket = dbContext.Tickets.Where(x => x.Id == 1).FirstOrDefault();
            await service.MarkTicketAsPaid(1, "transactionId", 150);
            var stringSecondResult = await service.MarkTicketAsPaid(1, "SecondTransactionId", 150);

            Assert.Equal("OK", stringSecondResult);
        }
    }
}
