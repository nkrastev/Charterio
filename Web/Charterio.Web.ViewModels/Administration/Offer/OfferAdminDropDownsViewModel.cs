namespace Charterio.Web.ViewModels.Administration.Offer
{
    using System;
    using System.Collections.Generic;

    public class OfferAdminDropDownsViewModel
    {
        public ICollection<OfferAdminFlightDropDownViewModel> FlightsDropDown { get; set; } = new List<OfferAdminFlightDropDownViewModel>();

        public ICollection<OfferAdminAirportDropDownViewModel> AirportsDropDown { get; set; } = new List<OfferAdminAirportDropDownViewModel>();
    }
}
