using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Payment), Schema = SchemaName.MCS)]
    public class Payment
    {
        [Key]
        
        public long PaymentId { get; set; }

        
        public long BookingId { get; set; }

        public string InvoiceCNNo { get; set; }

        public string DebitOrCredit { get; set; }

        public string PaymentType { get; set; }
        public decimal PaymentAmount { get; set; }
        public string PaymentPreparedBy { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Remarks { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}