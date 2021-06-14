using MyShop.Models;
using MyShop.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MyShop.Extensions
{
    public class CartHelper
    {
        public List<ProductModel> CartProducts { get; set; }
        public CartHelper()
        {
            CartProducts = new List<ProductModel>();
        }

        public decimal GetProductsTotalPrice(List<ProductModel> products)
        {
            decimal price = 0;
            if (products != null)
            {
                foreach (var item in products)
                {
                    price += item.Price;
                }
            }
            return price;
        }

        public Comparer<ProductModel> GetComparerByTitle()
        {
            var byTitle = Comparer<ProductModel>.Create((a, b) => a.Title.CompareTo(b.Title));
            return byTitle;
        }

        public List<CartViewModel> GroupedProducts(List<ProductModel> list)
        {
            if (list != null)
            {
                var query = list.GroupBy(product => product.Title).Select(p => new
                {
                    ProductName = p.Key,
                    ProductCount = p.Count(),
                    ProductTotalPrice = p.Select(p => p.Price).Sum(),
                    ProductPicture = p.Select(p => p.PictureLink).First(),
                    ProductPrice = p.Select(p => p.Price).First()
                }).OrderBy(p => p.ProductName);

                List<CartViewModel> cartProducts = new List<CartViewModel>();
                foreach (var item in query)
                {
                    CartViewModel product = new CartViewModel(item.ProductName, item.ProductCount,
                                                              item.ProductTotalPrice, item.ProductPicture, item.ProductPrice);
                    cartProducts.Add(product);
                }
                return cartProducts;
            }
            else
            {
                var empty = new List<CartViewModel>();
                return empty;
            }
        }
    }
}
