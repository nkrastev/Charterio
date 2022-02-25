namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Common;
    using Charterio.Services.Data;
    using Charterio.Services.Data.Offer;
    using Charterio.Web.ViewModels.Administration.Offer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class OfferController : Controller
    {
        private readonly IOfferService offerService;

        public OfferController(IOfferService offerService)
        {
            this.offerService = offerService;
        }

        public IActionResult Index()
        {
            var model = this.offerService.GetAll();
            return this.View(model);
        }

        public IActionResult Edit(int id)
        {
            var offer = this.offerService.GetById(id);
            return this.View(offer);
        }

        [HttpPost]
        public IActionResult Edit(OfferAdminViewModel modelInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(modelInput);
            }

            this.offerService.Edit(modelInput);

            return this.RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            OfferAdminDropDownsViewModel dropDownData = this.offerService.GetDropdowns();
            return this.View(dropDownData);
        }

        [HttpPost]
        public IActionResult Add(OfferAdminAddViewModel modelInput)
        {

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction("Add");
            }

            this.offerService.Add(modelInput);
            return this.RedirectToAction("Index");
        }
    }
}
