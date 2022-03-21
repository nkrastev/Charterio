namespace Charterio.Services.Data.Administration
{
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Global;
    using Charterio.Web.ViewModels.Administration.AdminMain;
    using Charterio.Web.ViewModels.Administration.Faq;

    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext db;

        public AdminService(ApplicationDbContext db)
        {
            this.db = db;
        }

        // Admin Home Services
        public List<TicketsAdminViewModel> GetLast10Tickets()
        {
            var tickets = this.db.Tickets
                .OrderByDescending(x => x.Id)
                .Take(10)
                .Select(x => new TicketsAdminViewModel
                {
                    Code = x.TicketCode,
                    Status = x.TicketStatus.Status,
                    Pax = x.TicketPassengers.Count(),
                })
                .ToList();
            return tickets;
        }

        public List<UserQuestionAdminViewModel> GetLast10UserQuestions()
        {
            var questions = this.db.UserQuestions
                .OrderByDescending(x => x.CreatedOn)
                .Take(10)
                .Select(x => new UserQuestionAdminViewModel
                {
                    Id = x.Id,
                    Question = x.Question,
                    CreatedOn = x.CreatedOn.ToShortDateString(),
                    ModifiedOn = x.ModifiedOn != null ? GlobalConstants.QuestionAnswered : GlobalConstants.QuestionNotAnswered,
                })
                .ToList();
            return questions;
        }
    }
}
