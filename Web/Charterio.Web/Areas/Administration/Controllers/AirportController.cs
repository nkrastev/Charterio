namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Common;
    using Charterio.Services.Data.Airport;
    using Charterio.Web.ViewModels.Administration.Airport;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class AirportController : Controller
    {
        private readonly IAirportService airportService;

        public AirportController(IAirportService airportService)
        {
            this.airportService = airportService;
        }
        
        public IActionResult Index()
        {
            var model = this.airportService.GetAll();
            return this.View(model);
        }
       
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AirportAddViewModel modelInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(modelInput);
            }

            this.airportService.Add(modelInput);
            return this.RedirectToAction("Index");
        }
       
        public IActionResult Edit(int id)
        {
            var airport = this.airportService.GetById(id);
            return this.View(airport);
        }

        [HttpPost]
        public IActionResult Edit(AirportViewModel modelInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(modelInput);
            }

            this.airportService.Edit(modelInput);

            return this.RedirectToAction("Index");
        }

        
    }
}
