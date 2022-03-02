namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Global;
    using Charterio.Services.Data.Company;
    using Charterio.Web.ViewModels.Administration.Company;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class CompanyController : Controller
    {
        private readonly ICompanyService companyService;

        public CompanyController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        public IActionResult Index()
        {
            var model = this.companyService.GetAll();
            return this.View(model);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CompanyAddViewModel modelInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(modelInput);
            }

            this.companyService.Add(modelInput);
            return this.RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var faq = this.companyService.GetById(id);
            return this.View(faq);
        }

        [HttpPost]
        public IActionResult Edit(CompanyViewModel modelInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(modelInput);
            }

            this.companyService.Edit(modelInput);

            return this.RedirectToAction("Index");
        }
    }
}
