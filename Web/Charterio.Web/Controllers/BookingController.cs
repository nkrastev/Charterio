﻿namespace Charterio.Web.Controllers
{
    using System;
    using System.Collections.Generic;
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
        public IActionResult Processing(string stripeToken, string stripeEmail)
        {
            Dictionary<string, string> metadata = new()
            {
                { "Product", "Flight Ticket" },
                { "Quantity", "1" },
            };
            var options = new ChargeCreateOptions
            {
                Amount = 1700,
                Currency = "EUR",
                Description = "Payment for ticket with id ....",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
                Metadata = metadata,
            };
            var service = new ChargeService();
            Charge charge = service.Create(options);
            return this.View();
        }

        [HttpPost]
        public IActionResult ChargeChange()
        {
            var json = new StreamReader(this.HttpContext.Request.Body).ReadToEnd();

            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json, this.Request.Headers["Stripe-Signature"], this.webhookSecret, throwOnApiVersionMismatch: true);
                Charge charge = (Charge)stripeEvent.Data.Object;
                switch (charge.Status)
                {
                    case "succeeded":
                        // This is an example of what to do after a charge is successful
                        charge.Metadata.TryGetValue("Product", out string product);
                        charge.Metadata.TryGetValue("Quantity", out string quantity);

                        // SAVE SOMETHING TO THE DB
                        break;
                    case "failed":
                        // Code to execute on a failed charge
                        break;
                }
            }
            catch (Exception e)
            {
                // Console.WriteLine(e.Message);
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}