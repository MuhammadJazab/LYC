using System.ComponentModel.DataAnnotations;
using static Utilities.Constants;
using System.ComponentModel.DataAnnotations.Schema;
using Utilities;
using Utilities.Enums;

#nullable disable
namespace LYC.Models
{
    [Table(nameof(Room), Schema = SchemaName.MCS)]
    public class Room
    {
        [Key]
        public long RoomId { get; set; }

        public long BranchId { get; set; }

        public long RoomNumber { get; set; }

        public string RoomName { get; set; }

        public string RoomType { get; set; }

        public int MaxOccupents { get; set; }

        public EnumAccomodationChoice? AccomodationChoice { get; set; }

        public EnumRoomStatus RoomStatus { get; set; }

        public bool DeleteState { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

