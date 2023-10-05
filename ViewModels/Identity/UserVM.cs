using System;
using System.Collections.Generic;

namespace ViewModels.Identity
{
    public class UserVM
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string ICPassport { get; set; }
        public string PostalCode { get; set; }
        public string DateOfBirth { get; set; }
        public string State { get; set; }
        public string Contact { get; set; }
        public string Country { get; set; }
        public string BloodGroup { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public long BranchId { get; set; }
    }
}

