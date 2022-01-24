namespace Charterio.Services.Data.Flight
{
    using System.Linq;

    using Charterio.Data;

    public class AllotmentService : IAllotmentService
    {
        private readonly ApplicationDbContext db;

        public AllotmentService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public bool AreSeatsAvailable(int offerId, int neededSeats)
        {
            var initialSeats = this
                .db.Offers
                .Where(x => x.Id == offerId && x.IsActiveInWeb)
                .Select(x => new
                {
                    Count = x.AllotmentCount,
                }).FirstOrDefault();

            // list of sold tickets for particular offer
            var soldTicketsForOffer = this
                .db.Tickets
                .Where(x => x.OfferId == offerId)
                .Select(x => new
                {
                    Pax = this.db.TicketPassangers.Where(t => t.TicketId == x.Id).Sum(t => t.Id),
                })
                .ToList();

            // compare needed seat with initial allotment and sum of all tickets with all paxes in them
            if (neededSeats > initialSeats.Count - soldTicketsForOffer.Sum(x => x.Pax))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
