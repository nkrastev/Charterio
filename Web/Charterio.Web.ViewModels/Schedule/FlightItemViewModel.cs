namespace Charterio.Web.ViewModels.Schedule
{
    using System;

    public class FlightItemViewModel
    {
        public int Id { get; set; }

        public string StartDestination { get; set; }

        public string EndDestination { get; set; }

        public DateTime StartDate { get; set; }

        public int StartDateUtcPosition { get; set; }

        public DateTime EndDate { get; set; }

        public int EndDateUtcPosition { get; set; }
    }
}
