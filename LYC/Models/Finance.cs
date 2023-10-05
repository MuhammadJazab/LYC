using System;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Utilities.Enums;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Finance), Schema = SchemaName.MCS)]
    public class Finance
    {
        [Key]
        public long FinanceId { get; set; }
        public string UserId { get; set; }
        public EnumFinanceType AccountsEntry { get; set; }
        public long ItemId { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal Cost { get; set; }

        public bool DeletionState { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

