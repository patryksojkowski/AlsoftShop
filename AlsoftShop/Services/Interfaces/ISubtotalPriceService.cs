using AlsoftShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Services.Interfaces
{
    public interface ISubtotalPriceService
    {
        decimal GetPrice(IEnumerable<ShoppingCartItem> items);
    }
}
