using AlsoftShop.Models;
using AlsoftShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;
using AlsoftShop.Factories.Interfaces;
using System.Globalization;

namespace AlsoftShop.Repository
{
    public class DatabaseRepository : IRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public DatabaseRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public void AddProductToCart(int productId)
        {
            using IDbConnection db = dbConnectionFactory.GetConnection();
            db.Open();

            var cartItem = db.Query<CartItem>($"SELECT * FROM CartItem WHERE ShoppingCartId = 1 AND ProductId = {productId}")
                .FirstOrDefault();

            if(cartItem == null)
            {
                db.Query($"INSERT INTO CartItem (ProductId, Quantity, ShoppingCartId) VALUES ({productId}, 1, 1)");
            }
            else
            {
                db.Query($"UPDATE dbo.CartItem SET QUANTITY = {cartItem.Quantity + 1} WHERE ProductId = {productId}");
            }
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            using IDbConnection db = dbConnectionFactory.GetConnection();
            db.Open();

            var result = db.Query<CartItem>("SELECT * FROM CartItem WHERE ShoppingCartId = 1");
            var products = GetProducts();
            foreach (var item in result)
            {
                item.Product = products.First(p => p.Id == item.ProductId);
            }

            return result;
        }

        public IEnumerable<Discount> GetDiscounts()
        {
            using IDbConnection db = dbConnectionFactory.GetConnection();
            db.Open();

            var result = db.Query<Discount>("SELECT * FROM Discount");

            return result;
        }

        public IEnumerable<Product> GetProducts()
        {
            using IDbConnection db = dbConnectionFactory.GetConnection();
            db.Open();

            var result = db.Query<Product>("SELECT * FROM Product");

            return result;
        }

        public void RemoveProductFromCart(int productId)
        {
            using IDbConnection db = dbConnectionFactory.GetConnection();
            db.Open();

            var cartItem = db.Query<CartItem>($"SELECT * FROM CartItem WHERE ShoppingCartId = 1 AND ProductId = {productId}")
                .FirstOrDefault();

            if (cartItem == null)
            {
                throw new ArgumentOutOfRangeException(nameof(productId));
            }

            if(cartItem.Quantity > 1)
            {
                db.Query($"UPDATE dbo.CartItem SET Quantity = {cartItem.Quantity - 1} WHERE ProductId = {productId}");
            }
            else
            {
                db.Query($"DELETE FROM CartItem WHERE ProductId = {productId}");

            }
        }

        public void StorePrice(PriceInfo priceInfo)
        {
            using IDbConnection db = dbConnectionFactory.GetConnection();
            db.Open();

            var str = $"UPDATE ShoppingCart SET Subtotal = {priceInfo.Subtotal}, Discount = {priceInfo.Discount}, Total = {priceInfo.Total} ";
            var result = db.Query<Product>(str);
        }
    }
}
