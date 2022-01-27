namespace Charterio.Web.ViewModels.Booking
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class BookingPaxViewModel : IValidatableObject
    {
        [Required]
        public string PaxTitle { get; set; }

        [Required]
        public string PaxFirstName { get; set; }

        [Required]
        public string PaxLastName { get; set; }

        public string Dob { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //TODO validate DOB field if is infant or children
            throw new System.NotImplementedException();
        }
    }
}
