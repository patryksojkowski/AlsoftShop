using AlsoftShop.Models;
using AlsoftShop.Repository.Interfaces;
using AlsoftShop.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public PriceInfo GetPrice(IEnumerable<ShoppingCartItem> shoppingCartItems)
        {
            var subtotal = subtotalPriceService.GetPrice(shoppingCartItems);

            var discounts = repository.GetDiscounts();

            var totalDiscount = discountService.GetDiscount(shoppingCartItems, discounts);

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
