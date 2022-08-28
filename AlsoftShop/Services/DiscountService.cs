using AlsoftShop.Models;
using AlsoftShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Services
{
    public class DiscountService : IDiscountService
    {
        public decimal GetDiscount(IEnumerable<ShoppingCartItem> items, IEnumerable<Discount> discounts)
        {

            var discountTotal = 0M;
            foreach (var discount in discounts)
            {
                var shoppingCartItem = items.FirstOrDefault(i => i.Item.Id == discount.DiscountedItemId);

                var shoppingCartDiscountTriggerItem = items.FirstOrDefault(i => i.Item.Id == discount.DiscountTriggerItemId);

                if (shoppingCartItem == null || shoppingCartDiscountTriggerItem == null)
                {
                    continue;
                }

                var discountedProduct = shoppingCartItem.Item;
                var discountedProductQuantity = shoppingCartItem.Quantity;

                var discountTriggerQuantity = shoppingCartDiscountTriggerItem.Quantity;

                var discountedItems = Math.Min(
                    discountedProductQuantity / discount.DiscountTriggerItemCount,
                    discountedProductQuantity);

                discountTotal += discountedItems * discountedProduct.Price * discount.DiscountPercentage;
            }

            return discountTotal;
        }
    }
}
