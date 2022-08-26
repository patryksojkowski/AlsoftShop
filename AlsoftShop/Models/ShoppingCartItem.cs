using System;

namespace AlsoftShop.Models
{
    public class ShoppingCartItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Item Item { get; set; }
        public int Quantity { get; set; }
    }
}
