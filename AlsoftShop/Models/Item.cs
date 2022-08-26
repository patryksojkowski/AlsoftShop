using System.Collections.Generic;

namespace AlsoftShop.Models
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Unit { get; set; }
        public Dictionary<string, string> AdditionalProperties { get; set; } = new Dictionary<string, string>();
    }
}
