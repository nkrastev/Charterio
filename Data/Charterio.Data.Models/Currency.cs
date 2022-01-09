namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Currency
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public double Ratio { get; set; }

        public bool IsActive { get; set; }
    }
}
