namespace Charterio.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Braintree;
    using Charterio.Data.Models;
    using Charterio.Global;
    using Charterio.Services.Data;
    using Charterio.Services.Data.Flight;
    using Charterio.Services.Data.Ticket;
    using Charterio.Services.Payment.ViaBraintree;
    using Charterio.Services.Payment.ViaStripe;
    using Charterio.Web.ViewModels.Booking;
    using Charterio.Web.ViewModels.Ticket;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Stripe.Checkout;

    [Authorize]
    public class BookingController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAllotmentService allotmentService;
        private readonly IFlightService flightService;
        private readonly ITicketService ticketService;
        private readonly IStripeService stripeService;
        private readonly IBraintreeService braintreeService;

        public BookingController(
            UserManager<ApplicationUser> userManager,
            IAllotmentService allotmentService,
            IFlightService flightService,
            ITicketService ticketService,
            IStripeService stripeService,
            IBraintreeService braintreeService)
        {
            this.userManager = userManager;
            this.allotmentService = allotmentService;
            this.flightService = flightService;
            this.ticketService = ticketService;
            this.stripeService = stripeService;
            this.braintreeService = braintreeService;
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

            // ticket is valid, user has access, vizualize data and prepare BrainTree for payment
            var gateway = this.braintreeService.GetGateway();
            var clientToken = gateway.ClientToken.Generate();  // Genarate a token
            this.ViewBag.ClientToken = clientToken;

            // send active payment methods to view
            this.ViewBag.ActivePaymentMethods = this.ticketService.GetActivePaymentMethods();

            return this.View(ticket);
        }

        // Stripe payments
        [HttpPost]
        public IActionResult ProcessingViaStripe(string stripeToken, string stripeEmail, int ticketId)
        {
            // Check if ticketId is valid
            if (!this.ticketService.IsTicketIdValid(ticketId))
            {
                return this.Redirect("~/SomethingIsWrong");
            }

            var sessionUrl = this.stripeService.ProcessPayment(stripeToken, stripeEmail, ticketId);

            this.Response.Headers.Add("Location", sessionUrl);
            return new StatusCodeResult(303);
        }

        public IActionResult SuccessStripe(string sid)
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
                    this.stripeService.MarkTicketAsPaid(ticketId, response.Id, (double)(response.AmountTotal != null ? (response.AmountTotal / 100.0) : 0));
                }
            }
            catch (Exception)
            {
                return this.Redirect("~/AccessDenied");
            }

            return this.View();
        }

        public IActionResult FailStripe(string sid)
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

        // Braintree payment
        [HttpPost]
        public IActionResult ProcessingViaBraintree(BraintreeBookingViewModel model)
        {
            if (!this.ticketService.IsTicketIdValid(model.TicketId))
            {
                return this.Redirect("~/SomethingIsWrong");
            }

            var redirectUrl = this.braintreeService.ProcessPayment(model);

            return this.Redirect(redirectUrl);
        }

        public IActionResult SuccessBraintree()
        {
            return this.View();
        }

        public IActionResult FailBraintree()
        {
            return this.View();
        }

        public IActionResult Ticket(int tid)
        {
            // Check if ticketId is valid and user has access to it
            if (!this.ticketService.IsTicketIdValid(tid) || !this.ticketService.IfUserHasAccessToTicket(this.User.FindFirstValue(ClaimTypes.NameIdentifier), tid))
            {
                return this.Redirect("~/SomethingIsWrong");
            }

            var data = this.ticketService.GetTicketById(tid);

            return this.View(data);
        }
    }
}
