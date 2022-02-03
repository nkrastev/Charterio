namespace Charterio.Services.Data.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Data.Models;
    using Charterio.Services.Data.Flight;
    using Charterio.Web.ViewModels.Ticket;
    using global::Data.Models;

    public class TicketService : ITicketService
    {
        private readonly ApplicationDbContext db;
        private readonly IFlightService flightService;
        private readonly IAllotmentService allotmentService;

        public TicketService(ApplicationDbContext db, IFlightService flightService, IAllotmentService allotmentService)
        {
            this.db = db;
            this.flightService = flightService;
            this.allotmentService = allotmentService;
        }

        public int CreateTicket(TicketCreateViewModel input)
        {
            var countOfExistingTickets = this.db.Tickets.Count();

            // validate offer id
            if (!this.flightService.IsFlightExisting(input.OfferId))
            {
                return 0;
            }

            // validate available seats
            if (!this.allotmentService.AreSeatsAvailable(input.OfferId, input.PaxList.Count))
            {
                return 0;
            }

            // Ticket status 3: Waiting for payment, Issuer = 1: website
            var ticket = new Ticket()
            {
                TicketCode = RandomString(4) + "-" + RandomString(4) + "-" + countOfExistingTickets.ToString("D5"),
                TicketStatusId = 3,
                TicketIssuerId = 1,
                OfferId = input.OfferId,
                UserId = input.UserId,
            };

            this.db.Tickets.Add(ticket);
            this.db.SaveChanges();

            var lastAddedTicket = this.db.Tickets.OrderByDescending(x => x.Id).FirstOrDefault();

            // Adding passangers
            var passengers = new List<TicketPassenger>();
            foreach (var pax in input.PaxList)
            {
                var newPax = new TicketPassenger
                {
                    TicketId = lastAddedTicket.Id,
                    PaxTitle = pax.PaxTitle,
                    PaxFirstName = pax.PaxFirstName,
                    PaxLastName = pax.PaxLastName,
                    DOB = pax.Dob,
                };
                passengers.Add(newPax);
            }

            this.db.TicketPassengers.AddRange(passengers);
            this.db.SaveChanges();

            return lastAddedTicket.Id;
        }

        public TicketViewModel GetTicketById(int ticketId)
        {
            var targetTicket = this.db.Tickets.Where(x => x.Id == ticketId)
                .Select(x => new TicketViewModel
                {
                    TicketId = ticketId,
                    TicketCode = x.TicketCode,
                    UserId = x.UserId,
                    StartAptName = x.Offer.StartAirport.Name,
                    EndAptName = x.Offer.EndAirport.Name,
                    StartInUtc = x.Offer.StartTimeUtc.AddHours(x.Offer.StartAirport.UtcPosition).ToString(),
                    EndInUtc = x.Offer.EndTimeUtc.AddHours(x.Offer.EndAirport.UtcPosition).ToString(),
                    PaxList = this.db.TicketPassengers.Where(p => p.TicketId == ticketId)
                        .Select(p => new TicketPaxViewModel
                        {
                            PaxTitle = p.PaxTitle,
                            PaxFirstName = p.PaxFirstName,
                            PaxLastName = p.PaxLastName,
                            PaxDob = p.DOB,
                        })
                        .ToList(),
                })
                .FirstOrDefault();

            return targetTicket;
        }

        public bool IsTicketIdValid(int ticketId)
        {
            if (!this.db.Tickets.Where(x => x.Id == ticketId).Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public double CalculateTicketPrice(int ticketId)
        {
            var price = this.db.Tickets.Where(x => x.Id == ticketId).Select(x => new { PerPerson = x.Offer.Price, }).FirstOrDefault();
            var ticketPax = this.db.TicketPassengers.Where(x => x.TicketId == ticketId).Count();

            return price.PerPerson * ticketPax;
        }

        private static string RandomString(int length)
        {
            Random random = new();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
