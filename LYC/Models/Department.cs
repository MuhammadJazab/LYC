using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Department), Schema = SchemaName.MCS)]
    public class Department
    {
        [Key]
        public long DepartmentID { get; set; }

        public string DepartmentName { get; set; }

        public string CreatedBy { get; set; }

        public bool DeletionState { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

