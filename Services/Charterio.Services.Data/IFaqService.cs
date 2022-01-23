namespace Charterio.Services.Data
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Faq;

    public interface IFaqService
    {
        FaqListViewModel GetAllFaq();
    }
}
