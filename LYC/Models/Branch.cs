using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Utilities.Constants;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Branch), Schema = SchemaName.MCS)]
    public class Branch
    {
        [Key]
        public long BranchId { get; set; }

        public string RegistrationNumber { get; set; }

        public string BranchName { get; set; }

        public string PersonInCharge { get; set; }

        public string Address { get; set; }

        public string TelephoneNumber { get; set; }

        public string CSTelNumber { get; set; }

        public string Email { get; set; }

        public string WebSite { get; set; }

        public bool DeletionState { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
