namespace Charterio.Services.Data.Airport
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Administration.Airport;

    public interface IAirportService
    {
        List<AirportViewModel> GetAll();

        void Add(AirportAddViewModel model);

        AirportViewModel GetById(int id);

        void Edit(AirportViewModel model);
    }
}
