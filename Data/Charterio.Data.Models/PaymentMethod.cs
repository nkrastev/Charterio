namespace Charterio.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class PaymentMethod
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
