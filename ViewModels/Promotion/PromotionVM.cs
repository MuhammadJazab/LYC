using System;
namespace ViewModels.Promotion
{
    public class PromotionVM
    {
        public long? PromotionId { get; set; }

        public string PromotionName { get; set; }

        public int DiscountType { get; set; }
        public decimal Discount { get; set; }

        public DateTime ExpiryDate { get; set; }
        public string PromotionImage { get; set; }

        public long? ServiceId { get; set; }
        public long? ProductId { get; set; }
    }
}

