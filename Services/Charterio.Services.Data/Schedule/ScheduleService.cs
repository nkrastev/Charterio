namespace Charterio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Web.ViewModels.Schedule;

    public class ScheduleService : IScheduleService
    {
        private readonly ApplicationDbContext db;

        public ScheduleService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<FlightItemViewModel> GetFlightsForSchedule()
        {
            var flights = this
                .db.Offers
                .Where(x => x.StartTimeUtc > DateTime.UtcNow && x.IsActiveInWeb)
                .Select(x => new FlightItemViewModel
                {
                    Id = x.Id,
                    StartDestination = x.StartAirport.Name,
                    EndDestination = x.EndAirport.Name,
                    StartDate = x.StartTimeUtc,
                    StartDateUtcPosition = x.StartAirport.UtcPosition,
                    EndDate = x.EndTimeUtc,
                    EndDateUtcPosition = x.EndAirport.UtcPosition,
                })
                .ToList()
                .OrderBy(x => x.StartDate)
                .ToList();

            return flights;
        }
    }
}
