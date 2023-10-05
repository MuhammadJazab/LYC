using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(RoomServiceAssociation), Schema = SchemaName.MCS)]
    public class RoomServiceAssociation
    {
        [Key]
        public long RoomServiceAssociationId { get; set; }
        public long ServiceId { get; set; }
        public long BranchId { get; set; }
        public long RoomId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

