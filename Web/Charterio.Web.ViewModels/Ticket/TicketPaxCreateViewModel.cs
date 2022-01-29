namespace Charterio.Web.ViewModels.Ticket
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;

    using Charterio.Common;

    public class TicketPaxCreateViewModel : IValidatableObject
    {
        [Required]
        public string PaxTitle { get; set; }

        [Required]
        [MinLength(3)]
        public string PaxFirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string PaxLastName { get; set; }

        public string Dob { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.PaxTitle == GlobalConstants.PaxTitleInf)
            {
                string pattern = "dd-MM-yyyy";
                DateTime parsedDate;
                if (!DateTime.TryParseExact(this.Dob, pattern, null, DateTimeStyles.None, out parsedDate))
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
