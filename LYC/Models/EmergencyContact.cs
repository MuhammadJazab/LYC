using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(EmergencyContact), Schema = SchemaName.MCS)]
    public class EmergencyContact
    {
        [Key]
        public long EmergencyContactId { get; set; }

        public long CustomerId { get; set; }

        public string ContactName { get; set; }

        public string ContactRelation { get; set; }

        public string ContactPhoneNo { get; set; }

        public string ContactEmail { get; set; }

        public string ContactAddress { get; set; }
    }
}

