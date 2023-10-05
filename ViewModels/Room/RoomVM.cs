using System;
using Utilities.Enums;

namespace ViewModels.Room
{
    public class RoomVM
    {
        public long RoomId { get; set; }
        public long BranchId { get; set; }

        public long RoomNumber { get; set; }    
        public string RoomName { get; set; }
        public string RoomType { get; set; }
        public EnumAccomodationChoice? AccomodationChoice { get; set; }
        public EnumRoomStatus RoomStatus { get; set; }
    }
}

