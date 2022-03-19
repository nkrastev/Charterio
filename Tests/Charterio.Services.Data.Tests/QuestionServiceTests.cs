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
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Moq;
    using Xunit;

    public class QuestionServiceTests
    {
        [Fact]
        public void GetAllReturnsListOfData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetAllReturnsListOfData").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var mailService = new SendGrid(configuration);

            var service = new QuestionService(dbContext, mailService);

            Assert.Empty(service.GetAll());
            dbContext.UserQuestions.Add(new UserQuestion { Question = "Test", UserName = "TestName", UserPhone = "08880", UserEmail = "test@test.com", });
            dbContext.SaveChanges();
            Assert.Single(service.GetAll());
        }

        [Fact]
        public void GetByIdReturnsCorrectQuestion()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdReturnsCorrectQuestion").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var mailService = new SendGrid(configuration);

            var service = new QuestionService(dbContext, mailService);
            dbContext.UserQuestions.Add(new UserQuestion { Question = "Test", UserName = "TestName", UserPhone = "08880", UserEmail = "test@test.com", });
            dbContext.UserQuestions.Add(new UserQuestion { Question = "Test2", UserName = "TestName2", UserPhone = "088802", UserEmail = "test2@test.com", });
            dbContext.SaveChanges();

            Assert.Equal("Test2", service.GetById(2).Question);
        }

        [Fact]
        public void GetByIdReturnsNullIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("GetByIdReturnsNullIfIdIsInvalid").Options;
            var dbContext = new ApplicationDbContext(options);

            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();

            var mailService = new SendGrid(configuration);

            var service = new QuestionService(dbContext, mailService);

            Assert.Null(service.GetById(2));
            Assert.Null(service.GetById(-1));
        }

        [Fact]

        public async Task AnswerChangesTheDbAsync()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("AnswerChangesTheDbAsync").Options;
            var dbContext = new ApplicationDbContext(options);
            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();
            var mailService = new SendGrid(configuration);
            var service = new QuestionService(dbContext, mailService);

            dbContext.Users.Add(new ApplicationUser
            {
                UserName = "administrator@charterio.com",
                Email = "administrator@charterio.com",
                FirstName = "Admin",
                LastName = "Adminov",
                PhoneNumber = "11223344",
            });
            dbContext.UserQuestions.Add(new UserQuestion { Question = "Test", UserName = "TestName", UserPhone = "08880", UserEmail = "test@test.com", });
            dbContext.SaveChanges();

            await service.Answer(
                new Web.ViewModels.Administration.Question.QuestionAnswerViewModel
                {
                    QuestionId = 1,
                    UserEmail = "test@test.com",
                    Question = "Q1",
                    Answer = "A1",
                },
                "administrator@charterio.com");

            var targetQuestion = dbContext.UserQuestions.Where(x => x.Id == 1).FirstOrDefault();

            Assert.True(targetQuestion.IsAnswered);
        }

        [Fact]

        public void ArchiveChangesValueInDb()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ArchiveChangesValueInDb").Options;
            var dbContext = new ApplicationDbContext(options);
            var appSettingsStub = new Dictionary<string, string> { { "SendGridApiKey", "3" }, };
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(appSettingsStub)
                .Build();
            var mailService = new SendGrid(configuration);
            var service = new QuestionService(dbContext, mailService);

            dbContext.UserQuestions.Add(new UserQuestion { Question = "Test", UserName = "TestName", UserPhone = "08880", UserEmail = "test@test.com", });
            dbContext.SaveChanges();

            service.Archive(1);

            var targetQuestion = dbContext.UserQuestions.Where(x => x.Id == 1).FirstOrDefault();

            Assert.True(targetQuestion.IsArchived);
        }
    }
}
