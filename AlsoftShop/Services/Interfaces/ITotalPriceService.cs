using AlsoftShop.Models;
using System.Collections.Generic;

namespace AlsoftShop.Services.Interfaces
{
    public interface ITotalPriceService
    {
        PriceInfo GetPrice(IEnumerable<CartItem> cartItems);
    }
}
