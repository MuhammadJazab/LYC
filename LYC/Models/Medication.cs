using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Medication), Schema = SchemaName.MCS)]
    public class Medication
    {
        [Key]
        public long MedicationId { get; set; }

        
        public string UserID { get; set; }

        
        public string MedicationName { get; set; }

        
        public string MedicationDescription { get; set; }

        
        public DateTime MedicationStartingDate { get; set; }

        
        public DateTime MedicationEndingDate { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

