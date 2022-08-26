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
        public IEnumerable<Item> GetItems()
        {
            return new List<Item>
            {
                new Item
                {
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
    }
}
