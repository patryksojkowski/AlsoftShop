using AlsoftShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Repository.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Product> GetProducts();

        // todo CartItems should be stored in session and should be stored in DB only at program exit
        IEnumerable<CartItem> GetCartItems();

        void AddProductToCart(int productId);

        void RemoveProductFromCart(int productId);

        IEnumerable<Discount> GetDiscounts();
    }
}
