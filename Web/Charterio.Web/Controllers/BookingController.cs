namespace Charterio.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
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
    using Stripe;
    using Stripe.Checkout;

    [Authorize]
    public class BookingController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAllotmentService allotmentService;
        private readonly IFlightService flightService;
        private readonly ITicketService ticketService;

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

            // ticket is valid, check if it is not paid or cancelled
            if (ticket.TicketStatusId != 3)
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

            var domain = "https://localhost:44319";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                {
                  new SessionLineItemOptions
                  {
                    Name = "Flight Ticket",
                    Quantity = 1,
                    Amount = (long?)(this.ticketService.CalculateTicketPrice(ticketId) * 100),
                    Currency = "EUR",
                    Description = GlobalConstants.StripePaymentDescription,
                  },
                },
                Metadata = new Dictionary<string, string> { { "TicketId", ticketId.ToString() } },
                Mode = "payment",
                SuccessUrl = domain + "/booking/SuccessPayment?sid={CHECKOUT_SESSION_ID}",
                CancelUrl = domain + "/booking/CancelPayment?sid={CHECKOUT_SESSION_ID}",

            };
            var service = new SessionService();
            Session session = service.Create(options);

            this.Response.Headers.Add("Location", session.Url);

            return new StatusCodeResult(303);
        }

        public IActionResult SuccessPayment(string sid)
        {
            if (sid == null)
            {
                return this.Redirect("~/AccessDenied");
            }

            var service = new SessionService();

            try
            {
                var response = service.Get(sid);
                var ticketId = int.Parse(response.Metadata["TicketId"]);

                if (response.PaymentStatus == "paid")
                {
                    // charge is OK, mark it, send confirmation, prepare view
                    this.ticketService.MarkTicketAsPaidviaStripe(ticketId, response.Id, (double)(response.AmountTotal != null ? (response.AmountTotal / 100.0) : 0));
                }
            }
            catch (Exception)
            {
                return this.Redirect("~/AccessDenied");
            }

            return this.View();
        }

        public IActionResult CancelPayment(string sid)
        {
            if (sid == null)
            {
                return this.Redirect("~/AccessDenied");
            }

            var service = new SessionService();
            try
            {
                var response = service.Get(sid);
                var ticketId = int.Parse(response.Metadata["TicketId"]);

                // mark ticket as cancelled
                this.ticketService.MarkTicketAsCancelled(ticketId);
            }
            catch (Exception)
            {
                return this.Redirect("~/AccessDenied");
            }

            return this.View();
        }

        public IActionResult Ticket(int tid)
        {
            // Check if ticketId is valid
            if (!this.ticketService.IsTicketIdValid(tid))
            {
                return this.Redirect("~/SomethingIsWrong");
            }

            var data = this.ticketService.GetTicketById(tid);

            return this.View(data);
        }
    }
}
