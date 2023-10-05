using System;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Meal), Schema = SchemaName.MCS)]
    public class Meal
    {
        [Key]
        public long MealId { get; set; }

        
        public DateTime MealDateTime { get; set; }

        
        public string BreakfastOption { get; set; }

        
        public string LunchOption { get; set; }

        
        public string Dinnerption { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

