using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyShop.Data;
using MyShop.Extensions;
using MyShop.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MyShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private CartHelper cartH;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<dynamic> Index()
        {
            if (HttpContext.Session.GetInt32("inc") == null)
            {
                HttpContext.Session.SetInt32("inc", 0);
            }
            if (HttpContext.Session.GetString("cart") == null)
            {
                cartH = new CartHelper();
                Extensions.SessionExtensions.SetObjectAsJson(HttpContext.Session, "cart", cartH.CartProducts);
            }

            List<ProductModel> products = await _context.Products.ToListAsync();
            List<ProductModel> collection = new List<ProductModel>();

            var menItems = products.Where(p => p.ForSex.Equals("V") && p.MostWanted == true).ToList();
            var womenItems = products.Where(p => p.ForSex.Equals("M") && p.MostWanted == true).ToList();

            int i = 0;
            foreach (var item in menItems)
            {
                if (i < 3)
                {
                    collection.Add(item);
                    i++;
                }
                else
                {
                    break;
                }
            }

            int j = 0;
            foreach (var item in womenItems)
            {
                if (j < 3)
                {
                    collection.Add(item);
                    j++;
                }
                else
                {
                    break;
                }
            }

            return View(collection);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Komanda()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
