namespace Charterio.Web.Areas.Administration.Controllers
{
    using Charterio.Data;
    using Charterio.Global;
    using Charterio.Services.Payment;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]

    public class PaymentController : Controller
    {
        private readonly IPaymentAdministrationService paymentService;

        public PaymentController(IPaymentAdministrationService paymentService)
        {
            this.paymentService = paymentService;
        }

        public IActionResult Index()
        {
            var model = this.paymentService.GetAll();
            return this.View(model);
        }

        public IActionResult Disable(int id)
        {
            this.paymentService.DisableMethodById(id);
            return this.RedirectToAction("Index");
        }

        public IActionResult Enable(int id)
        {
            this.paymentService.EnableMethodById(id);
            return this.RedirectToAction("Index");
        }
    }
}
