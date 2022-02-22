namespace Charterio.Services.Data.Company
{
    using System.Collections.Generic;

    using Charterio.Web.ViewModels.Administration.Company;

    public interface ICompanyService
    {
        List<CompanyViewModel> GetAll();

        void Add(CompanyAddViewModel model);

        CompanyViewModel GetById(int id);

        void Edit(CompanyViewModel model);
    }
}
