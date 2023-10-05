using LYC.Controllers.IdentityResponsibilityService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Utilities.Constants;
using ViewModels.Identity;
using Utilities;

namespace LYC.Controllers.IdentityResponsibilityController
{
    [ApiController]
    //[Produces("application/json")]
    public class IdentityResponsibilityController : BaseController
    {
        private const string api = "api/IdentityResponsibility/";

        private readonly ILogger<IdentityResponsibilityController> _logger;
        private readonly IIdentityResponsibilityService _identityResponsibilityService;

        public IdentityResponsibilityController(ILogger<IdentityResponsibilityController> logger, IIdentityResponsibilityService identityResponsibilityService)
        {
            _logger = logger;
            _identityResponsibilityService = identityResponsibilityService;
        }

        [AllowAnonymous]
        [HttpPost($"{api}{nameof(AuthenticateUser)}")]
        public async Task<string> AuthenticateUser([FromBody] LoginVM model)
        {
            _logger.LogInformation($"A request is made to {nameof(AuthenticateUser)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.AuthenticateUser(model);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(RegisterUser)}")]
        public async Task<string> RegisterUser([FromBody] StaffVM model)
        {
            _logger.LogInformation($"A request is made to {nameof(RegisterUser)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.RegisterUser(model, GetTokenFromHeader());
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(AddNewRole)}")]
        public async Task<string> AddNewRole([FromBody] RoleVM model)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewRole)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.AddNewRole(model, GetTokenFromHeader());
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpGet($"{api}{nameof(GetRoles)}")]
        public async Task<string> GetRoles()
        {
            _logger.LogInformation($"A request is made to {nameof(GetRoles)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.GetRoles(GetTokenFromHeader());
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpGet($"{api}{nameof(GetAllUsers)}")]
        public async Task<string> GetAllUsers()
        {
            _logger.LogInformation($"A request is made to {nameof(GetAllUsers)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.GetAllUsers(GetTokenFromHeader());
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpPost($"{api}{nameof(UpdateRole)}")]
        public async Task<string> UpdateRole([FromBody] RoleVM roleVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdateRole)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.UpdateRole(roleVM, GetTokenFromHeader());
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpPost($"{api}{nameof(UpdateUser)}")]
        public async Task<string> UpdateUser([FromBody] StaffVM staffVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdateUser)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.UpdateUser(staffVM, GetTokenFromHeader());
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(DeleteRole)}")]
        public async Task<string> DeleteRole(string roleId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteRole)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.DeleteRole(roleId, GetTokenFromHeader());
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(DeleteStaff)}")]
        public async Task<string> DeleteStaff(string userId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteRole)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.DeleteStaff(userId, GetTokenFromHeader());
        }

        #region Customer
        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},,{UserRoles.Accountant},{UserRoles.Chef}")]
        [HttpGet($"{api}{nameof(GetAllCustomers)}")]
        public async Task<string> GetAllCustomers()
        {
            _logger.LogInformation($"A request is made to {nameof(GetAllCustomers)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.GetAllCustomers(GetTokenFromHeader());
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},,{UserRoles.Accountant}")]
        [HttpGet($"{api}{nameof(AddNewCustomer)}")]
        public async Task<string> AddNewCustomer(UserVM userVM)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewCustomer)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.AddNewCustomer(userVM,GetTokenFromHeader());
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},,{UserRoles.Accountant},{UserRoles.Chef}")]
        [HttpGet($"{api}{nameof(GetAllCustomersByBranchId)}")]
        public async Task<string> GetAllCustomersByBranchId(long branchId)
        {
            _logger.LogInformation($"A request is made to {nameof(GetAllCustomersByBranchId)} at {DateTime.UtcNow.ToLongTimeString()}");

            return await _identityResponsibilityService.GetAllCustomersByBranchId(branchId, GetTokenFromHeader());
        }
        #endregion
    }
}

