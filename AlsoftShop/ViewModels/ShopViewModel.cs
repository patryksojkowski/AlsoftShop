using AlsoftShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.ViewModels
{
    public class ShopViewModel
    {
        public IEnumerable<Item> Items { get; set; } = new List<Item>();
    }
}
