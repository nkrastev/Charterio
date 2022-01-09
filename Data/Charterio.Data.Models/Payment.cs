namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using Charterio.Data.Models;

    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public Ticket Ticket { get; set; }

        public int TicketId { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public int PaymentMethodId { get; set; }

        [Required]
        public string TransactionId { get; set; }

        [Required]
        public string TransactionCode { get; set; }

        [Required]
        public double Amount { get; set; }
    }
}
