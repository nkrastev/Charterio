namespace Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Flight
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        public int PlaneId { get; set; }

        [Required]
        public Plane Plane { get; set; }

        public int CompanyId { get; set; }

        [Required]
        public Company Company { get; set; }
    }
}
