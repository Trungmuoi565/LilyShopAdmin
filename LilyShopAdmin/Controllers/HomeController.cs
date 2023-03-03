using Microsoft.AspNetCore.Mvc;

namespace LilyShopAdmin.Controllers
{
    public class HomeController : Controller
    {
        [Route("admin/", Name ="AdminHome")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
