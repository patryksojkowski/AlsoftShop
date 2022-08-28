using AlsoftShop.Controllers;
using AlsoftShop.Models;
using AlsoftShop.Repository.Interfaces;
using AlsoftShop.Services.Interfaces;
using AlsoftShop.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlsoftShop.Tests.Controllers
{
    [TestFixture]
    public class ShopControllerTests
    {
        private Mock<IRepository> repositoryMock;
        private Mock<ITotalPriceService> totalPriceServiceMock;

        private ShopController sut;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IRepository>();

            totalPriceServiceMock = new Mock<ITotalPriceService>();

            sut = new ShopController(repositoryMock.Object, totalPriceServiceMock.Object);
        }

        [Test]
        public void Index_WhenDependencyThrowsException_ShouldReturnBadRequest()
        {
            // Arrange
            repositoryMock
                .Setup(x => x.GetProducts())
                .Throws(new Exception());

            // Act
            var result = sut.Index() as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
        }

        [Test]
        public void Index_WhenDependeciesReturnsCorrectResults_ShouldReturnCorrectViewModel()
        {
            // Arrange
            var someproduct = new Product
            {
                Id = 1,
                Name = "Some name",
                Price = 1,
                Unit = "Some unit",
                AdditionalProperties = new Dictionary<string, string>
                {
                    ["Origin"] = "Poland"
                }
            };

            var products = new List<Product>
            {
                someproduct
            };

            var cartItems = new List<CartItem>
            {
                new CartItem
                {
                    Quantity = 10,
                    Product = someproduct
                }
            };

            repositoryMock
                .Setup(x => x.GetProducts())
                .Returns(products);

            repositoryMock.Setup(x => x.GetCartItems())
                .Returns(cartItems);

            var priceInfo = new PriceInfo
            {
                Subtotal = 100,
                Discount = 10,
                Total = 90,
            };

            totalPriceServiceMock
            .Setup(x => x.GetPrice(cartItems))
                .Returns(priceInfo);

            // Act
            var result = sut.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);

            var viewModel = result.Model as ShopViewModel;
            Assert.NotNull(viewModel);

            CollectionAssert.AreEquivalent(products, viewModel.Products);
            CollectionAssert.AreEquivalent(cartItems, viewModel.CartItems);
            Assert.AreEqual(priceInfo.Subtotal, viewModel.PriceInfo.Subtotal);
            Assert.AreEqual(priceInfo.Discount, viewModel.PriceInfo.Discount);
            Assert.AreEqual(priceInfo.Total, viewModel.PriceInfo.Total);
        }

        // TODO below tests would make more sense
        // if shopping cart would be stored in session
        // and session content would be checked after Add() or Remove() calls
        [Test]
        public void Add_WhenObjectAddedCorrectly_ShouldReturnCorrectViewModel()
        {
            // Act
            var result = sut.Add(1) as ViewResult;

            // Assert
            Assert.NotNull(result);

            var viewModel = result.Model as ShopViewModel;
            Assert.NotNull(viewModel);

            repositoryMock.Verify(x => x.AddProductToCart(1), Times.Once);
        }

        // TODO below tests would make more sense
        // if shopping cart would be stored in session
        // and session content would be checked after Add() or Remove() calls
        [Test]
        public void Remove_WhenObjectRemovedCorrectly_ShouldReturnCorrectViewModel()
        {
            // Act
            var result = sut.Add(1) as ViewResult;

            // Assert
            Assert.NotNull(result);

            var viewModel = result.Model as ShopViewModel;
            Assert.NotNull(viewModel);

            repositoryMock.Verify(x => x.RemoveProductFromCart(1), Times.Once);
        }
    }
}
