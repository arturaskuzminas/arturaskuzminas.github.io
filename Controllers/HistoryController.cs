using Microsoft.AspNetCore.Mvc;
using MyShop.Data;
using MyShop.Models;
using MyShop.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<OrderDetail> myOrderDetails = new List<OrderDetail>();
            HistoryViewModel history = new HistoryViewModel();
            var name = HttpContext.User.Identity.Name;
            history.Order = _context.Orders.Where(o => o.UserID.Equals(name)).ToList();
            myOrderDetails = _context.OrderDetails.ToList();

            foreach (var item in history.Order)
            {
                if (item.OrderTotalPrice > 0)
                {
                    var tempOrderID = item.OrderID;
                    foreach (var detail in myOrderDetails)
                    {
                        if (detail.OrderID.Equals(tempOrderID))
                        {
                            history.Details.Add(detail);
                        }
                    }
                }
            }
            return View(history);
        }
    }
}
