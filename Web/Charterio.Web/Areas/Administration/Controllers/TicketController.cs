namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Common;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class TicketController : Controller
    {
        public IActionResult Index()
        {
            return this.View();
        }
    }
}
