namespace Charterio.Web.ViewModels.Administration.AdminMain
{
    using System.Collections.Generic;

    public class TicketQuestionAdminViewModel
    {
        public List<TicketsAdminViewModel> Tickets { get; set; }

        public List<UserQuestionAdminViewModel> Questions { get; set; }
    }
}
