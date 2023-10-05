using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Food), Schema = SchemaName.MCS)]
    public class Food
    {
        [Key]
        public long FoodId { get; set; }

        
        public int FoodNo { get; set; }

        
        public long FoodTypeId { get; set; }

        
        public string FoodName { get; set; }

        
        public decimal FoodCost { get; set; }

        public byte[] FoodImage { get; set; }

        public bool DeletionState { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

