namespace Charterio.Services.Data.Question
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Global;
    using Charterio.Services.Data.SendGrid;
    using Charterio.Web.ViewModels.Administration.Question;
    using Ganss.XSS;

    public class QuestionService : IQuestionService
    {
        private readonly ApplicationDbContext db;
        private readonly ISendGrid emailSender;

        public QuestionService(ApplicationDbContext db, ISendGrid emailSender)
        {
            this.db = db;
            this.emailSender = emailSender;
        }

        public List<QuestionViewModel> GetAll()
        {
            var list = this.db.UserQuestions
                .Where(x => !x.IsArchived && !x.IsAnswered)
                .Select(x => new QuestionViewModel
                {
                    Id = x.Id,
                    Question = x.Question,
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    UserPhone = x.UserPhone,
                    IsAnswered = x.IsAnswered.ToString(),
                    CreatedOn = x.CreatedOn.ToString(),
                })
                .OrderByDescending(x => x.Id)
                .ToList();

            return list;
        }

        public QuestionViewModel GetById(int id)
        {
            var model = this.db.UserQuestions
                .Where(x => !x.IsArchived && !x.IsAnswered)
                .Select(x => new QuestionViewModel
                {
                    Id = x.Id,
                    Question = x.Question,
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    UserPhone = x.UserPhone,
                })
                .FirstOrDefault();

            return model;
        }

        public async Task Answer(QuestionAnswerViewModel model, string adminUsername)
        {
            var question = this.db.UserQuestions.FirstOrDefault(x => x.Id == model.QuestionId);
            var administratorUser = this.db.Users.FirstOrDefault(x => x.UserName == adminUsername);
            if (question != null && administratorUser != null)
            {
                question.IsAnswered = true;
                this.db.SaveChanges();

                await this.emailSender.SendEmailAsync(
                    GlobalConstants.SystemEmail,
                    administratorUser.FirstName + " " + administratorUser.LastName,
                    model.UserEmail,
                    GlobalConstants.AnswerToYourQuestionSubject,
                    this.ConstructHtmlAnswer(model.Question, model.Answer, administratorUser.FirstName + " " + administratorUser.LastName));
            }
        }

        public void Archive(int id)
        {
            var question = this.db.UserQuestions.Where(x => x.Id == id).FirstOrDefault();
            if (question != null)
            {
                question.IsArchived = true;
                this.db.SaveChanges();
            }
        }

        private string ConstructHtmlAnswer(string question, string answer, string sender)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<b>Question:</b><br />{question}");
            sb.AppendLine($"<hr />");
            sb.AppendLine($"<b>Answer:</b><br />{answer}");
            sb.AppendLine($"<hr />");
            sb.AppendLine($"<b>Best regards,<br />{sender}</b>");

            return sb.ToString();
        }
    }
}
