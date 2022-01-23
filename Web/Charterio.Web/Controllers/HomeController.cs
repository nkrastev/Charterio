namespace Charterio.Web.Controllers
{
    using System.Diagnostics;

    using Charterio.Services.Data;
    using Charterio.Web.ViewModels;
    using Charterio.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IFlightService flightService;

        public HomeController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        public IActionResult Index()
        {
            var homeViewModel = new SearchViewModel
            {
                Cheapest3Flights = this.flightService.GetCheapest3Flights(),
            };

            var airportsList = this.flightService.GetAllAirports();

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

        public IActionResult Tos()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        [Route("/NotFound")]
        public IActionResult PageNotFound()
        {
            return this.View();
        }
    }
}
