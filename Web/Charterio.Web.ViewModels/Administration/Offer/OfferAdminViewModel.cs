namespace Charterio.Web.ViewModels.Administration.Offer
{
    using System;
    using System.Collections.Generic;

    public class OfferAdminViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string FlightNumber { get; set; }

        public string StartAirportCode { get; set; }

        public DateTime StartUTC { get; set; }

        public string EndAirportCode { get; set; }

        public DateTime EndUTC { get; set; }

        public int AllotmentCount { get; set; }

        public double Price { get; set; }

        public string Luggage { get; set; }

        public string Catering { get; set; }

        public bool IsActiveInWeb { get; set; }

        public bool IsActiveInAdmin { get; set; }

        public ICollection<OfferAdminFlightDropDownViewModel> FlightsDropDown { get; set; } = new List<OfferAdminFlightDropDownViewModel>();

        public ICollection<OfferAdminAirportDropDownViewModel> AirportsDropDown { get; set; } = new List<OfferAdminAirportDropDownViewModel>();
    }
}