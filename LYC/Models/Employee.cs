using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Employee), Schema = SchemaName.MCS)]
    public class Employee
    {
        /// <summary>
        /// Identity UserID
        /// </summary>
        [Column(Order = 0)]
        
        public string UserID { get; set; }

        [Key]
        public long EmployeeId { get; set; }

        
        public long BranchId { get; set; }

        
        public long RoleId { get; set; }

        
        public long EmployeeNo { get; set; }

        
        public string EmployeeName { get; set; }

        
        public string Designation { get; set; }

        
        public string Department { get; set; }

        
        public string EmployeeContactNumber { get; set; }

        
        public string EmployeeEmail { get; set; }

        public bool DeletionState { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

