namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Global;
    using Charterio.Services.Data.Airplane;
    using Charterio.Web.ViewModels.Administration.Airplane;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class AirplaneController : Controller
    {
        private readonly IAirplaneService airplaneService;

        public AirplaneController(IAirplaneService airplaneService)
        {
            this.airplaneService = airplaneService;
        }

        public IActionResult Index()
        {
            var model = this.airplaneService.GetAll();
            return this.View(model);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AirplaneAddViewModel modelInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(modelInput);
            }

            this.airplaneService.Add(modelInput);
            return this.RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var faq = this.airplaneService.GetById(id);
            return this.View(faq);
        }

        [HttpPost]
        public IActionResult Edit(AirplaneViewModel modelInput)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(modelInput);
            }

            this.airplaneService.Edit(modelInput);

            return this.RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            this.airplaneService.Delete(id);
            return this.RedirectToAction("Index");
        }
    }
}
