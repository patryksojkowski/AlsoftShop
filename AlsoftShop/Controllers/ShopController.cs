using AlsoftShop.Repository.Interfaces;
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

        public ShopController(IRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index()
        {
            var items = repository.GetItems();
            var vm = new ShopViewModel
            {
                Items = items,
            };

            return View(vm);
        }
    }
}
