namespace Charterio.Services.Data.Contacts
{
    using System.Linq;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Web.ViewModels.Contacts;
    using Ganss.XSS;

    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext db;
        private readonly IHtmlSanitizer htmlSanitizer;

        public ContactService(ApplicationDbContext db, IHtmlSanitizer htmlSanitizer)
        {
            this.db = db;
            this.htmlSanitizer = htmlSanitizer;
        }

        public void SaveUserQuestion(ContactsViewModel input)
        {
            var dataToBeSaved = new UserQuestion
            {
                UserName = this.htmlSanitizer.Sanitize(input.Name),
                UserEmail = this.htmlSanitizer.Sanitize(input.Email),
                UserPhone = this.htmlSanitizer.Sanitize(input.Phone),
                Question = this.htmlSanitizer.Sanitize(input.Message),
                IsAnswered = false,
            };

            this.db.UserQuestions.Add(dataToBeSaved);
            this.db.SaveChanges();
        }

        public RegisteredUserContactsViewModel GetAspNetUserByUserName(string userName)
        {
            var user = this.db
                .Users
                .Where(x => x.UserName == userName)
                .Select(x => new RegisteredUserContactsViewModel
                {
                    Fullname = x.FirstName + " " + x.LastName,
                    Email = x.UserName,
                    Phone = x.PhoneNumber,
                })
                .FirstOrDefault();
            return user;
        }
    }
}
