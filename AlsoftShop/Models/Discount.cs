namespace AlsoftShop.Models
{
    public class Discount
    {
        public int DiscountedProductId { get; set; }

        public int DiscountTriggerProductId { get; set; }

        public int DiscountTriggerProductCount { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}
