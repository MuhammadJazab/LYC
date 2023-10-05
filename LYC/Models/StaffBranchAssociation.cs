using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(StaffBranchAssociation), Schema = SchemaName.MCS)]
    public class StaffBranchAssociation
    {
        [Key]
        public long StaffBranchAssociationId { get; set; }

        public long BranchId { get; set; }

        public string UserId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

