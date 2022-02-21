namespace Charterio.Services.Data.Airplane
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Administration.Airplane;

    public interface IAirplaneService
    {
        List<AirplaneViewModel> GetAll();

        void Add(AirplaneAddViewModel model);

        AirplaneViewModel GetById(int id);

        void Edit(AirplaneViewModel model);

        void Delete(int id);
    }
}
