using AlsoftShop.Models;
using AlsoftShop.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlsoftShop.Tests.Services
{
    public class SubtotalPriceServiceTests
    {
        private SubtotalPriceService sut = new SubtotalPriceService();

        [Test]
        public void GetSubtotal_WhenNullPassed_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetSubtotal(null));
        }

        [Test]
        public void GetSubtotal_WhenEmptyCartPassed_ReturnsZero()
        {
            // Arrange
            var cartItems = new List<CartItem>();

            // Act
            var result = sut.GetSubtotal(cartItems);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void GetSubtotal_WhenCartItemsPassed_ReturnsPrice()
        {
            // Arrange
            var cartItems = new List<CartItem>
            {
                new CartItem
                {
                    Quantity = 10,
                    Product = new Product
                    {
                        Price = 10,
                    }
                },
                new CartItem
                {
                    Quantity = 1,
                    Product = new Product
                    {
                        Price = 1,
                    }
                }
            };

            // Act
            var result = sut.GetSubtotal(cartItems);

            // Assert
            Assert.AreEqual(101M, result);
        }

    }
}
