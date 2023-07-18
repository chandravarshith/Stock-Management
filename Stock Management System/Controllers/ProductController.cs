using Microsoft.AspNetCore.Mvc;
using Stock_Management_System.Data;
using Stock_Management_System.Models;

namespace Stock_Management_System.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext db;

        public ProductController(ApplicationDbContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = db.Products;
            return View(productList);
        }

        //GET
        public IActionResult AddProduct()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(Product p)
        {
            if(p.AvailableQuantity < 0 || p.AvailableQuantity > 100000)
            {
                ModelState.AddModelError("AvailableQuantity", "The Quantity cannot be negative or more than 100000.");
            }
            if (ModelState.IsValid)
            {
                if (p.AvailableQuantity > 0)
                {
                    p.AvailabilityStatus = "Available";
                }
                else p.AvailabilityStatus = "Out of Stock";
                p.DispatchedQuantity = 0;
                p.DeliveredQuantity = 0;
                db.Products.Add(p);
                db.SaveChanges();
                TempData["success"] = "Product Added!";
                return RedirectToAction("Index");
            }
            return View();
        }

        //GET
        public IActionResult EditProduct(int? id)
        {
            if(id == 0 || id == null)
            {
                return NotFound();
            }
            var product = db.Products.Find(id);
            if(product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product p)
        {
            if (p.AvailableQuantity < 0 || p.AvailableQuantity > 100000)
            {
                ModelState.AddModelError("AvailableQuantity", "The Quantity cannot be negative or more than 100000.");
            }
            if (ModelState.IsValid)
            {
                if (p.AvailableQuantity > 0)
                {
                    p.AvailabilityStatus = "Available";
                }
                else p.AvailabilityStatus = "Out of Stock";

                db.Products.Update(p);
                db.SaveChanges();
                TempData["success"] = "Product Updated!";
                return RedirectToAction("Index");
            }
            return View(p);
        }

        //GET
        public IActionResult DeleteProduct(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            var product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        //POST
        [HttpPost, ActionName("DeleteProduct")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProductPost(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var p = db.Products.Find(id);
            if (p == null)
            {
                return NotFound();
            }
            db.Products.Remove(p);
            db.SaveChanges();
            TempData["success"] = "Product Deleted!";
            return RedirectToAction("Index");
        }

    }
}
