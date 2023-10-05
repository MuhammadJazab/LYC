using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(MealOrder), Schema = SchemaName.MCS)]
    public class MealOrder
    {
        [Key]
        public long MealOrderId { get; set; }

        
        public long MealId { get; set; }

        
        public long BookingId { get; set; }

        
        public long FoodId { get; set; }

        
        public DateTime MealDate { get; set; }

        
        public string MealType { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

