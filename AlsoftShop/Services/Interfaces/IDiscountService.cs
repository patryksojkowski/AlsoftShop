using AlsoftShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Services.Interfaces
{
    public interface IDiscountService
    {
        decimal GetDiscount(IEnumerable<ShoppingCartItem> items, IEnumerable<Discount> discounts);
    }
}
