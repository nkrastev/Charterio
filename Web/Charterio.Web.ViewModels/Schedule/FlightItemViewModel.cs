namespace Charterio.Web.ViewModels.Schedule
{
    using System;

    public class FlightItemViewModel
    {
        public int Id { get; init; }

        public string StartDestination { get; init; }

        public string EndDestination { get; init; }

        public DateTime StartDate { get; init; }

        public int StartDateUtcPosition { get; init; }

        public DateTime EndDate { get; init; }

        public int EndDateUtcPosition { get; init; }
    }
}
