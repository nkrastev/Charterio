namespace Charterio.Web.Controllers
{
    using Charterio.Web.ViewModels.Search;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Routing;

    public class SearchController : Controller
    {
        public IActionResult RedirectRequest(SearchFlightInputModel input)
        {
            return this.RedirectToRoute("searchForFlight", input);
        }

        public IActionResult SearchFlight(SearchFlightInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            return this.View(input);
        }
    }
}
