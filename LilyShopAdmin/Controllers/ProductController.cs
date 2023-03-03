using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LilyShopAdmin.Models;

namespace LilyShopAdmin.Controllers
{
    public class ProductController : Controller
    {
        private IWebHostEnvironment Environment;

        public ProductController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }
        [Route("admin/san-pham", Name = "AdminProduct")]
        public IActionResult Index()
        {
            DBContext db = new DBContext();
            var data = db.Products.Include(x => x.ProductCategory)
                                  .OrderByDescending(x => x.CreateTime)
                                  .Take(20)
                                  .ToList();
            return View(data);
        }
        [HttpGet]
        [Route("admin/chi-tiet-san-pham/{id?}", Name = "AdminProductDetail")]
        public IActionResult Detail(int? id)
        {
            DBContext db = new DBContext();
            var category = db.ProductCategories.OrderBy(x => x.Position).ToList();
            ViewBag.ProductCategory = category;

            var item = db.Products.Find(id);
            if (item == null)
                return View(new Product());
            else
                return View(item);
        }

        [HttpGet]
        [Route("admin/chinh-sua-san-pham", Name = "AdminProductEdit")]
        public IActionResult Detail(int id)
        {
            DBContext db = new DBContext();
            var data = db.Products.Where(x => x.ProductID == id).FirstOrDefault();
            return View();
        }

        [HttpPost]
        [Route("admin/chinh-sua-san-pham", Name = "AdminProductEdit")]
        public IActionResult Detail(int id, Product item)
        {
            DBContext db = new DBContext();
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToRoute("AdminProduct");

        }

        [HttpGet]
        [Route("admin/them-san-pham", Name = "AdminProductAdd")]
        public IActionResult AddProduct()
        {
            
            return View();

        }

        [HttpPost]
        [Route("admin/them-san-pham", Name = "AdminProductAdd")]
        public IActionResult AddProduct(Product item)
        {
            DBContext db = new DBContext();
            item.CreateTime = DateTime.Now;
            db.Entry(item).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToRoute("AdminProduct");

        }

        //[HttpPost]
        //[Route("admin/chi-tiet-san-pham/{id?}", Name = "AdminProductDetail")]
        //public IActionResult Detail(int? id, Product item)
        //{
        //    DBContext db = new DBContext();

        //    if (!ValidateForm(item))
        //    {
        //        var category = db.ProductCategories.OrderBy(x => x.Position).ToList();
        //        ViewBag.ProductCategory = category;
        //        return View(item);
        //    }

        //    if (id > 0)
        //    {
        //        item.ProductID = id.Value;
        //        var existItem = db.Products.Find(id);
        //        if (existItem == null)
        //            return View();

        //        existItem.ProductCategoryID = item.ProductCategoryID;
        //        existItem.Position = item.Position;
        //        existItem.Title = item.Title;
        //        existItem.Price = item.Price;
        //        //existItem.Description = item.Description;
        //        existItem.Content = item.Content;

        //        if (item.Avatar != null && item.Avatar.StartsWith("data:image"))
        //        {
        //            existItem.Avatar = SaveImage(item.Avatar);
        //        }
        //    }
        //    else
        //    {
        //        item.CreateTime = DateTime.Now;
        //        item.Status = true;
        //        item.CreateBy = HttpContext.Session.GetString("Username") ?? null;
        //        if (item.Avatar != null && item.Avatar.StartsWith("data:image"))
        //        {
        //            item.Avatar = SaveImage(item.Avatar);
        //        }
        //        db.Entry(item).State = EntityState.Added;
        //    }

        //    db.SaveChanges();
        //    return RedirectToAction(nameof(Index));
        //}
        [HttpGet]
        [Route("/admin/xem-san-pham/{id}", Name = "AdminProductView")]
        public IActionResult View(int id)
        {
            DBContext db = new DBContext();
            var item = db.Products.Where(x => x.ProductID == id).FirstOrDefault();

            return View(item);
        }

        [HttpGet]
        [Route("admin/xoa-san-pham/{id}", Name = "AdminProductDelete")]
        public IActionResult Delete(int id)
        {
            DBContext db = new DBContext();
            var item = db.Products.Find(id);

            if (item != null)
            {
                db.Products.Remove(item);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("admin/the-loai-san-pham/{id?}", Name = "AdminProductCategory")]
        public IActionResult Category(int? id)
        {
            DBContext db = new DBContext();
            var data = db.ProductCategories.ToList();
            return View(data);
        }
        private string SaveImage(string base64)
        {
            base64 = base64.Replace("data:image/jpeg;base64,", string.Empty);
            base64 = base64.Replace("data:image/jpg;base64,", string.Empty);
            base64 = base64.Replace("data:image/gif;base64,", string.Empty);
            base64 = base64.Replace("data:image/png;base64,", string.Empty);

            string rootFolder = this.Environment.WebRootPath;
            string fileName = Guid.NewGuid() + ".jpg";
            byte[] bytes = Convert.FromBase64String(base64);
            string folderSave = $"/FileUploads/Article/Avatar/{fileName}";
            string folderDownload = $"{rootFolder}/{folderSave}".Replace("/", "\\");
            System.IO.File.WriteAllBytes(folderDownload, bytes);
            return folderSave;
        }

        private bool ValidateForm(Product item)
        {
            if (item.ProductCategoryID == null || item.ProductCategoryID <= 0)
            {
                ViewBag.MessageText = "Vui lòng chọn 1 thể loại tin tức";
                ViewBag.MessageClass = "alert-warning";
                return false;
            }
            else if (string.IsNullOrEmpty(item.Title))
            {
                ViewBag.MessageText = "Vui lòng nhập tiêu đề tin tức";
                ViewBag.MessageClass = "alert-warning";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
