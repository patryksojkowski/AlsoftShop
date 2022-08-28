using AlsoftShop.Models;
using System.Collections.Generic;

namespace AlsoftShop.ViewModels
{
    public class ShopViewModel
    {
        public PriceInfo PriceInfo { get; set; }
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public IEnumerable<CartItem> CartItems { get; set; } = new List<CartItem>();
    }
}
