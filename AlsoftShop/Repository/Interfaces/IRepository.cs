﻿using AlsoftShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Repository.Interfaces
{
    public interface IRepository
    {
        IEnumerable<Item> GetItems();

        IEnumerable<ShoppingCartItem> GetCurrentItems();

        void AddItem(int id);

        void RemoveItem(Guid id);
    }
}
