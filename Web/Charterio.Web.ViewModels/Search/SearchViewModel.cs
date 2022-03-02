namespace Charterio.Web.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Charterio.Global;
    using Charterio.Web.ViewModels.Airport;
    using Charterio.Web.ViewModels.Flight;
    using Charterio.Web.ViewModels.Result;

    public class SearchViewModel : IValidatableObject
    {
        [Required]
        [RegularExpression(@"[A-Z]{3}", ErrorMessage = GlobalConstants.ErrorStartApt)]
        [Display(Name ="Departure destination")]
        public string StartApt { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]{3}", ErrorMessage = GlobalConstants.ErrorEndApt)]
        [Display(Name = "Arrival destination")]
        public string EndApt { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start interval date")]
        public DateTime StartFlightDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End interval date")]
        public DateTime EndFlightDate { get; set; }

        [Range(1, 8)]
        [Display(Name = "Passanger Number")]
        public int PaxCount { get; set; }

        public ICollection<AirportViewModel> AirportsForDropDown { get; set; } = new List<AirportViewModel>();

        public ICollection<ResultViewModel> FlightResults { get; set; } = new List<ResultViewModel>();

        public ICollection<Cheapest3FlightsViewModel> Cheapest3Flights { get; set; } = new List<Cheapest3FlightsViewModel>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartApt == this.EndApt)
            {
                yield return new ValidationResult(GlobalConstants.ErrorSameAirports);
            }

            if (this.StartFlightDate >= this.EndFlightDate)
            {
                yield return new ValidationResult(GlobalConstants.ErrorStartDateAfterEndDate);
            }

            if (this.StartFlightDate <= DateTime.UtcNow.Date || this.EndFlightDate <= DateTime.UtcNow.Date)
            {
                yield return new ValidationResult(GlobalConstants.ErrorSomeOfDateIsTodayOrInThePast);
            }
        }
    }
}
