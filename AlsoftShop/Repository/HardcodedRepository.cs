using AlsoftShop.Models;
using AlsoftShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Repository
{
    public class HardcodedRepository : IRepository
    {
        private IEnumerable<Product> products;

        private IList<CartItem> cartItems;

        private IEnumerable<Discount> discounts;

        public HardcodedRepository()
        {
            products = InitializeProducts();
            cartItems = InitializeCart();
            discounts = InitializeDiscounts();
        }

        public void AddProductToCart(int productId)
        {
            var cartItem = cartItems.FirstOrDefault(i => i.Product.Id == productId);
            if(cartItem is null)
            {
                var item = products.FirstOrDefault(i => i.Id == productId)
                    ?? throw new ArgumentOutOfRangeException(nameof(productId));

                cartItem = new CartItem
                {
                    Product = item
                };

                cartItems.Add(cartItem);
            }

            cartItem.Quantity++;
        }

        public void RemoveProductFromCart(int productId)
        {
            var cartItem = cartItems.FirstOrDefault(i => i.Product.Id == productId)
                ?? throw new ArgumentOutOfRangeException(nameof(productId));

            if(cartItem.Quantity == 1)
            {
                cartItems.Remove(cartItem);
            }

            cartItem.Quantity--;
        }

        public IEnumerable<Product> GetProducts()
        {
            return products;
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            return cartItems;
        }

        public IEnumerable<Discount> GetDiscounts()
        {
            return discounts;
        }

        private IEnumerable<Product> InitializeProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Apple",
                    Price = 1M,
                    Unit = "Bag",
                    AdditionalProperties = new Dictionary<string, string>
                    {
                        ["Origin"] = "Poland"
                    }
                },
                new Product
                {
                    Id = 2,
                    Name = "Soup",
                    Price = 0.65M,
                    Unit = "Tin",
                    AdditionalProperties = new Dictionary<string, string>
                    {
                        ["Flavour"] = "Chicken"
                    }
                },
            };
        }

        private IList<CartItem> InitializeCart()
        {
            return new List<CartItem>
            {
                new CartItem
                {
                    Product = new Product
                    {
                        Id = 2,
                        Name = "Soup",
                        Price = 0.65M,
                        Unit = "Tin",
                        AdditionalProperties = new Dictionary<string, string>
                        {
                            ["Flavour"] = "Chicken"
                        }
                    },
                    Quantity = 2
                }

            };
        }

        private IEnumerable<Discount> InitializeDiscounts()
        {
            return new List<Discount>
            {
                new Discount
                {
                    DiscountedProductId = 1,
                    DiscountTriggerProductId = 1,
                    DiscountTriggerProductCount = 1,
                    DiscountPercentage = 0.1M,
                }
            };
        }
    }
}
