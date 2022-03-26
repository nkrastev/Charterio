namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Flight;
    using Charterio.Services.Data.SendGrid;
    using Charterio.Services.Data.Ticket;
    using Charterio.Web.ViewModels.Ticket;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Xunit;

    public class TicketServiceTests
    {
        [Fact]
        public void CreateTicketIncreaseCountInDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CreateTicketIncreaseCountInDb").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);

            var service = new TicketService(dbContext, flightService, allotmentService, emailSender);

            var offer = new Offer
            {
                Name = "TestOffer",
                FlightId = 1,
                StartAirportId = 1,
                EndAirportId = 1,
                StartTimeUtc = DateTime.UtcNow.AddDays(1),
                EndTimeUtc = DateTime.UtcNow.AddDays(2),
                Price = 1,
                CurrencyId = 1,
                AllotmentCount = 10,
                IsActiveInWeb = true,
            };
            dbContext.Offers.Add(offer);
            dbContext.SaveChanges();

            var modelForTicket = new TicketCreateViewModel
            {
                OfferId = 1,
                UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                PaxList = new List<TicketPaxCreateViewModel> { new TicketPaxCreateViewModel { PaxTitle = "MR", PaxFirstName = "Name1", PaxLastName = "Name2" } },
            };

            service.CreateTicket(modelForTicket);
            service.CreateTicket(modelForTicket);

            Assert.Equal(2, dbContext.Tickets.ToList().Count);
        }

        [Fact]
        public void IsTicketIdValidReturnsValidOrNot()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("IsTicketIdValidReturnsValidOrNot").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);

            var service = new TicketService(dbContext, flightService, allotmentService, emailSender);

            dbContext.Tickets.Add(new Ticket
            {
                Id = 1,
                TicketCode = "XXX",
                TicketStatusId = 1,
                TicketIssuerId = 1,
                OfferId = 1,
                UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
            });
            dbContext.SaveChanges();

            // Act & Assert
            Assert.True(service.IsTicketIdValid(1));
            Assert.False(service.IsTicketIdValid(99));
        }

        [Fact]
        public void MarkTicketAsCanceledChangeStatusTo2()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MarkTicketAsCanceledChangeStatusTo2").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);

            var service = new TicketService(dbContext, flightService, allotmentService, emailSender);

            dbContext.Tickets.Add(new Ticket
            {
                Id = 1,
                TicketCode = "XXX",
                TicketStatusId = 1,
                TicketIssuerId = 1,
                OfferId = 1,
                UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
            });
            dbContext.SaveChanges();

            // Act
            service.MarkTicketAsCancelled(1);
            var ticket = dbContext.Tickets.Where(x => x.Id == 1).FirstOrDefault();

            // Assert
            Assert.Equal(2, ticket.TicketStatusId);
        }

        [Fact]
        public void GetActivePaymentMethodsReturnsTheirNames()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetActivePaymentMethodsReturnsTheirNames").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);

            var service = new TicketService(dbContext, flightService, allotmentService, emailSender);

            dbContext.PaymentMethods.Add(new PaymentMethod { Name = "Borica", IsActive = false });
            dbContext.PaymentMethods.Add(new PaymentMethod { Name = "Paypal", IsActive = true });
            dbContext.PaymentMethods.Add(new PaymentMethod { Name = "Stripe", IsActive = true });
            dbContext.SaveChanges();

            // Act
            var paymentMethods = service.GetActivePaymentMethods();

            // Assert
            Assert.Equal(2, paymentMethods.Count());
            Assert.Contains(paymentMethods, x => x == "Paypal");
        }

        [Fact]
        public void CalculateTicketPriceReturnsCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CalculateTicketPriceReturnsCorrectData").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);

            var service = new TicketService(dbContext, flightService, allotmentService, emailSender);

            dbContext.Offers.Add(new Offer
            {
                Name = "TestOffer",
                FlightId = 1,
                StartAirportId = 1,
                EndAirportId = 1,
                StartTimeUtc = DateTime.UtcNow.AddDays(1),
                EndTimeUtc = DateTime.UtcNow.AddDays(2),
                Price = 150,
                CurrencyId = 1,
                AllotmentCount = 10,
                IsActiveInWeb = true,
            });

            dbContext.Tickets.Add(new Ticket
            {
                Id = 1,
                TicketCode = "XXX",
                TicketStatusId = 1,
                TicketIssuerId = 1,
                OfferId = 1,
                UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.SaveChanges();

            // Act
            var price = service.CalculateTicketPrice(1);

            // Assert
            Assert.Equal(150, price);
        }

        [Fact]
        public async Task SendConfirmationEmailAsyncDoesNotThrow()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("SendConfirmationEmailAsyncDoesNotThrow").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);

            var service = new TicketService(dbContext, flightService, allotmentService, emailSender);

            var flight = new Flight { Id = 1, Number = "NotNull", CompanyId = 1, PlaneId = 1 };

            var start = new Airport { Id = 1, IataCode = "SOF", Name = "Sofia", Latitude = 1, Longtitude = 1, UtcPosition = 0 };
            var end = new Airport { Id = 2, IataCode = "BOJ", Name = "Burgas", Latitude = 1, Longtitude = 1, UtcPosition = 0 };

            dbContext.Flights.Add(flight);
            dbContext.Airports.Add(start);
            dbContext.Airports.Add(end);
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
            var user = new ApplicationUser
            {
                UserName = "user@charterio.com",
                Email = "user@charterio.com",
                PhoneNumber = "77777",
            };
            dbContext.Users.Add(user);

            dbContext.Tickets.Add(new Ticket
            {
                Id = 1,
                TicketCode = "XXX",
                TicketStatusId = 1,
                TicketIssuerId = 1,
                OfferId = 1,
                User = user,
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.SaveChanges();

            // Act & Assert
            var ex = await Record.ExceptionAsync(() => service.SendConfirmationEmailAsync(1));

            Assert.Null(ex);
        }

        [Fact]
        public void GetTicketByIdReturnsCorrectTicket()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetTicketByIdReturnsCorrectTicket").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);

            var service = new TicketService(dbContext, flightService, allotmentService, emailSender);

            var flight = new Flight { Id = 1, Number = "NotNull", CompanyId = 1, PlaneId = 1 };

            var start = new Airport { Id = 1, IataCode = "SOF", Name = "Sofia", Latitude = 1, Longtitude = 1, UtcPosition = 0 };
            var end = new Airport { Id = 2, IataCode = "BOJ", Name = "Burgas", Latitude = 1, Longtitude = 1, UtcPosition = 0 };
            var offer = new Offer
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
            };
            var user = new ApplicationUser
            {
                UserName = "user@charterio.com",
                Email = "user@charterio.com",
                PhoneNumber = "77777",
            };
            var status = new TicketStatus { Id = 1, Status = "NotNull" };

            dbContext.Flights.Add(flight);
            dbContext.Airports.Add(start);
            dbContext.Airports.Add(end);
            dbContext.Offers.Add(offer);
            dbContext.Users.Add(user);
            dbContext.TicketStatuses.Add(status);

            dbContext.Tickets.Add(new Ticket
            {
                Id = 1,
                TicketCode = "XXX",
                TicketStatus = status,
                TicketIssuerId = 1,
                Offer = offer,
                User = user,
                PaymentId = 1,
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.SaveChanges();

            // Act
            var ticket = service.GetTicketById(1);

            // Assert
            Assert.NotNull(ticket);
            Assert.Equal("XXX", ticket.TicketCode);
        }

        [Fact]
        public void UserHasAccessOnlyToTicketsThatOwns()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("UserHasAccessOnlyToTicketsThatOwns").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var allotmentService = new AllotmentService(dbContext);
            var flightService = new FlightService(dbContext, allotmentService);
            var emailSender = new SendGrid(configuration);

            var service = new TicketService(dbContext, flightService, allotmentService, emailSender);

            dbContext.Tickets.Add(new Ticket
            {
                Id = 1,
                TicketCode = "XXX",
                TicketStatusId = 1,
                TicketIssuerId = 1,
                OfferId = 1,
                UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
            });

            // Ticket with Id 2 is owned by another user
            dbContext.Tickets.Add(new Ticket
            {
                Id = 2,
                TicketCode = "XXX",
                TicketStatusId = 1,
                TicketIssuerId = 1,
                OfferId = 1,
                UserId = "0864f1fa-6c17-4157-80bf-dc5f5f079699",
            });
            dbContext.SaveChanges();

            // Act & Assert
            Assert.True(service.IfUserHasAccessToTicket("0864f1fa-6c17-4157-80bf-dc5f5f0796d8", 1));
            Assert.False(service.IfUserHasAccessToTicket("0864f1fa-6c17-4157-80bf-dc5f5f0796d8", 2));
        }
    }
}
