using AlsoftShop.Repository;
using AlsoftShop.Repository.Interfaces;
using AlsoftShop.Services.Interfaces;
using AlsoftShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AlsoftShop.Controllers
{
    public class ShopController : Controller
    {
        private readonly IRepository repository;
        private readonly ITotalPriceService totalPriceService;
        private readonly DatabaseRepository databaseRepository;

        public ShopController(
            IRepository repository,
            ITotalPriceService totalPriceService,
            DatabaseRepository databaseRepository)
        {
            this.repository = repository;
            this.totalPriceService = totalPriceService;
            this.databaseRepository = databaseRepository;
        }

        public IActionResult Index()
        {
            try
            {
                var products = databaseRepository.GetProducts();
                var cartItems = repository.GetCartItems();

                var priceInfo = totalPriceService.GetPrice(cartItems);

                var vm = new ShopViewModel
                {
                    Products = products,
                    CartItems = cartItems,
                    PriceInfo = priceInfo
                };

                return View("Index", vm);
            }
            catch (Exception ex)
            {
                // todo it probably should redirect user to error page
                return BadRequest(new { ex, msg = "I am sorry, sth went wrong" });
            }
        }

        [Route("Add/{id}")]
        public IActionResult Add(int id)
        {
            try
            {
                repository.AddProductToCart(id);

                return Index();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex, msg = "I am sorry, sth went wrong" });
            }
        }

        [Route("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            try
            {
                repository.RemoveProductFromCart(id);

                return Index();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex, msg = "I am sorry, sth went wrong" });
            }
        }
    }
}
