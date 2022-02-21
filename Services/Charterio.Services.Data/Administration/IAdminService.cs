namespace Charterio.Services.Data.Administration
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Administration.AdminMain;

    public interface IAdminService
    {
        List<UserQuestionAdminViewModel> GetLast10UserQuestions();

        List<TicketsAdminViewModel> GetLast10Tickets();
    }
}
