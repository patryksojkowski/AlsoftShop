using System.Collections.Generic;

namespace AlsoftShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
    }
}
