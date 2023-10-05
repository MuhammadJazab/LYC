using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Utilities.Constants;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Product),Schema = SchemaName.MCS)]
    public class Product
    {
        [Key]
        public long ProductId { get; set; }

        public string ProductNumber { get; set; }

        
        public string ProductName { get; set; }

        
        public decimal ProductCost { get; set; }
        public decimal DisplayCost { get; set; }


        public string ProductImage { get; set; }

        
        public string ProductStatus { get; set; }

        public bool DeletionState { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
