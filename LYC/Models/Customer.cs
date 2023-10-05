using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Customer), Schema = SchemaName.MCS)]
    public class Customer
    {
        /// <summary>
        /// Identity UserID
        /// </summary>
        [Column(Order = 0)]
        
        public string UserID { get; set; }

        [Key]
        public long CustomerId { get; set; }

        
        public string CustomerName { get; set; }

        
        public string CustomerEmail { get; set; }

        
        public string ICNoPassport { get; set; }

        
        public byte[] PhotoImage { get; set; }

        
        public DateTime DateOfBirth { get; set; }

        
        public int ContactNumber { get; set; }

        
        public string Address { get; set; }

        
        public string City { get; set; }

        
        public string State { get; set; }

        
        public string PostalCode { get; set; }

        
        public string Citizenship { get; set; }

        
        public DateTime EstDeliveryDate { get; set; }

        public int FollowupByHospital { get; set; }

        
        public long DoctorInChargeID { get; set; }

        public string Gravida { get; set; }

        public string Para { get; set; }

        /// <summary>
        /// JSON string is required
        /// </summary>
        public string LeadSourcrs { get; set; }

        public string PreferedFood { get; set; }

        public string ConsultancyNotes { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

