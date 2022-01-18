namespace Charterio.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DetailController : Controller
    {
        public IActionResult Index(int id)
        {
            return this.View();
        }
    }
}
