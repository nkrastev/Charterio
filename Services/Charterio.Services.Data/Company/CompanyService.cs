namespace Charterio.Services.Data.Airplane
{
    using System.Collections.Generic;
    using System.Linq;

    using Charterio.Data;
    using Charterio.Services.Data.Company;
    using Charterio.Web.ViewModels.Administration.Airplane;
    using Charterio.Web.ViewModels.Administration.Company;
    using global::Data.Models;

    public class CompanyService : ICompanyService
    {
        private readonly ApplicationDbContext db;

        public CompanyService(ApplicationDbContext db)
        {
            this.db = db;
        }

        // Administration services

        public void Edit(CompanyViewModel model)
        {
            var company = this.db.Companies.Where(x => x.Id == model.Id).FirstOrDefault();
            if (company != null)
            {
                company.Name = model.Name;
                this.db.SaveChanges();
            }
        }

        public CompanyViewModel GetById(int id)
        {
            var company = this.db.Companies.Where(x => x.Id == id).Select(x => new CompanyViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).FirstOrDefault();
            if (company != null)
            {
                return company;
            }
            else
            {
                return null;
            }
        }

        public void Add(CompanyAddViewModel model)
        {
            var company = new Company
            {
                Name = model.Name,
            };

            this.db.Companies.Add(company);
            this.db.SaveChanges();
        }

        public List<CompanyViewModel> GetAll()
        {
            var companies = this.db.Companies.Select(x => new CompanyViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
            return companies;
        }
    }
}
