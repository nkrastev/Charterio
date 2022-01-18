namespace Charterio.Services.Data
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Airport;
    using Charterio.Web.ViewModels.Result;
    using Charterio.Web.ViewModels.Search;

    public interface IFlightService
    {
        ICollection<AirportViewModel> GetAllAirports();

        ICollection<ResultViewModel> GetFlightsBySearchTerms(SearchViewModel term);
    }
}
