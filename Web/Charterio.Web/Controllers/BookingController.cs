namespace Charterio.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;

    using Charterio.Common;
    using Charterio.Data.Models;
    using Charterio.Services.Data;
    using Charterio.Services.Data.Flight;
    using Charterio.Services.Data.Ticket;
    using Charterio.Web.ViewModels.Booking;
    using Charterio.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Stripe;

    [Authorize]
    public class BookingController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAllotmentService allotmentService;
        private readonly IFlightService flightService;
        private readonly ITicketService ticketService;
        private readonly string webhookSecret = "whsec_CharterioSecretSaltAndWater17";

        public BookingController(
            UserManager<ApplicationUser> userManager,
            IAllotmentService allotmentService,
            IFlightService flightService,
            ITicketService ticketService)
        {
            this.userManager = userManager;
            this.allotmentService = allotmentService;
            this.flightService = flightService;
            this.ticketService = ticketService;
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
        public async Task<IActionResult> Index(BookingViewModel inputData)
        {
            var user = await this.userManager.GetUserAsync(this.User);

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

            // prepare passengers for ticket creation
            var passengers = new List<TicketPaxCreateViewModel>();
            foreach (var pax in inputData.PaxList)
            {
                passengers.Add(new TicketPaxCreateViewModel
                {
                    PaxTitle = pax.PaxTitle,
                    PaxFirstName = pax.PaxFirstName,
                    PaxLastName = pax.PaxLastName,
                    Dob = pax.Dob,
                });
            }

            // create and save ticket
            var currentTicketId = this.ticketService.CreateTicket(new ViewModels.Ticket.TicketCreateViewModel
            {
                OfferId = inputData.OfferId,
                UserId = user.Id,
                PaxList = passengers,
            });

            // check if currentTicketId is 0 then some error in creation ticket
            return this.RedirectToAction("ConfirmData", new { tid = currentTicketId });
        }

        public async Task<IActionResult> ConfirmData(int tid)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            // Check if ticketId is valid
            if (!this.ticketService.IsTicketIdValid(tid))
            {
                return this.Redirect("~/SomethingIsWrong");
            }

            var ticket = this.ticketService.GetTicketById(tid);

            // ticket is valid, check if user has access to it
            if (user.Id != ticket.UserId)
            {
                return this.Redirect("~/AccessDenied");
            }

            // ticket is valid, user has access, vizualize data
            return this.View(ticket);
        }

        [HttpPost]
        public IActionResult Processing(string stripeToken, string stripeEmail, int ticketId)
        {
            // Check if ticketId is valid
            if (!this.ticketService.IsTicketIdValid(ticketId))
            {
                return this.Redirect("~/SomethingIsWrong");
            }

            var options = new ChargeCreateOptions
            {
                Amount = (long?)(this.ticketService.CalculateTicketPrice(ticketId) * 100),
                Currency = "EUR",
                Description = $"Payment for ticket with id {ticketId}",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
                Metadata = new Dictionary<string, string> { { "Product", "Flight Ticket" }, { "Quantity", "1" }, },
            };
            var service = new ChargeService();
            var response = service.Create(options);

            if (response.Status == "succeeded")
            {
                // charge is OK, mark it, send confirmation, prepare view
                this.ticketService.MarkTicketAsPaidviaStripe(ticketId, response.Id, response.PaymentMethod, response.AmountCaptured);
            }
            else
            {
                // charge is failed
            }

            return this.View();
        }

    }
}
