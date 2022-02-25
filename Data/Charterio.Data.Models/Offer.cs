namespace Charterio.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Charterio.Data.Common.Models;
    using Charterio.Data.Models;

    public class Offer : IAuditInfo
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public Flight Flight { get; set; }

        public int FlightId { get; set; }

        [Required]
        public Airport StartAirport { get; set; }

        public int StartAirportId { get; set; }

        [Required]
        public Airport EndAirport { get; set; }

        public int EndAirportId { get; set; }

        [Required]
        public DateTime StartTimeUtc { get; set; }

        [Required]
        public DateTime EndTimeUtc { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public Currency Currency { get; set; }

        public int CurrencyId { get; set; }

        public string Luggage { get; set; }

        public string Categing { get; set; }

        public int AllotmentCount { get; set; }

        public bool IsActiveInWeb { get; set; }

        public bool IsActiveInAdmin { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
