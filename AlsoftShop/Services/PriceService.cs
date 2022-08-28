using AlsoftShop.Models;
using AlsoftShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Services
{
    public class PriceService : IPriceService
    {
        public decimal GetTotalPrice(IEnumerable<ShoppingCartItem> items)
        {
            var total = 0M;
            foreach (var item in items)
            {
                total += item.Quantity * item.Item.Price;
            }

            return total;
        }
    }
}
