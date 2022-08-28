using AlsoftShop.Models;
using System.Collections.Generic;

namespace AlsoftShop.Services.Interfaces
{
    public interface IDiscountService
    {
        decimal GetDiscount(IEnumerable<CartItem> cartItems, IEnumerable<Discount> discounts);
    }
}
