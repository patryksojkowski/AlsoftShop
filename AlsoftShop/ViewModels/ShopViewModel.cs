using AlsoftShop.Models;
using System.Collections.Generic;

namespace AlsoftShop.ViewModels
{
    public class ShopViewModel
    {
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
