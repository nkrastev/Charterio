namespace Charterio.Web.ViewModels.Administration.Offer
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Charterio.Common;

    public class OfferAdminAddViewModel : IValidatableObject
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string FlightNumber { get; set; }

        [Required]
        [MaxLength(3)]
        public string StartAirportCode { get; set; }

        [Required]
        public DateTime StartUTC { get; set; }

        [Required]
        [MaxLength(3)]
        public string EndAirportCode { get; set; }

        [Required]
        public DateTime EndUTC { get; set; }

        [Required]
        [Range(1, 600)]
        public int AllotmentCount { get; set; }

        [Required]
        [Range(5, 10000)]
        public double Price { get; set; }

        [Required]
        public string Luggage { get; set; }

        [Required]
        public string Catering { get; set; }

        [Required]
        public bool IsActiveInWeb { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.StartAirportCode == this.EndAirportCode)
            {
                yield return new ValidationResult(GlobalConstants.AdminSameAirports);
            }

            if (this.StartUTC <= DateTime.Now || this.EndUTC <= DateTime.Now)
            {
                yield return new ValidationResult(GlobalConstants.AdminDatesInPast);
            }

            if (this.StartUTC >= this.EndUTC)
            {
                yield return new ValidationResult(GlobalConstants.AdminEndBeforeStart);
            }
        }
    }
}
