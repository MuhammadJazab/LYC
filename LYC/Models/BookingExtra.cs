using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(BookingExtra), Schema = SchemaName.MCS)]
    public class BookingExtra
    {
        [Key]
        public long BookExtraId { get; set; }

        
        public long BranchId { get; set; }

        
        public string BookExtraType { get; set; }

        
        public long BookExtraTypeId { get; set; }

        
        public string TypeName { get; set; }

        
        public decimal UnitCost { get; set; }
        
        public int UnityQty { get; set; }

        public bool DeletionState { get; set; }

        public string CreatedBy { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

