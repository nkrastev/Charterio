namespace Charterio.Web.Controllers
{
    using Charterio.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class DetailController : Controller
    {
        private readonly IFlightService flightService;

        public DetailController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        public IActionResult Index(int id)
        {
            var data = this.flightService.GetFlightById(id);
            if (data == null)
            {
                return this.Redirect("/NotFound");
            }

            return this.View(data);
        }
    }
}
