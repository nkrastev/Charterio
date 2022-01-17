namespace Charterio.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class ErrorController : Controller
    {
        [Route("/NotFound")]
        public IActionResult PageNotFound()
        {
            return this.View();
        }
    }
}
