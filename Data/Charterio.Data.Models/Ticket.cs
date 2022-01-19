namespace Charterio.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using global::Data.Models;

    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string TicketCode { get; set; }

        [Required]
        public TicketStatus TicketStatus { get; set; }

        public int TicketStatusId { get; set; }

        [Required]
        public TicketIssuer TicketIssuer { get; set; }

        public int TicketIssuerId { get; set; }

        [Required]
        public int OfferId { get; set; }

        public Offer Offer { get; set; }

        [Required]
        public Payment Payment { get; set; }

        public int PaymentId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<TicketPassanger> TicketPassangers { get; set; } = new HashSet<TicketPassanger>();
    }
}
