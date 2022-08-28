using AlsoftShop.Repository.Interfaces;
using AlsoftShop.Services.Interfaces;
using AlsoftShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
            var products = repository.GetProducts();
            var cartItems = repository.GetCartItems();

            var priceInfo = totalPriceService.GetPrice(cartItems);

            var vm = new ShopViewModel
            {
                Products = products,
                CartItems = cartItems,
                Subtotal = priceInfo.Subtotal,
                Discount = priceInfo.Discount,
                Total = priceInfo.Total,
            };

            return View("Index", vm);
        }

        [Route("Add/{id}")]
        public IActionResult Add(int id)
        {
            repository.AddProductToCart(id);

            return Index();
        }

        [Route("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            repository.RemoveProductFromCart(id);

            return Index();
        }
    }
}
