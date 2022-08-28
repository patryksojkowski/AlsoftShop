﻿using AlsoftShop.Models;
using AlsoftShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlsoftShop.Services
{
    public class DiscountService : IDiscountService
    {
        public decimal GetDiscount(IEnumerable<CartItem> cartItems, IEnumerable<Discount> discounts)
        {
            if (cartItems is null)
            {
                throw new ArgumentNullException(nameof(cartItems));
            }

            if (discounts is null)
            {
                throw new ArgumentNullException(nameof(discounts));
            }

            return GetDiscountInternal(cartItems, discounts);
        }

        private decimal GetDiscountInternal(IEnumerable<CartItem> cartItems, IEnumerable<Discount> discounts)
        {
            var discountTotal = 0M;
            foreach (var discount in discounts)
            {
                var cartItem = cartItems.FirstOrDefault(i => i.Product.Id == discount.DiscountedProductId);

                var discountTriggerItem = cartItems.FirstOrDefault(i => i.Product.Id == discount.DiscountTriggerProductId);

                if (cartItem == null || discountTriggerItem == null)
                {
                    continue;
                }

                var discountedProduct = cartItem.Product;
                var discountedProductQuantity = cartItem.Quantity;
                var discountTriggerItemQuantity = discountTriggerItem.Quantity;

                // todo verify it actually works via tests :)
                var discountedItems = Math.Min(
                    discountTriggerItemQuantity / discount.DiscountTriggerProductCount,
                    discountedProductQuantity);

                // todo verify it actually works via tests :)
                discountTotal += discountedItems * discountedProduct.Price * discount.DiscountPercentage;
            }

            return discountTotal;
        }
    }
}
