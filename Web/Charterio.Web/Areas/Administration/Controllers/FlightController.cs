namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Common;
    using Charterio.Services.Data;
    using Charterio.Web.ViewModels.Administration.Flight;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class FlightController : Controller
    {
        private readonly IFlightService flightService;

        public FlightController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        public IActionResult Index()
        {
            var model = this.flightService.GetAll();
            return this.View(model);
        }

        public IActionResult Edit(int id)
        {
            var airport = this.flightService.GetById(id);
            return this.View(airport);
        }

        [HttpPost]
        public IActionResult Edit(FlightAdminViewModel modelInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(modelInput);
            }

            this.flightService.Edit(modelInput);

            return this.RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            FlightAdminDropdownsViewModel dropDownData = this.flightService.GetDropdowns();
            return this.View(dropDownData);
        }

        [HttpPost]
        public IActionResult Add(FlightAdminAddViewModel modelInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(modelInput);
            }

            this.flightService.Add(modelInput);
            return this.RedirectToAction("Index");
        }

    }
}
