using AlsoftShop.Models;
using AlsoftShop.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace AlsoftShop.Services
{
    public class SubtotalPriceService : ISubtotalPriceService
    {
        public decimal GetSubtotal(IEnumerable<CartItem> cartItems)
        {
            if (cartItems == null)
            {
                throw new ArgumentNullException(nameof(cartItems));
            }

            return GetSubtotalInternal(cartItems);
        }

        private decimal GetSubtotalInternal(IEnumerable<CartItem> cartItems)
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
