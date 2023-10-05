using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(DepartmentUserAssociation), Schema = SchemaName.MCS)]
    public class DepartmentUserAssociation
    {
        [Key]
        public long DepartmentUserAssociationId { get; set; }

        public long DepartmentId { get; set; }
        public string UserId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}

