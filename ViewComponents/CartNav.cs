using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyShop.Data;
using MyShop.Extensions;
using MyShop.Models;
using MyShop.ViewModels;
using System.Collections.Generic;

namespace MyShop.ViewComponents
{
    public class CartNav : ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private CartHelper helper;

        public CartNav(ApplicationDbContext context)
        {
            _context = context;
            helper = new CartHelper();
        }

        public IViewComponentResult Invoke()
        {
            List<ProductModel> collection = Extensions.SessionExtensions.GetObjectFromJson<List<ProductModel>>(HttpContext.Session, "cart");
            ViewBag.Total = helper.GetProductsTotalPrice(collection);
            List<CartViewModel> products = helper.GroupedProducts(collection);

            return View(products);
        }
    }
}
