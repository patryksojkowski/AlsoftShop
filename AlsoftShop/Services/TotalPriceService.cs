using AlsoftShop.Models;
using AlsoftShop.Repository.Interfaces;
using AlsoftShop.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace AlsoftShop.Services
{
    public class TotalPriceService : ITotalPriceService
    {
        private readonly IRepository repository;
        private readonly ISubtotalPriceService subtotalPriceService;
        private readonly IDiscountService discountService;

        public TotalPriceService(IRepository repository,
            ISubtotalPriceService subtotalPriceService,
            IDiscountService discountService)
        {
            this.repository = repository;
            this.subtotalPriceService = subtotalPriceService;
            this.discountService = discountService;
        }
        public PriceInfo GetPrice(IEnumerable<CartItem> cartItems)
        {
            if(cartItems is null)
            {
                throw new ArgumentNullException(nameof(cartItems));
            }

            return GetPriceInternal(cartItems);
        }

        private PriceInfo GetPriceInternal(IEnumerable<CartItem> cartItems)
        {
            var subtotal = subtotalPriceService.GetSubtotal(cartItems);

            var discounts = repository.GetDiscounts();

            var totalDiscount = discountService.GetDiscount(cartItems, discounts);

            var total = subtotal - totalDiscount;

            return new PriceInfo
            {
                Subtotal = subtotal,
                Discount = totalDiscount,
                Total = total,
            };
        }
    }
}
