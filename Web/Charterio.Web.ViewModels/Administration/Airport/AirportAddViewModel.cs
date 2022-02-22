namespace Charterio.Web.ViewModels.Administration.Airport
{
    using System.ComponentModel.DataAnnotations;

    public class AirportAddViewModel
    {
        [Required]
        [MaxLength(3)]
        public string IataCode { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int UtcPosition { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longtitude { get; set; }
    }
}
