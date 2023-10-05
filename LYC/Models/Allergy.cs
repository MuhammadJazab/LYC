using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Allergy), Schema = SchemaName.MCS)]
    public class Allergy
    {
        [Key]
        public long AllergyId { get; set; }

        
        public string UserID { get; set; }

        
        public string AllergyName { get; set; }

        
        public string AllergyDescription { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

