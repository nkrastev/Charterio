namespace Charterio.Web.ViewModels.Booking
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BookingViewModel
    {
        [Required]
        public string CustomerName { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        [Required]
        public string CustomerPhone { get; set; }

        // Validate if offer ID is a valid one in controller
        [Required]
        public int OfferId { get; set; }

        // Validate if enought seats available in controller
        [Required]
        [Range(1, 8)]
        [Display(Name = "Passenger count")]
        public int PaxCount { get; set; }

        public string Airports { get; set; }

        public double PricePerTicket { get; set; }

        public List<BookingPaxViewModel> PaxList { get; set; }
    }
}
