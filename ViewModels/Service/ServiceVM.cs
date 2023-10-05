using System;
using System.Collections.Generic;
using Utilities.Enums;
using ViewModels.Room;

namespace ViewModels.Service
{
    public class ServiceVM
    {
        public long? ServiceId { get; set; }
        public long BranchId { get; set; }
        public string ServiceName { get; set; }
        public string BranchName { get; set; }
        public EnumServiceStatus ServiceStatus { get; set; }
        public decimal ServiceCost { get; set; }
        public int MaxOccupants { get; set; }
        public DateTime StartingDateTime { get; set; }
        public DateTime EndingDateTime { get; set; }
        public List<int> Days { get; set; }
        public List<RoomVM>? Rooms { get; set; }
        public string? RoomNames { get; set; }
    }
}

