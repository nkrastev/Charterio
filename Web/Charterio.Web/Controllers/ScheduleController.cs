namespace Charterio.Web.Controllers
{
    using Charterio.Services.Data;
    using Charterio.Web.ViewModels.Schedule;
    using Microsoft.AspNetCore.Mvc;

    public class ScheduleController : Controller
    {
        private readonly IScheduleService scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            this.scheduleService = scheduleService;
        }

        public IActionResult Index()
        {
            var listOfFlights = new FlightListViewModel
            {
                FlightList = this.scheduleService.GetFlightsForSchedule(),
            };

            return this.View(listOfFlights);
        }
    }
}
