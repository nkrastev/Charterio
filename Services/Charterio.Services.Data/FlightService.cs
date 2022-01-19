namespace Charterio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Web.ViewModels;
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

            List<ResultViewModel> flightResults = new();

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

        public FlightViewModel GetFlightById(int id)
        {
            var flight = this.db.Offers.FirstOrDefault(x => x.Id == id);

            if (flight == null)
            {
                return null;
            }

            var startAirport = this.db.Airports.Where(x => x.Id == flight.StartAirportId).FirstOrDefault();
            var endAirport = this.db.Airports.Where(x => x.Id == flight.EndAirportId).FirstOrDefault();
            var flightNumber = this.db.Flights.Where(x => x.Id == flight.FlightId).FirstOrDefault();

            var data = new FlightViewModel
            {
                Id = flight.Id,
                Start = startAirport.Name,
                End = endAirport.Name,
                StartDate = flight.StartTimeUtc,
                EndDate = flight.EndTimeUtc,
                StartUtcPosition = flight.StartAirport.UtcPosition,
                EndUtcPosition = flight.EndAirport.UtcPosition,
                Luggage = "luggage data insert field in DB",
                Catering = "catering data insert field in DB !!!",
                FlightNumber = flightNumber.Number,
                Price = flight.Price,
            };
            return data;
        }
    }
}
