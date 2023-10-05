using System.Collections.Generic;

namespace ViewModels.Identity
{
    public class StaffVM
    {
        public string UserId { get; set; }
        public long StaffId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Department { get; set; }
        public string UserRole { get; set; }
        public List<long> BranchIds { get; set; }
        public string Contact { get; set; }
    }
}

