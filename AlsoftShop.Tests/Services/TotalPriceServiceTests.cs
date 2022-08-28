using AlsoftShop.Repository.Interfaces;
using AlsoftShop.Services;
using AlsoftShop.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using AlsoftShop.Models;

namespace AlsoftShop.Tests.Services
{
    [TestFixture]
    public class TotalPriceServiceTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<ISubtotalPriceService> subtotalPriceServiceMock;
        private Mock<IDiscountService> discountServiceMock;

        private TotalPriceService sut;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();

            subtotalPriceServiceMock = new Mock<ISubtotalPriceService>();

            discountServiceMock = new Mock<IDiscountService>();

            sut = new TotalPriceService(repositoryMock.Object, subtotalPriceServiceMock.Object, discountServiceMock.Object);
        }

        [Test]
        public void GetPrice_WhenNullProvided_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetPrice(null));
        }

        [Test]
        public void GetPrice_WhenEmptyCartProvided_ReturnsZero()
        {
            Assert.AreEqual(0, sut.GetPrice(new List<CartItem>()).Subtotal);
            Assert.AreEqual(0, sut.GetPrice(new List<CartItem>()).Discount);
            Assert.AreEqual(0, sut.GetPrice(new List<CartItem>()).Total);
        }

        [Test]
        [TestCase(100, 10, 90)]
        [TestCase(100, 0, 100)]
        [TestCase(100, 100, 0)]
        public void GetPrice_WhenCartAndDiscountsProvided_ReturnsCorrectPrice(decimal expectedSubtotal, decimal expectedDiscount, decimal expectedTotal)
        {
            // Arrange
            subtotalPriceServiceMock.Setup(x => x.GetSubtotal(It.IsAny<IEnumerable<CartItem>>()))
                .Returns(expectedSubtotal);
            discountServiceMock.Setup(x => x.GetDiscount(It.IsAny<IEnumerable<CartItem>>(), It.IsAny<IEnumerable<Discount>>()))
                .Returns(expectedDiscount);


            var cartItems = new List<CartItem>
            {
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
            var priceInfo = sut.GetPrice(cartItems);

            // Assert
            Assert.AreEqual(expectedSubtotal, priceInfo.Subtotal);
            Assert.AreEqual(expectedDiscount, priceInfo.Discount);
            Assert.AreEqual(expectedTotal, priceInfo.Total);
        }

    }
}
