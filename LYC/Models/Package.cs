using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Package), Schema = SchemaName.MCS)]
    public class Package
    {
        [Key]
        public long PackageId { get; set; }

        public string PackageNumber { get; set; }

        public string PackageName { get; set; }

        public int StayPeriodDays { get; set; }
        
        public string PackageType { get; set; }
        
        public decimal PackageCost { get; set; }

        public bool DeletionState { get; set; }

        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

