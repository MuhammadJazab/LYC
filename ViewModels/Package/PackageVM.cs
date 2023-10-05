using System;
namespace ViewModels.Package
{
    public class PackageVM
    {
        public long? PackageId { get; set; }

        public long? ServiceId { get; set; }

        public string PackageNumber { get; set; }

        public string PackageName { get; set; }

        public int StayPeriodDays { get; set; }

        public string PackageType { get; set; }

        public decimal PackageCost { get; set; }
    }
}

