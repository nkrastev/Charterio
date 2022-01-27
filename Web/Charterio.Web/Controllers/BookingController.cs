namespace Charterio.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Charterio.Common;
    using Charterio.Data.Models;
    using Charterio.Services.Data;
    using Charterio.Services.Data.Flight;
    using Charterio.Web.ViewModels.Booking;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class BookingController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAllotmentService allotmentService;
        private readonly IFlightService flightService;

        public BookingController(
            UserManager<ApplicationUser> userManager,
            IAllotmentService allotmentService,
            IFlightService flightService)
        {
            this.userManager = userManager;
            this.allotmentService = allotmentService;
            this.flightService = flightService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int offer, int pax)
        {
            // check if flight is valid to get the price
            if (!this.flightService.IsFlightExisting(offer))
            {
                return this.Redirect("~/SomethingIsWrong");
            }

            var data = new BookingViewModel();
            var user = await this.userManager.GetUserAsync(this.User);
            data.CustomerName = user.FirstName + " " + user.LastName;
            data.CustomerEmail = user.Email;
            data.CustomerPhone = user.PhoneNumber;
            data.OfferId = offer;
            data.PaxCount = pax;
            data.PricePerTicket = this.flightService.GetOfferPrice(offer);
            data.Airports = this.flightService.GetOfferAirportsAsString(offer);
            data.PaxList = new List<BookingPaxViewModel>(new BookingPaxViewModel[pax]);

            return this.View(data);
        }

        [HttpPost]
        public IActionResult Index(BookingViewModel inputData)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputData);
            }

            // check if offer exists
            if (!this.flightService.IsFlightExisting(inputData.OfferId))
            {
                this.ModelState.AddModelError(string.Empty, GlobalConstants.ErrorFlightDoesNotExists);
                return this.View(inputData);
            }

            // check for enought seats
            if (!this.allotmentService.AreSeatsAvailable(inputData.OfferId, inputData.PaxCount))
            {
                this.ModelState.AddModelError(string.Empty, GlobalConstants.ErrorNotEnoughtAvailableSeats);
                return this.View(inputData);
            }

            // create and save ticket
            return this.RedirectToAction("Confirm", new { tid = 7 });
        }

        public IActionResult Confirm(int ticketId)
        {
            // Ticket is created link to pay now
            return this.View();
        }

    }
}
