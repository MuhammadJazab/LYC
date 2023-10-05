using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Nursery), Schema = SchemaName.MCS)]
    public class Nursery
    {
        [Key]
        
        public long NurseryId { get; set; }

        public long BookingId { get; set; }
        public string ChildName { get; set; }
        public int ChildCNo { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

