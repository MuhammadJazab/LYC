using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

#nullable disable
namespace LYC.Models.Identity
{
    public class ApplicationUser: IdentityUser
    {
        //[Key]
        //public long StaffId { get; set; }

        public bool Deleted { get; set; }

        public string UserDepartment { get; set; }

        public string Contact { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}