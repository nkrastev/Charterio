namespace Charterio.Services.Data.Tests
{
    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Administration;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class AdminServiceTests
    {
        [Fact]
        public void GetLast10TicketsReturnsData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetLast10TicketsReturnsData").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new AdminService(dbContext);

            var status = new TicketStatus { Id = 1, Status = "new", };
            dbContext.TicketStatuses.Add(status);
            dbContext.SaveChanges();

            dbContext.Tickets.Add(new Ticket
            {
                Id = 1, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 2, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 3, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 4, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 5, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 6, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 7, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 8, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 9, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 10, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });
            dbContext.Tickets.Add(new Ticket
            {
                Id = 11, TicketCode = "XXX", TicketStatus = status, TicketIssuerId = 1, OfferId = 1, UserId = "0864f1fa-6c17-4157-80bf-dc5f5f0796d8",
                TicketPassengers = new List<TicketPassenger> { new TicketPassenger { PaxFirstName = "Ivan", PaxLastName = "Ivanov", TicketId = 1, PaxTitle = "MR", } },
            });

            dbContext.SaveChanges();

            // Act
            var list = service.GetLast10Tickets();

            // Assert
            Assert.Equal(10, list.Count);
        }

        [Fact]
        public void GetLast10UserQuestionsReturnsCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetLast10UserQuestionsReturnsCorrectData").Options;
            var dbContext = new ApplicationDbContext(options);

            var service = new AdminService(dbContext);

            dbContext.UserQuestions.Add(new UserQuestion { Id = 1, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 2, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 3, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 4, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 5, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 6, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 7, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 8, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 9, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 10, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 11, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });
            dbContext.UserQuestions.Add(new UserQuestion { Id = 12, Question = "Q1", UserName = "Name", UserPhone = "17", UserEmail = "Valid@email.com" });


            dbContext.SaveChanges();

            // Act
            var list = service.GetLast10UserQuestions();

            // Assert
            Assert.Equal(10, list.Count);
        }
    }
}
