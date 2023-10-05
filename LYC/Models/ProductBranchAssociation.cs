using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(ProductBranchAssociation), Schema = SchemaName.MCS)]
    public class ProductBranchAssociation
    {
        [Key]
        public long ProductBranchAssociationId { get; set; }

        public long BranchId { get; set; }
        public long ProductId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

