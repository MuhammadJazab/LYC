using System;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Day), Schema = SchemaName.MCS)]
    public class Day
    {
        [Key]
        public int DayId { get; set; }
        public string DayName { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

