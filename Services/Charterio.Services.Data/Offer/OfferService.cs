namespace Charterio.Services.Data.Offer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Data;
    using Charterio.Web.ViewModels.Administration.Offer;

    public class OfferService : IOfferService
    {
        private readonly ApplicationDbContext db;

        public OfferService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(OfferAdminAddViewModel model)
        {
            throw new NotImplementedException();
        }

        public void Edit(OfferAdminViewModel model)
        {
            var offer = this.db.Offers.Where(x => x.Id == model.Id).FirstOrDefault();
            if (offer != null)
            {
                offer.Name = model.Name;
                offer.FlightId = int.Parse(model.FlightNumber);
                offer.StartAirportId = int.Parse(model.StartAirportCode);
                offer.EndAirportId = int.Parse(model.EndAirportCode);

                offer.StartTimeUtc = model.StartUTC;
                offer.EndTimeUtc = model.EndUTC;

                offer.AllotmentCount = model.AllotmentCount;
                offer.Price = model.Price;
                offer.Luggage = model.Luggage;
                offer.Categing = model.Catering;

                offer.IsActiveInWeb = model.IsActiveInWeb;

                //var offerFlight = this.db.Flights.Where(x => x.Id == model.FlightNumber).FirstOrDefault();

                this.db.SaveChanges();
            }
        }

        public List<OfferAdminViewModel> GetAll()
        {
            var list = this.db.Offers
                .Where(x => x.IsActiveInAdmin)
                .Select(x => new OfferAdminViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    StartAirportCode = x.StartAirport.IataCode,
                    EndAirportCode = x.EndAirport.IataCode,
                    StartUTC = x.StartTimeUtc,
                    AllotmentCount = x.AllotmentCount,
                    Price = x.Price,
                    IsActiveInWeb = x.IsActiveInWeb,

                })
                .OrderByDescending(x => x.Id)
                .ToList();

            return list;
        }

        public OfferAdminViewModel GetById(int id)
        {
            var model = this.db.Offers
               .Where(x => x.IsActiveInAdmin && x.Id == id)
               .Select(x => new OfferAdminViewModel
               {
                   Id = x.Id,
                   Name = x.Name,
                   StartAirportCode = x.StartAirport.IataCode,
                   EndAirportCode = x.EndAirport.IataCode,
                   FlightsDropDown = this.db.Flights
                            .Select(f => new OfferAdminFlightDropDownViewModel
                               {
                                   Id = f.Id,
                                   Number = f.Number,
                               }).ToList(),
                   AirportsDropDown = this.db.Airports
                            .Select(a => new OfferAdminAirportDropDownViewModel
                               {
                                   Id = a.Id,
                                   AirportCode = a.IataCode,
                               }).ToList(),
                   StartUTC = x.StartTimeUtc,
                   EndUTC = x.EndTimeUtc,
                   Price = x.Price,
                   AllotmentCount = x.AllotmentCount,
                   Catering = x.Categing,
                   Luggage = x.Luggage,
                   IsActiveInWeb = x.IsActiveInWeb,
               })
               .FirstOrDefault();

            return model;
        }
    }
}
