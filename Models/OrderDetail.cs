using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
        public decimal ItemTotalPrice { get; set; }
        public string pictureLink { get; set; }
        public decimal ItemPrice { get; set; }
        public int OrderID { get; set; }

        [ForeignKey("OrderID")]
        public Order Order { get; set; }

        public OrderDetail()
        {

        }

        public OrderDetail(string itemName, int itemCount, decimal itemTotalPrice, string pictureLink, decimal itemPrice, int orderid)
        {
            ItemName = itemName;
            ItemCount = itemCount;
            ItemTotalPrice = itemTotalPrice;
            this.pictureLink = pictureLink;
            ItemPrice = itemPrice;
            OrderID = orderid;
        }
    }
}
