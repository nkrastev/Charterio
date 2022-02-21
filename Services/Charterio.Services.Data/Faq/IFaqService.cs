namespace Charterio.Services.Data
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Administration.Faq;
    using Charterio.Web.ViewModels.Faq;

    public interface IFaqService
    {
        IEnumerable<FaqItemViewModel> GetAllFaq(int page, int itemsPerPage);

        int GetCount();

        // Administration services
        List<FaqViewModel> GetAll();

        void Add(FaqAddViewModel model);

        void Edit(FaqViewModel model);

        FaqViewModel GetById(int id);

        void Delete(int id);
    }
}
