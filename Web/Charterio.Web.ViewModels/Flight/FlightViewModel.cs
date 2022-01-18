namespace Charterio.Web.ViewModels
{
    using System;

    public class FlightViewModel
    {
        public int Id { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public DateTime StartDate { get; set; }

        public int StartUtcPosition { get; set; }

        public DateTime EndDate { get; set; }

        public int EndUtcPosition { get; set; }

        public string Luggage { get; set; }

        public string Catering { get; set; }

        public string FlightNumber { get; set; }

        public double Price { get; set; }
    }
}
