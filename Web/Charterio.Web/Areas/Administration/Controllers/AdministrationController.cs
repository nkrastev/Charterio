namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Common;
    using Charterio.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
