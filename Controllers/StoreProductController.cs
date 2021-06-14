using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyShop.Data;
using MyShop.Extensions;
using MyShop.Models;
using MyShop.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    public class StoreProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly CartHelper helper;

        public StoreProductController(ApplicationDbContext context)
        {
            _context = context;
            helper = new CartHelper();
        }

        [AllowAnonymous]
        // GET: StoreProduct
        public async Task<IActionResult> Product(int? id)
        {
            ViewBag.id = id;
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.Products.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            return View(productModel);
        }

        [AllowAnonymous]
        // GET: StoreProducts
        [HttpGet]
        public async Task<IActionResult> ProductsAsync(string id)
        {
            IEnumerable<ProductModel> results = await _context.Products.ToListAsync();

            if (string.IsNullOrEmpty(id) || id.Length < 2)
            {
                ViewBag.searchStr = "incorrect";
                return View();
            }
            else
            {
                results = results.Where(s => s.Title.ToLowerInvariant().StartsWith(id.ToLowerInvariant()));
                if (results.Count() == 0)
                {
                    ViewBag.searchStr = "incorrect";
                    return View();
                }
                else
                {
                    return View(results);
                }
            }
        }

        [AllowAnonymous]
        // GET: CategoryProducts
        [HttpGet]
        public IActionResult CategoryProducts(string id, int? sort)
        {
            IEnumerable<ProductModel> products = _context.Products.ToList();
            int categoryID = _context.Categories.Where(n => n.ID == int.Parse(id)).First().ID;

            if (sort == null)
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.searchStr = "incorrect";
                    return View();
                }
                else
                {
                    products = products.Where(pr => pr.CategoryID == categoryID);
                    ViewBag.CategoryName = _context.Categories.Where(n => n.ID == int.Parse(id)).First().Title;
                    ViewBag.SortFieldText = "Rikiuoti pagal";
                    return View(products);
                }
            }
            if (sort == 1)
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.searchStr = "incorrect";
                    return View();
                }
                else
                {
                    products = products.Where(pr => pr.CategoryID == categoryID);
                    ViewBag.CategoryName = _context.Categories.Where(n => n.ID == int.Parse(id)).First().Title;
                    ViewBag.SortFieldText = "Pavadinimą (A-Z)";
                    return View(products.OrderBy(p => p.Title));
                }
            }

            if (sort == 2)
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.searchStr = "incorrect";
                    return View();
                }
                else
                {
                    products = products.Where(pr => pr.CategoryID == categoryID);
                    ViewBag.CategoryName = _context.Categories.Where(n => n.ID == int.Parse(id)).First().Title;
                    ViewBag.SortFieldText = "Pavadinimą (Z-A)";
                    return View(products.OrderByDescending(p => p.Title));
                }
            }

            if (sort == 3)
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.searchStr = "incorrect";
                    return View();
                }
                else
                {
                    products = products.Where(pr => pr.CategoryID == categoryID);
                    ViewBag.CategoryName = _context.Categories.Where(n => n.ID == int.Parse(id)).First().Title;
                    ViewBag.SortFieldText = "Kainą (brangiausi viršuje)";
                    return View(products.OrderByDescending(p => p.Price));
                }
            }

            if (sort == 4)
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.searchStr = "incorrect";
                    return View();
                }
                else
                {
                    products = products.Where(pr => pr.CategoryID == categoryID);
                    ViewBag.CategoryName = _context.Categories.Where(n => n.ID == int.Parse(id)).First().Title;
                    ViewBag.SortFieldText = "Kainą (pigiausi viršuje)";
                    return View(products.OrderBy(p => p.Price));
                }
            }
            return View();
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult AddToCart(int id)
        {
            List<ProductModel> list = Extensions.SessionExtensions.GetObjectFromJson<List<ProductModel>>(HttpContext.Session, "cart");
            List<CartViewModel> groups = helper.GroupedProducts(list);
            var product = _context.Products.Where(p => p.ID.Equals(id)).First();
            bool eql = false;
            if (product.StockCount != 0)
            {
                foreach (var item in groups)
                {
                    if (item.ItemName.Equals(product.Title))
                    {
                        if (item.ItemCount == product.StockCount)
                        {
                            eql = true;
                            break;
                        }
                        break;
                    }
                }
                if (!eql)
                {
                    list.Add(product);
                    list.Sort(helper.GetComparerByTitle());
                    Extensions.SessionExtensions.SetObjectAsJson(HttpContext.Session, "cart", list);
                    HttpContext.Session.SetInt32("inc", list.Count());
                }
            }
            return RedirectToAction("Product", "StoreProduct", new { id });
        }
    }
}