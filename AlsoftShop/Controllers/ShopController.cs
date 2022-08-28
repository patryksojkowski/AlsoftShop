using AlsoftShop.Repository.Interfaces;
using AlsoftShop.Services.Interfaces;
using AlsoftShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly IRepository repository;
        private readonly IPriceService priceService;
        private readonly IDiscountService discountService;

        public ShopController(
            IRepository repository,
            IPriceService priceService,
            IDiscountService discountService)
        {
            this.repository = repository;
            this.priceService = priceService;
            this.discountService = discountService;
        }

        public IActionResult Index()
        {
            var items = repository.GetItems();
            var currentItems = repository.GetCurrentItems();


            // TODO call to PriceService and DiscountService should be performed by TotalPriceService (name in progress)
            var totalPrice = priceService.GetTotalPrice(currentItems);

            var discounts = repository.GetDiscounts();

            var totalDiscount = discountService.GetDiscount(currentItems, discounts);

            var finalPrice = totalPrice - totalDiscount;

            var vm = new ShopViewModel
            {
                Items = items,
                CurrentItems = currentItems,
                TotalPrice = totalPrice,
                TotalDiscount = totalDiscount,
                FinalPrice = finalPrice,
            };

            return View("Index", vm);
        }

        [Route("Add/{id}")]
        public IActionResult Add(int id)
        {
            repository.AddItem(id);

            return Index();
        }

        [Route("Remove/{id}")]
        public IActionResult Remove(Guid id)
        {
            repository.RemoveItem(id);

            return Index();
        }
    }
}
