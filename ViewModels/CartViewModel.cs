namespace MyShop.ViewModels
{
    public class CartViewModel
    {
        public string ItemName { get; set; }
        public int ItemCount { get; set; }
        public decimal ItemTotalPrice { get; set; }
        public string pictureLink { get; set; }
        public decimal ItemPrice { get; set; }
        public CartViewModel(string itemName, int itemCount, decimal itemTotalPrice, string pictureLink, decimal itemPrice)
        {
            ItemName = itemName;
            ItemCount = itemCount;
            ItemTotalPrice = itemTotalPrice;
            this.pictureLink = pictureLink;
            ItemPrice = itemPrice;
        }
    }
}
