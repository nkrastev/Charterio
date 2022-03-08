namespace Charterio.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Services.Data.Flight;
    using Charterio.Web.ViewModels;
    using Charterio.Web.ViewModels.Administration.Flight;
    using Charterio.Web.ViewModels.Airport;
    using Charterio.Web.ViewModels.Flight;
    using Charterio.Web.ViewModels.Result;
    using Charterio.Web.ViewModels.Search;

    public class FlightService : IFlightService
    {
        private readonly ApplicationDbContext db;
        private readonly IAllotmentService allotmentService;

        public FlightService(ApplicationDbContext db, IAllotmentService allotmentService)
        {
            this.db = db;
            this.allotmentService = allotmentService;
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
            var flightResults = this.db.Offers.
                Where(
                    x => x.StartAirport.IataCode == terms.StartApt &&
                    x.EndAirport.IataCode == terms.EndApt &&
                    x.StartTimeUtc > DateTime.UtcNow &&
                    x.StartTimeUtc >= terms.StartFlightDate &&
                    x.EndTimeUtc <= terms.EndFlightDate &&
                    x.IsActiveInWeb == true)
                .Select(x => new ResultViewModel()
                {
                    Id = x.Id,
                    StartAptName = x.StartAirport.Name,
                    StartAptUtc = x.StartAirport.UtcPosition,
                    EndAptName = x.EndAirport.Name,
                    FlightStartDate = x.StartTimeUtc,
                    AvailableSeats = this.allotmentService.GetAvailableSeatsForOffer(x.Id),
                    Price = x.Price,
                })
                .OrderBy(x => x.Price)
                .ToList();

            return flightResults;
        }

        public FlightViewModel GetFlightById(int id)
        {
            // flight have to be in future
            var flight = this.db.Offers.FirstOrDefault(x => x.Id == id && x.IsActiveInWeb == true && x.StartTimeUtc > DateTime.UtcNow);

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
                Luggage = flight.Luggage,
                Catering = flight.Categing,
                FlightNumber = flightNumber.Number,
                Price = flight.Price,
                DistanceInKm = this.CalculateDistance(startAirport.Latitude, endAirport.Latitude, startAirport.Longtitude, endAirport.Longtitude),
            };
            return data;
        }

        public bool IsFlightExisting(int id)
        {
            var flight = this.db.Offers.FirstOrDefault(x => x.Id == id);

            if (flight == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ICollection<Cheapest3FlightsViewModel> GetCheapest3Flights()
        {
            var flights = this.db.Offers.Where(x => x.IsActiveInWeb).ToList().OrderBy(x => x.Price).Take(3).ToList();
            var data = new List<Cheapest3FlightsViewModel>();

            foreach (var flight in flights)
            {
                var startAirport = this.db.Airports.Where(x => x.Id == flight.StartAirportId).FirstOrDefault();
                var endAirport = this.db.Airports.Where(x => x.Id == flight.EndAirportId).FirstOrDefault();

                data.Add(new Cheapest3FlightsViewModel
                {
                    Id = flight.Id,
                    Start = startAirport.IataCode,
                    StartDate = flight.StartTimeUtc,
                    StartUtcPosition = flight.StartAirport.UtcPosition,
                    End = endAirport.IataCode,
                    EndDate = flight.EndTimeUtc,
                    EndUtcPosition = flight.EndAirport.UtcPosition,
                    Price = flight.Price,
                });
            }

            return data;
        }

        public double GetOfferPrice(int id)
        {
            var offer = this.db.Offers.Where(x => x.Id == id && x.IsActiveInWeb).FirstOrDefault();
            return offer.Price;
        }

        public string GetOfferAirportsAsString(int offer)
        {
            var airports = this.db.Offers.Where(x => x.Id == offer).Select(x => new
                    {
                        Start = x.StartAirport.IataCode,
                        End = x.EndAirport.IataCode,
                    })
                .FirstOrDefault();
            return airports.Start + " - " + airports.End;
        }

        // Administration Methods
        public List<FlightAdminViewModel> GetAllFlights()
        {
            var list = this.db.Flights.Select(x => new FlightAdminViewModel
            {
                Id = x.Id,
                Number = x.Number,
                Plane = x.Plane.Model,
                Company = x.Company.Name,
            }).ToList();

            return list;
        }

        public FlightAdminDropdownsViewModel GetDropdowns()
        {
            var model = new FlightAdminDropdownsViewModel
            {
                AirplaneForDropDown = this.db.Planes.Select(a => new Web.ViewModels.Administration.Airplane.AirplaneViewModel
                {
                    Id = a.Id,
                    Model = a.Model,
                }).ToList(),
                CompanyForDropDown = this.db.Companies.Select(ap => new Web.ViewModels.Administration.Company.CompanyViewModel
                {
                    Id = ap.Id,
                    Name = ap.Name,
                }).ToList(),
            };
            return model;
        }

        public void Add(FlightAdminAddViewModel model)
        {
            if (model != null && this.IsANumber(model.PlaneId) && this.IsANumber(model.CompanyId))
            {
                var flight = new Charterio.Data.Models.Flight
                {
                    Number = model.Number,
                    Plane = this.db.Planes.Where(x => x.Id == int.Parse(model.PlaneId)).FirstOrDefault(),
                    Company = this.db.Companies.Where(x => x.Id == int.Parse(model.CompanyId)).FirstOrDefault(),
                };

                this.db.Flights.Add(flight);
                this.db.SaveChanges();
            }
        }

        public void Edit(FlightAdminViewModel model)
        {
            var flight = this.db.Flights.Where(x => x.Id == model.Id).FirstOrDefault();

            if (flight != null && this.IsANumber(model.Plane) && this.IsANumber(model.Company))
            {
                flight.Number = model.Number;
                flight.Plane = this.db.Planes.Where(x => x.Id == int.Parse(model.Plane)).FirstOrDefault();
                flight.Company = this.db.Companies.Where(x => x.Id == int.Parse(model.Company)).FirstOrDefault();
                this.db.SaveChanges();
            }
        }

        public FlightAdminViewModel GetById(int id)
        {
            var flight = this.db.Flights.Where(x => x.Id == id).Select(x => new FlightAdminViewModel
            {
                Id = x.Id,
                Number = x.Number,
                AirplaneForDropDown = this.db.Planes.Select(a => new Web.ViewModels.Administration.Airplane.AirplaneViewModel
                {
                    Id = a.Id,
                    Model = a.Model,
                }).ToList(),
                CompanyForDropDown = this.db.Companies.Select(ap => new Web.ViewModels.Administration.Company.CompanyViewModel
                {
                    Id = ap.Id,
                    Name = ap.Name,
                }).ToList(),
            })
                .FirstOrDefault();
            if (flight != null)
            {
                return flight;
            }
            else
            {
                return null;
            }
        }

        // Private method for frontend calculation
        private double CalculateDistance(double latitude1, double latitude2, double longtitude1, double longtitude2)
        {
            var lon1 = this.ToRadians(longtitude1);
            var lon2 = this.ToRadians(longtitude2);
            var lat1 = this.ToRadians(latitude1);
            var lat2 = this.ToRadians(latitude2);

            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) + (Math.Cos(lat1) * Math.Cos(lat2) * Math.Pow(Math.Sin(dlon / 2), 2));

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in
            // kilometers. Use 3956
            // for miles
            double r = 6371;

            // calculate the result
            return c * r;
        }

        private double ToRadians(double angleIn10thofaDegree)
        {
            return angleIn10thofaDegree * Math.PI / 180;
        }

        private bool IsANumber(string item)
        {
            bool canParse = int.TryParse(item, out var numberItem);
            if (canParse)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
