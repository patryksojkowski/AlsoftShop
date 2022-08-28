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

        public ShopController(IRepository repository, IPriceService priceService)
        {
            this.repository = repository;
            this.priceService = priceService;
        }

        public IActionResult Index()
        {
            var items = repository.GetItems();
            var currentItems = repository.GetCurrentItems();

            var totalPrice = priceService.GetTotalPrice(currentItems);

            var vm = new ShopViewModel
            {
                Items = items,
                CurrentItems = currentItems,
                TotalPrice = totalPrice
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
