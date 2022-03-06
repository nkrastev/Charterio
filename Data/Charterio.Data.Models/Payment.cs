namespace Charterio.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Charterio.Data.Common.Models;

    public class Payment : IAuditInfo
    {
        public int Id { get; init; }

        public bool IsSuccessful { get; set; }

        public PaymentMethod PaymentMethod { get; set; }

        public int PaymentMethodId { get; set; }

        [Required]
        public string TransactionId { get; set; }

        [Required]
        public double Amount { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
