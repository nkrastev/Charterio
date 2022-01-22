namespace Charterio.Web.ViewModels.Flight
{
    using System;

    public class Cheapest3FlightsViewModel
    {
        public int Id { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

        public DateTime StartDate { get; set; }

        public int StartUtcPosition { get; set; }

        public DateTime EndDate { get; set; }

        public int EndUtcPosition { get; set; }

        public double Price { get; set; }
    }
}
