using AlsoftShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.ViewModels
{
    public class ShopViewModel
    {
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Total { get; set; }
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
        public IEnumerable<ShoppingCartItem> CurrentItems { get; set; } = new List<ShoppingCartItem>();
    }
}
