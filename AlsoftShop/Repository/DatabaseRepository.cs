using AlsoftShop.Models;
using AlsoftShop.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using AlsoftShop.Factories.Interfaces;

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
            return;
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            return new List<CartItem>();
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
            return;
        }
    }
}
