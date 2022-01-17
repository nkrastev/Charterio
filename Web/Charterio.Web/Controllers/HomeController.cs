namespace Charterio.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Services.Data;
    using Charterio.Web.ViewModels;
    using Charterio.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IFlightService airportsService;


        public HomeController(IFlightService airportsService)
        {
            this.airportsService = airportsService;
        }

        public IActionResult Index()
        {
            var homeViewModel = new SearchFlightInputModel()
            {
            };

            var airportsList = this.airportsService.GetAllAirports();

            foreach (var airportItem in airportsList)
            {
                homeViewModel.AirportsForDropDown.Add(new ViewModels.Airport.AirportViewModel { IataCode = airportItem.IataCode, Name = airportItem.Name });
            }

            return this.View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
