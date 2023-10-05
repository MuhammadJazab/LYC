using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(ServiceDayAssociation), Schema = SchemaName.MCS)]
    public class ServiceDayAssociation
    {
        [Key]
        public long ServiceDayAssociationId { get; set; }

        public int DayId { get; set; }
        public long ServiceId { get; set; }

        public bool DeletionState { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

