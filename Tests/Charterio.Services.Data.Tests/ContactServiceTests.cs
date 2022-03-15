namespace Charterio.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Contacts;
    using Charterio.Web.ViewModels.Contacts;
    using Ganss.XSS;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class ContactServiceTests
    {
        [Fact]
        public void SaveUserQuestionSavesTheData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("SaveUserQuestionSavesTheData").Options;
            var dbContext = new ApplicationDbContext(options);
            var htmlSanitizer = new HtmlSanitizer();
            var service = new ContactService(dbContext, htmlSanitizer);

            ContactsViewModel input = new ContactsViewModel
            {
                Name = "Some name",
                Email = "test@user.com",
                Phone = "0888 888 888",
                Message = "Message not empty",
            };

            service.SaveUserQuestion(input);
            Assert.NotEmpty(dbContext.UserQuestions);
        }

        [Fact]
        public void SaveUserQuestionSkipsDangerousTagsFromTheInput()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("SaveUserQuestionSkipsDangerousTagsFromTheInput").Options;
            var dbContext = new ApplicationDbContext(options);
            var htmlSanitizer = new HtmlSanitizer();
            var service = new ContactService(dbContext, htmlSanitizer);

            ContactsViewModel input = new ContactsViewModel
            {
                Name = "Some name <script>alert('xss')</script>",
                Email = "test@user.com",
                Phone = "0888 888 888",
                Message = "Message not empty",
            };

            service.SaveUserQuestion(input);
            var savedData = dbContext.UserQuestions.Where(x => x.Id == 1).FirstOrDefault();

            Assert.NotEqual("Some name <script>alert('xss')</script>", savedData.UserName);
        }

        [Fact]
        public void RegisteredUserSendQuestionServiceReturnsItsUsername()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("RegisteredUserSendQuestionServiceReturnsItsUsername").Options;
            var dbContext = new ApplicationDbContext(options);
            var htmlSanitizer = new HtmlSanitizer();
            var service = new ContactService(dbContext, htmlSanitizer);

            var tempUser = new ApplicationUser
            {
                UserName = "user@charterio.com",
                Email = "user@charterio.com",
                FirstName = "Dimitrichko",
                LastName = "Testov",
                PhoneNumber = "00112233",
            };

            dbContext.Users.Add(tempUser);
            dbContext.SaveChanges();

            var serviceResultUser = service.GetAspNetUserByUserName("user@charterio.com");

            Assert.Equal(tempUser.Email, serviceResultUser.Email);
        }
    }
}
