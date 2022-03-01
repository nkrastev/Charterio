namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Common;
    using Charterio.Services.Data.Ticket;
    using Charterio.Web.ViewModels.Administration.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class TicketController : Controller
    {
        private readonly ITicketService ticketService;

        public TicketController(ITicketService ticketService)
        {
            this.ticketService = ticketService;
        }

        public IActionResult Index()
        {
            var model = this.ticketService.GetAll();
            return this.View(model);
        }
        
        public IActionResult Cancel(int id)
        {
            this.ticketService.MarkTicketAsCancelled(id);
            return this.RedirectToAction("Index");
        }
    }
}
