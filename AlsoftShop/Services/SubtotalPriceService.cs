using AlsoftShop.Models;
using AlsoftShop.Services.Interfaces;
using System.Collections.Generic;

namespace AlsoftShop.Services
{
    public class SubtotalPriceService : ISubtotalPriceService
    {
        public decimal GetSubtotal(IEnumerable<CartItem> cartItems)
        {
            var total = 0M;
            foreach (var cartItem in cartItems)
            {
                total += cartItem.Quantity * cartItem.Product.Price;
            }

            return total;
        }
    }
}
