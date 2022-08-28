using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AlsoftShop.Models
{
    public class Discount
    {
        public int DiscountedItemId { get; set; }

        public int DiscountTriggerItemId { get; set; }

        public int DiscountTriggerItemCount { get; set; }

        public decimal DiscountPercentage { get; set; }
    }
}
