using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities.Enums;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Service), Schema = SchemaName.MCS)]
    public class Service
    {
        [Key]
        public long ServiceId { get; set; }
        public long BranchId { get; set; }
        public long? PackageId { get; set; }

        public string ServiceName { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        public int MaxOccupants { get; set; }

        public decimal ServiceCost { get; set; }
        public EnumServiceStatus ServiceStatus { get; set; }
        public bool DeletionState { get; set; }

        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

