namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Common;
    using Charterio.Services.Data.Administration;
    using Charterio.Web.Controllers;
    using Charterio.Web.ViewModels.Administration.AdminMain;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IAdminService adminService;

        public AdministrationController(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public IActionResult Index()
        {
            var model = new TicketQuestionAdminViewModel();

            model.Tickets = this.adminService.GetLast10Tickets();
            model.Questions = this.adminService.GetLast10UserQuestions();

            return this.View(model);
        }
    }
}
