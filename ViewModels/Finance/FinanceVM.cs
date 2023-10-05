using System;
using Utilities.Enums;

namespace ViewModels.Finance
{
    public class FinanceVM
    {
        public long FinanceId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public EnumFinanceType AccountsEntry { get; set; }
        public long ItemId { get; set; }
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public int Qty { get; set; }
        public decimal Cost { get; set; }
    }
}

