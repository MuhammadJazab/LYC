using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Utilities.Constants;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Booking), Schema = SchemaName.MCS)]
    public class Booking
    {
        [Key]
        public long BookingId { get; set; }

        
        public long CustomerId { get; set; }

        
        public long BranchId { get; set; }

        
        public long PackageId { get; set; }

        
        public long RoomId { get; set; }

        
        public decimal DepositCost { get; set; }

        
        public decimal PackageCost { get; set; }

        
        public decimal ExtraBabyCost { get; set; }

        
        public decimal ServiceCost { get; set; }

        
        public decimal TotalCost { get; set; }

        public int Discount { get; set; }

        public long DiscountApprovedBy { get; set; }

        public DateTime DiscountApprovedDate { get; set; }

        
        public int NoOfBabies { get; set; }

        public bool SpecialRequest { get; set; }

        
        public DateTime StartDate { get; set; }

        
        public DateTime EndDate { get; set; }
        public string Status { get; set; }

        /// <summary>
        /// JSON string is required
        /// </summary>
        public string HistoryTracks { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
