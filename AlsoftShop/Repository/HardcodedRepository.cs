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
        private IEnumerable<Item> items;

        private IList<ShoppingCartItem> currentItems = new List<ShoppingCartItem>();

        public HardcodedRepository()
        {
            items = new List<Item>
            {
                new Item
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
                new Item
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

            currentItems = new List<ShoppingCartItem>
            {
                new ShoppingCartItem
                {
                    Item = new Item
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

        public void AddItem(int id)
        {
            var currentItem = currentItems.FirstOrDefault(i => i.Item.Id == id);
            if(currentItem is null)
            {
                var item = items.First(i => i.Id == id);
                currentItem = new ShoppingCartItem
                {
                    Item = item,
                };
                currentItems.Add(currentItem);
            }

            currentItem.Quantity++;
        }

        public void RemoveItem(Guid id)
        {
            var itemToRemove = currentItems.First(i => i.Id == id);

            if(itemToRemove.Quantity == 1)
            {
                currentItems.Remove(itemToRemove);
            }

            itemToRemove.Quantity--;
        }

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public IEnumerable<ShoppingCartItem> GetCurrentItems()
        {
            return currentItems;
        }
    }
}
