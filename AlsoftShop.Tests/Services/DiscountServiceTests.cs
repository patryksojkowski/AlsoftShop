using System;
using System.Collections.Generic;
using System.Linq;
using AlsoftShop.Models;
using AlsoftShop.Services;
using Moq;
using NUnit.Framework;

namespace AlsoftShop.Tests.Services
{
    [TestFixture]
    public class DiscountServiceTests
    {
        private DiscountService sut = new DiscountService();

        [Test]
        public void GetDiscount_WhenNullProvided_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetDiscount(null, It.IsAny<IEnumerable<Discount>>()));
            Assert.Throws<ArgumentNullException>(() => sut.GetDiscount(It.IsAny<IEnumerable<CartItem>>(), null));
        }

        [Test]
        public void GetDiscount_WhenNoCartItemsProvided_AndNoDiscountAvailable_ReturnsZero()
        {
            // Arrange
            var cartItems = new List<CartItem>();
            var discounts = new List<Discount>();

            // Act
            var result = sut.GetDiscount(cartItems, discounts);

            // Assert
            Assert.AreEqual(0m, result);
        }

        [Test]
        public void GetDiscount_WhenCartItemsProvided_ButNoDiscountAvailable_ReturnsZero()
        {
            // Arrange
            var cartItems = GetCartItems(
                new (int, decimal, int) [] {
                    (1, 1, 1),
                    (2, 1, 1)
                });
            var discounts = new List<Discount>();

            // Act
            var result = sut.GetDiscount(cartItems, discounts);

            // Assert
            Assert.AreEqual(0m, result);
        }

        [Test]
        public void GetDiscount_WhenCartItemsProvided_AndMultipleDiscountsOnSameProduct_ShouldReturnCorrectValue()
        {
            // Arrange
            var cartItems = GetCartItems(
                new (int, decimal, int)[] {
                    (1, 1, 1),
                    (2, 1, 1),
                    (3, 1, 1)
                });
            var discounts = new List<Discount>
            {
                new Discount
                {
                    DiscountedProductId = 1,
                    DiscountTriggerProductId = 2,
                    DiscountTriggerProductCount = 1,
                    DiscountPercentage = 0.5M,
                },
                new Discount
                {
                    DiscountedProductId = 1,
                    DiscountTriggerProductId = 3,
                    DiscountTriggerProductCount = 1,
                    DiscountPercentage = 0.5M,
                },
            };

            // Act
            var result = sut.GetDiscount(cartItems, discounts);

            // Assert
            Assert.AreEqual(0.5M, result);
        }
        
        [Test]
        public void GetDiscount_WhenCartItemsProvided_AndTriggerUsedMoreThanOnce_ShouldReturnCorrectValue()
        {
            // Arrange
            var cartItems = GetCartItems(
                new (int, decimal, int)[] {
                    (1, 1, 1),
                    (2, 1, 1),
                    (3, 1, 1)
                });
            var discounts = new List<Discount>
            {
                new Discount
                {
                    DiscountedProductId = 2,
                    DiscountTriggerProductId = 1,
                    DiscountTriggerProductCount = 1,
                    DiscountPercentage = 0.5M,
                },
                new Discount
                {
                    DiscountedProductId = 3,
                    DiscountTriggerProductId = 1,
                    DiscountTriggerProductCount = 1,
                    DiscountPercentage = 0.5M,
                },
            };

            // Act
            var result = sut.GetDiscount(cartItems, discounts);

            // Assert
            Assert.AreEqual(0.5M, result);
        }
        
        [Test]
        public void GetDiscount_WhenCartItemsProvided_AndMultipleDiscountsOnSameProduct_AndTriggerUsedMoreThanOnce_ShouldReturnCorrectValue()
        {
            // Arrange
            var cartItems = GetCartItems(
                new (int, decimal, int)[] {
                    (1, 1, 10),
                    (2, 1, 2),
                    (3, 1, 3)
                });
            var discounts = new List<Discount>
            {
                new Discount
                {
                    DiscountedProductId = 1,
                    DiscountTriggerProductId = 2,
                    DiscountTriggerProductCount = 1,
                    DiscountPercentage = 0.5M,
                },
                new Discount
                {
                    DiscountedProductId = 1,
                    DiscountTriggerProductId = 3,
                    DiscountTriggerProductCount = 1,
                    DiscountPercentage = 0.5M,
                },
                new Discount
                {
                    DiscountedProductId = 2,
                    DiscountTriggerProductId = 1,
                    DiscountTriggerProductCount = 1,
                    DiscountPercentage = 0.5M,
                },
                new Discount
                {
                    DiscountedProductId = 3,
                    DiscountTriggerProductId = 1,
                    DiscountTriggerProductCount = 1,
                    DiscountPercentage = 0.5M,
                },
            };

            // Act
            var result = sut.GetDiscount(cartItems, discounts);

            // Assert
            Assert.AreEqual(5M, result);
        }

        [Test]
        [TestCaseSource(nameof(GetTestCases))]
        public void GetDiscount_CalculatesDiscountCorrectlyForTestCases(DiscountServiceTestCase tc)
        {
            // Arrange
            var cartItems = GetCartItems(new (int, decimal, int)[] {
                (tc.CartProductId1, tc.CartProductPrice1, tc.CartProductQuantity1),
                (tc.CartProductId2, tc.CartProductPrice2, tc.CartProductQuantity2)
            });
            var discounts = GetDiscounts(tc.DiscountProductId, tc.DiscountTriggerId, tc.DiscountTriggerCount, tc.DiscountPercentage);

            // Act
            var result = sut.GetDiscount(cartItems, discounts);

            // Assert
            Assert.AreEqual(tc.ExpectedDiscount, result, message: tc.Message);
        }

        public static IEnumerable<DiscountServiceTestCase> GetTestCases()
        {
            return new[]
            {
                new DiscountServiceTestCase (1, 10, 10m, 2, 10, 10m, 3, 4, 1, 0.5M, 0M, "Items do not match discounts"),
                new DiscountServiceTestCase (1, 10, 10m, 2, 10, 10m, 1, 3, 1, 0.5M, 0M, "Items do not match discounts"),
                new DiscountServiceTestCase (1, 10, 10m, 2, 10, 10m, 3, 1, 1, 0.5M, 0M, "Items do not match discounts"),

                new DiscountServiceTestCase (1, 10, 10m, 2, 10, 10m, 1, 2, 1, 0.5M, 50M, "Items match discounts"),
                new DiscountServiceTestCase (1, 10, 10m, 2, 20, 10m, 1, 2, 2, 0.5M, 50M, "Items match discounts"),

                new DiscountServiceTestCase (1, 5, 10m, 2, 20, 10m, 1, 2, 2, 0.5M, 25M, "More triggers than products"),
                new DiscountServiceTestCase (1, 10, 10m, 2, 10, 10m, 1, 2, 2, 0.5M, 25M, "More products than triggers"),
                new DiscountServiceTestCase (1, 20, 10m, 2, 20, 10m, 1, 2, 2, 0.5M, 50M, "More products than triggers"),

                new DiscountServiceTestCase (1, 2, 10m, 2, 2, 10m, 1, 2, 3, 0.5M, 0M, "Not enough triggers"),
            };
        }

        public class DiscountServiceTestCase
        {
            public DiscountServiceTestCase(
                int cartProductId1,
                int cartProductQuantity1,
                decimal cartProductPrice1,
                int cartProductId2,
                int cartProductQuantity2,
                decimal cartProductPrice2,
                int discountProductId,
                int discountTriggerId,
                int discountTriggerCount,
                decimal discountPercantage,
                decimal expectedDiscount,
                string message = null)
            {
                CartProductId1 = cartProductId1;
                CartProductQuantity1 = cartProductQuantity1;
                CartProductPrice1 = cartProductPrice1;
                CartProductId2 = cartProductId2;
                CartProductQuantity2 = cartProductQuantity2;
                CartProductPrice2 = cartProductPrice2;
                DiscountProductId = discountProductId;
                DiscountTriggerId = discountTriggerId;
                DiscountTriggerCount = discountTriggerCount;
                DiscountPercentage = discountPercantage;
                ExpectedDiscount = expectedDiscount;
                Message = message;
            }
            public int CartProductId1 { get; }
            public int CartProductQuantity1 { get; }
            public decimal CartProductPrice1 { get; }
            public int CartProductId2 { get; }
            public int CartProductQuantity2 { get; }
            public decimal CartProductPrice2 { get; }
            public int DiscountProductId { get; }
            public int DiscountTriggerId { get; }
            public int DiscountTriggerCount { get; }
            public decimal DiscountPercentage { get; }
            public decimal ExpectedDiscount { get; }
            public string Message { get; }
        }

        private IEnumerable<CartItem> GetCartItems(IEnumerable<(int id, decimal price, int quantity)> products)
        {
            return products.Select(p =>
                new CartItem
                {
                    Quantity = p.quantity,
                    Product = new Product
                    {
                        Id = p.id,
                        Price = p.price
                    }
                }
            );
        }

        private IEnumerable<Discount> GetDiscounts(
            int discountProductId,
            int discountTriggerId,
            int discountTriggerCount,
            decimal discountPercentage)
        {
            return new[]
            {
                new Discount
                {
                    DiscountedProductId = discountProductId,
                    DiscountTriggerProductId = discountTriggerId,
                    DiscountTriggerProductCount = discountTriggerCount,
                    DiscountPercentage = discountPercentage
                }
            };
        }
    }
}
