using AlsoftShop.Models;
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
            var alreadyDiscounted = new Dictionary<int, int>();
            var alreadyTriggered = new Dictionary<int, int>();

            var discountTotal = 0M;
            foreach (var discount in discounts)
            {
                var cartItem = cartItems.FirstOrDefault(i => i.Product.Id == discount.DiscountedProductId);

                var discountTriggerItem = cartItems.FirstOrDefault(i => i.Product.Id == discount.DiscountTriggerProductId);

                // if cart does not contain at least one of discount product and trigger product, then skip
                if (cartItem == null || discountTriggerItem == null)
                {
                    continue;
                }

                // detetmine how many products were already discounted by previous discounts
                var discountedProduct = cartItem.Product;
                var disctountedProductId = discountedProduct.Id;
                var productsToBeDiscountedQuantity = cartItem.Quantity;
                if(alreadyDiscounted.ContainsKey(disctountedProductId))
                {
                    productsToBeDiscountedQuantity -= alreadyDiscounted[disctountedProductId];
                }

                // detetmine how many triggers were already used by previous discounts
                var productsToBeTriggers = discountTriggerItem.Quantity;
                var discountTriggerItemId = discountTriggerItem.Product.Id;
                if (alreadyTriggered.ContainsKey(discountTriggerItemId))
                {
                    productsToBeTriggers -= alreadyTriggered[discountTriggerItemId];
                }

                // calculate how many items can we discount
                var discountedItems = Math.Min(
                    productsToBeTriggers / discount.DiscountTriggerProductCount,
                    productsToBeDiscountedQuantity);

                // store info about discounted products
                alreadyDiscounted[disctountedProductId] = discountedItems;
                // store info about triggered products
                alreadyTriggered[discountTriggerItemId] = discountedItems * discount.DiscountTriggerProductCount;

                // increase total discount
                discountTotal += discountedItems * discountedProduct.Price * discount.DiscountPercentage;
            }

            return discountTotal;
        }
    }
}
