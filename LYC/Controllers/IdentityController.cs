using LYC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Utilities;
using ViewModels;
using ViewModels.Identity;
using static Utilities.Constants;

namespace LYC.Controllers
{
    [Produces("Application/json")]
    public class IdentityController : BaseController
    {
        private const string api = "api/Identity/";
        private readonly ILogger<IdentityController> _logger;
        private readonly IIdentityService _identityService;

        public IdentityController(ILogger<IdentityController> logger, IIdentityService identityService)
        {
            _logger = logger;
            _identityService = identityService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<Response> AuthenticateUser([FromBody] LoginVM model)
        {
            _logger.LogInformation($"A request is made to {nameof(AuthenticateUser)} at {DateTime.UtcNow.ToLongTimeString()}");
            return await _identityService.AuthenticateUser(model);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<Response> RegisterUser([FromBody] StaffVM model)
        {
            _logger.LogInformation($"A request is made to {nameof(RegisterUser)} at {DateTime.UtcNow.ToLongTimeString()}");

            UserJwtVM userJwt = GetAuthorizationHeader();
            return await _identityService.RegisterUser(model, userJwt);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<Response> AddNewRole([FromBody] RoleVM model)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewRole)} at {DateTime.UtcNow.ToLongTimeString()}");

            UserJwtVM userJwt = GetAuthorizationHeader();
            return await _identityService.AddNewRole(model, userJwt);
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        public Response GetRoles()
        {
            _logger.LogInformation($"A request is made to {nameof(GetRoles)} at {DateTime.UtcNow.ToLongTimeString()}");
            return _identityService.GetRoles();
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        public async Task<Response> GetAllUsers()
        {
            _logger.LogInformation($"A request is made to {nameof(GetAllUsers)} at {DateTime.UtcNow.ToLongTimeString()}");
            return await _identityService.GetAllUsers();
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},,{UserRoles.Accountant},{UserRoles.Chef}")]
        public async Task<Response> GetAllCustomers()
        {
            _logger.LogInformation($"A request is made to {nameof(GetAllCustomers)} at {DateTime.UtcNow.ToLongTimeString()}");
            return await _identityService.GetAllCustomers();
        }

        [HttpGet]
        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},,{UserRoles.Accountant},{UserRoles.Chef}")]
        public async Task<Response> GetAllCustomersByBranchId(long branchId)
        {
            _logger.LogInformation($"A request is made to {nameof(GetAllCustomersByBranchId)} at {DateTime.UtcNow.ToLongTimeString()}");
            return await _identityService.GetAllCustomersByBranchId(branchId);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        public async Task<Response> UpdateRole([FromBody] RoleVM roleVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdateRole)} at {DateTime.UtcNow.ToLongTimeString()}");

            UserJwtVM userJwt = GetAuthorizationHeader();
            return await _identityService.UpdateRole(roleVM, userJwt);
        }

        [HttpPost]
        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        public async Task<Response> UpdateUser([FromBody] StaffVM staffVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdateUser)} at {DateTime.UtcNow.ToLongTimeString()}");

            UserJwtVM userJwt = GetAuthorizationHeader();
            return await _identityService.UpdateUser(staffVM, userJwt);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<Response> DeleteRole(string roleId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteRole)} at {DateTime.UtcNow.ToLongTimeString()}");

            UserJwtVM userJwt = GetAuthorizationHeader();
            return await _identityService.DeleteRole(roleId, userJwt);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.SuperAdmin)]
        public async Task<Response> DeleteStaff(string userId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteRole)} at {DateTime.UtcNow.ToLongTimeString()}");

            UserJwtVM userJwt = GetAuthorizationHeader();
            return await _identityService.DeleteStaff(userId, userJwt);
        }
    }
}