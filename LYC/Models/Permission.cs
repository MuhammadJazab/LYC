using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Permission), Schema = SchemaName.MCS)]
    public class Permission
    {
        
        public long RoleId { get; set; }

        [Key]
        public long PermissionId { get; set; }

        public bool AddPermission { get; set; }

        public bool EditPermission { get; set; }

        public bool DeletePermission { get; set; }

        public bool ViewPermission { get; set; }

        public bool MobileViewPermission { get; set; }
    }
}

