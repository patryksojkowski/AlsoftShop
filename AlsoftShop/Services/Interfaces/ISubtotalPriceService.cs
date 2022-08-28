using AlsoftShop.Models;
using System.Collections.Generic;

namespace AlsoftShop.Services.Interfaces
{
    public interface ISubtotalPriceService
    {
        decimal GetSubtotal(IEnumerable<CartItem> cartItems);
    }
}
