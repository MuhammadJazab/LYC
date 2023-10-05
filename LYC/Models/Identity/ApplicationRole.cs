using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

#nullable disable
namespace LYC.Models.Identity
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole():base()
        {}

        public ApplicationRole(string roleName)
        {
            Name = roleName;
        }

        public bool Deleted { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

