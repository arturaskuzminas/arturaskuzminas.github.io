using MyShop.Models;
using System.Collections.Generic;

namespace MyShop.ViewModels
{
    public class HistoryViewModel
    {
        public List<Order> Order { get; set; }
        public List<OrderDetail> Details { get; set; }

        public HistoryViewModel()
        {
            Order = new List<Order>();
            Details = new List<OrderDetail>();
        }

        public HistoryViewModel(List<Order> order, List<OrderDetail> details)
        {
            Order = order;
            Details = details;
        }
    }
}
