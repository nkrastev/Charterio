namespace Charterio.Services.Data
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Schedule;

    public interface IScheduleService
    {
        ICollection<FlightItemViewModel> GetFlightsForSchedule();
    }
}
