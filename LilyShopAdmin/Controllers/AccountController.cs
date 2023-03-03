using Microsoft.AspNetCore.Mvc;
using LilyShopAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace LilyShopAdmin.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [Route("/", Name = "Default")]
        [Route("admin/login", Name = "AdminLogin")]
        public IActionResult Login()
        {
            ViewBag.LoginMessage = "Xin mời đăng nhập tài khoản";
            ViewBag.LoginClass = "alert-info";
            return View();
        }
        [HttpPost]
        [Route("/", Name = "Default")]
        [Route("admin/login", Name = "AdminLogin")]
        public IActionResult Login(Account data)
        {
            DBContext context = new DBContext();
            var item = context.Accounts.FirstOrDefault(x => x.Username == data.Username 
                                                         && x.Password == data.Password);

            if (item == null)
            {
                ViewBag.LoginMessage = "Tài khoản không hợp lệ";
                ViewBag.LoginClass = "alert-danger";
                return View();
            }
            else
            {
                ViewBag.LoginMessage = "Đăng nhập thành công";
                ViewBag.LoginClass = "alert-success";
                ModelState.Clear();

                HttpContext.Session.SetString("Username", item.Username);
                HttpContext.Session.SetString("FullName", item.FullName ?? string.Empty);
                HttpContext.Session.SetString("Avatar", item.Avatar ?? string.Empty);

                return RedirectToRoute("AdminHome");
            }
        }
        [HttpGet]
        [Route("admin/logout", Name = "AdminLogout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        [Route("admin/chinh-sua-tai-khoan", Name = "ChangePassword")]
        public IActionResult ChangePassword(string username)
        {
            DBContext db = new DBContext();
            var data = db.Accounts.Where(x => x.Username == username).FirstOrDefault();
            return View();
        }

        [HttpPost]
        [Route("admin/chinh-sua-tai-khoan", Name = "ChangePassword")]
        public IActionResult ChangePassword(Account item)
        {
            DBContext db = new DBContext();
            
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToRoute("AdminLogin");

        }
    }
}
