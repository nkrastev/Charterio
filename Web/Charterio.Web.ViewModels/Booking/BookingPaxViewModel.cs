namespace Charterio.Web.ViewModels.Booking
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using Charterio.Global;

    public class BookingPaxViewModel : IValidatableObject
    {
        [Required]
        public string PaxTitle { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "First name")]
        public string PaxFirstName { get; set; }

        [Required]
        [MinLength(3)]
        [Display(Name = "Last name")]
        public string PaxLastName { get; set; }

        public string Dob { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.PaxTitle == GlobalConstants.PaxTitleInf)
            {
                string pattern = "dd-MM-yyyy";
                if (!DateTime.TryParseExact(this.Dob, pattern, null, DateTimeStyles.None, out DateTime parsedDate))
                {
                    yield return new ValidationResult(GlobalConstants.ErrorEmptyOrWrongDob);
                }

                if (parsedDate.AddYears(2) <= DateTime.UtcNow)
                {
                    yield return new ValidationResult(GlobalConstants.ErrorDobIsNotDobOfChild);
                }
            }
        }
    }
}
