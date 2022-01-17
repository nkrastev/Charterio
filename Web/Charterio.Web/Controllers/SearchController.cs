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

        public IActionResult RedirectRequest(SearchViewModel input)
        {
            return this.RedirectToRoute("searchForFlight", input);
        }

        public IActionResult SearchFlight(SearchViewModel input)
        {
            input.AirportsForDropDown = this.flightService.GetAllAirports();
            input.FlightResults = this.flightService.GetFlightsBySearchTerms(input);


            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.View(input);
        }
    }
}
