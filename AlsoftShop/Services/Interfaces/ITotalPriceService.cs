using AlsoftShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Services.Interfaces
{
    public interface ITotalPriceService
    {
        PriceInfo GetPrice(IEnumerable<ShoppingCartItem> shoppingCartItems);
    }
}
