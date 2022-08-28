using AlsoftShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.ViewModels
{
    public class ShopViewModel
    {
        public decimal TotalPrice { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal FinalPrice { get; set; }
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
        public IEnumerable<ShoppingCartItem> CurrentItems { get; set; } = new List<ShoppingCartItem>();
    }
}
