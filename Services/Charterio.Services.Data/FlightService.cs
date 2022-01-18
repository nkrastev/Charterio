namespace Charterio.Services.Data
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Web.ViewModels.Airport;
    using Charterio.Web.ViewModels.Result;
    using Charterio.Web.ViewModels.Search;
    using global::Data.Models;

    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext db;

        public FlightService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public ICollection<AirportViewModel> GetAllAirports()
        {
            var allAirports = this.db.Airports.ToList();
            var allAirportsForDropDown = new List<AirportViewModel>();

            foreach (var airportItem in allAirports)
            {
                allAirportsForDropDown.Add(new AirportViewModel { IataCode = airportItem.IataCode, Name = airportItem.Name });
            }

            return allAirportsForDropDown;
        }

        public ICollection<ResultViewModel> GetFlightsBySearchTerms(SearchViewModel terms)
        {
            var targetFlights = this.db.Offers.
                Where(
                    x => x.StartAirport.IataCode == terms.StartApt &&
                    x.EndAirport.IataCode == terms.EndApt &&
                    x.StartTimeUtc >= terms.StartFlightDate &&
                    x.EndTimeUtc <= terms.EndFlightDate &&
                    x.IsActiveInWeb == true)
                .ToList();

            List<ResultViewModel> flightResults = new List<ResultViewModel>();

            foreach (var flight in targetFlights)
            {
                flightResults.Add(new ResultViewModel
                    {
                        Id = flight.Id,
                        StartApt = flight.StartAirport.Name,
                        EndApt = flight.EndAirport.Name,
                        DepartureDate = flight.StartTimeUtc,
                        Price = flight.Price,
                    });
            }

            return flightResults.OrderBy(x => x.Price).ToList();
        }
    }
}
