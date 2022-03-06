namespace Charterio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PaymentMethod
    {
        [Key]
        public int Id { get; init; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
