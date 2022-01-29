namespace Charterio.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Charterio.Data.Common.Models;
    using global::Data.Models;

    public class Ticket : IAuditInfo
    {
        public int Id { get; set; }

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

        public Payment Payment { get; set; }

        public int? PaymentId { get; set; }

        [Required]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public virtual ICollection<TicketPassenger> TicketPassangers { get; set; } = new HashSet<TicketPassenger>();

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
