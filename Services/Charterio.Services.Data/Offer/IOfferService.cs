namespace Charterio.Services.Data.Offer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Charterio.Web.ViewModels.Administration.Offer;

    public interface IOfferService
    {
        List<OfferAdminViewModel> GetAll();

        OfferAdminViewModel GetById(int id);

        void Edit(OfferAdminViewModel model);

        void Add(OfferAdminAddViewModel model);
    }
}
