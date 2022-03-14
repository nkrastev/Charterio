namespace Charterio.Services.Data.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Services.Data.Flight;
    using Charterio.Web.ViewModels.Api;

    public class FlightApiService : IFlightApiService
    {
        private readonly ApplicationDbContext db;
        private readonly IAllotmentService allotmentService;

        public FlightApiService(ApplicationDbContext db, IAllotmentService allotmentService)
        {
            this.db = db;
            this.allotmentService = allotmentService;
        }

        public List<ApiViewModel> GetData()
        {
            var data = this.db.Offers.Where(x => x.IsActiveInWeb && x.StartTimeUtc > DateTime.UtcNow.AddDays(1))
                .Select(x => new ApiViewModel
                {
                    Id = x.Id,
                    From = x.StartAirport.IataCode,
                    To = x.EndAirport.IataCode,
                    DepartureUtc = x.StartTimeUtc.ToString(),
                    ArrivalUtc = x.EndTimeUtc.ToString(),
                    AvailableSeats = this.allotmentService.GetAvailableSeatsForOffer(x.Id),
                    PricePerSeat = x.Price,
                })
                .ToList();
            return data;
        }
    }
}
