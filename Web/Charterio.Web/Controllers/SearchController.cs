namespace Charterio.Web.Controllers
{
    using Charterio.Services.Data;
    using Charterio.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;

    public class SearchController : Controller
    {
        private readonly IFlightService flightService;

        public SearchController(IFlightService flightService)
        {
            this.flightService = flightService;
        }

        public IActionResult RedirectRequest(SearchFlightInputModel input)
        {
            return this.RedirectToRoute("searchForFlight", input);
        }

        public IActionResult SearchFlight(SearchFlightInputModel input)
        {

            var airportsList = this.flightService.GetAllAirports();
            var flightsList = this.flightService.GetFlightsBySearchTerms(input);
            
            foreach (var airportItem in airportsList)
            {
                input.AirportsForDropDown.Add(new ViewModels.Airport.AirportViewModel { IataCode = airportItem.IataCode, Name = airportItem.Name });
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.View(input);
        }
    }
}
