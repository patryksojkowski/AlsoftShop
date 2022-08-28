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
        private readonly ITotalPriceService totalPriceService;

        public ShopController(
            IRepository repository,
            ITotalPriceService totalPriceService)
        {
            this.repository = repository;
            this.totalPriceService = totalPriceService;
        }

        public IActionResult Index()
        {
            var items = repository.GetItems();
            var currentItems = repository.GetCurrentItems();

            var priceInfo = totalPriceService.GetPrice(currentItems);

            var vm = new ShopViewModel
            {
                Items = items,
                CurrentItems = currentItems,
                Subtotal = priceInfo.Subtotal,
                Discount = priceInfo.Discount,
                Total = priceInfo.Total,
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
