using System;
using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(RoomUnavailability), Schema = SchemaName.MCS)]
    public class RoomUnavailability
    {
        [Key]
        public long RoomUnavailabilityId { get; set; }

        public long RoomId { get; set; }

        public DateTime UnvailabeFrom { get; set; }

        public DateTime UnvailabeTo { get; set; }
    }
}