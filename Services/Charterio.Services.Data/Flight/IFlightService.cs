namespace Charterio.Services.Data
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels;
    using Charterio.Web.ViewModels.Airport;
    using Charterio.Web.ViewModels.Flight;
    using Charterio.Web.ViewModels.Result;
    using Charterio.Web.ViewModels.Search;

    public interface IFlightService
    {
        ICollection<AirportViewModel> GetAllAirports();

        ICollection<ResultViewModel> GetFlightsBySearchTerms(SearchViewModel term);

        FlightViewModel GetFlightById(int id);

        ICollection<Cheapest3FlightsViewModel> GetCheapest3Flights();

        bool IsFlightExisting(int id);

        double GetOfferPrice(int id);

        string GetOfferAirportsAsString(int offer);
    }
}
