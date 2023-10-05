using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(FoodIngredient), Schema = SchemaName.MCS)]
    public class FoodIngredient
    {
        [Key]
        public long FoodIngredientsId { get; set; }

        
        public long FoodId { get; set; }

        
        public string IngredientName { get; set; }

        
        public string IngredientDescription { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

