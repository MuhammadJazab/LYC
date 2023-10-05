using System;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Doctor), Schema = SchemaName.MCS)]
    public class Doctor
    {
        /// <summary>
        /// Identity UserID
        /// </summary>
        [Column(Order = 0)]
        public string UserID { get; set; }

        [Key]
        public long DoctorId { get; set; }

        
        public string DoctorName { get; set; }

        
        public string DoctorEmail { get; set; }

        
        public string DoctorDesignation { get; set; }

        
        public bool InService { get; set; }

        
        public DateTime ServingFrom { get; set; }

        public DateTime ServedTill { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

