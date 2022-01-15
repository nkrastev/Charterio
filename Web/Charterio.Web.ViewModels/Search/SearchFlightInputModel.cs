namespace Charterio.Web.ViewModels.Search
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Charterio.Common;

    public class SearchFlightInputModel : IValidatableObject
    {
        [Required]
        [RegularExpression(@"[A-Z]{3}")]
        public string StartApt { get; set; }

        [Required]
        [RegularExpression(@"[A-Z]{3}")]
        public string EndApt { get; set; }

        public DateTime StartFlightDate { get; set; }

        public DateTime EndFlightDate { get; set; }

        [Range(1, 8)]
        public int PaxCount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartApt == this.EndApt)
            {
                yield return new ValidationResult(GlobalConstants.ErrorSameAirports);
            }
        }
    }
}
