using AlsoftShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Services.Interfaces
{
    public interface IPriceService
    {
        decimal GetTotalPrice(IEnumerable<ShoppingCartItem> items);
    }
}
