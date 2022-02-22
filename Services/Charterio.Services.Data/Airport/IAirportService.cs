namespace Charterio.Services.Data.Airport
{
    using Charterio.Web.ViewModels.Administration.Airport;
    using System.Collections.Generic;

    public interface IAirportService
    {
        List<AirportViewModel> GetAll();

        void Add(AirportAddViewModel model);

        AirportViewModel GetById(int id);

        void Edit(AirportViewModel model);
    }
}
