using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Promotion), Schema = SchemaName.MCS)]
    public class Promotion
    {
        [Key]
        public long PromotionId { get; set; }

        public string PromotionName { get; set; }

        public long? ServiceId { get; set; }
        public long? ProductId { get; set; }

        public int DiscountType { get; set; }
        public decimal Discount { get; set; }

        public DateTime ExpiryDate { get; set; }

        public string PromotionImage { get; set; }

        public bool DeletionState { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

