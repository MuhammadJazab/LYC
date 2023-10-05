using System;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(FoodNutration), Schema = SchemaName.MCS)]
    public class FoodNutration
    {
        [Key]
        public long FoodNutrationId { get; set; }

        
        public long FoodId { get; set; }

        
        public int TotalKiloCalories { get; set; }

        /// <summary>
        /// JSON string is required
        /// </summary>
        
        public string Nutration { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

