namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Airport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(3)]
        public string IataCode { get; set; }

        [Required]
        public string Name { get; set; }

        public int UtcPosition { get; set; }

        public double Latitude { get; set; }

        public double Longtitude { get; set; }
    }
}
