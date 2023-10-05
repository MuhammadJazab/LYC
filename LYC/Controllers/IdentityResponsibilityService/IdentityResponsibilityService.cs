using Newtonsoft.Json;
using Utilities;
using ViewModels.Identity;

#nullable disable
namespace LYC.Controllers.IdentityResponsibilityService
{
    public interface IIdentityResponsibilityService
    {
        Task<string> AddNewRole(RoleVM model, string token);
        Task<string> AuthenticateUser(LoginVM model);
        Task<string> RegisterUser(StaffVM model, string token);
        Task<string> GetRoles(string token);
        Task<string> GetAllUsers(string token);
        Task<string> DeleteRole(string roleId, string token);
        Task<string> DeleteStaff(string userId, string token);
        Task<string> UpdateUser(StaffVM staffVM, string token);
        Task<string> UpdateRole(RoleVM roleVM, string token);
        Task<string> GetAllCustomers(string token);
        Task<string> GetAllCustomersByBranchId(long branchId, string token);
        Task<string> AddNewCustomer(UserVM userVM, string v);
    }

    class IdentityResponsibilityService : IIdentityResponsibilityService
    {
        private readonly IHttpClientService _httpClientService;

        public IdentityResponsibilityService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        #region Identity
        async Task<string> IIdentityResponsibilityService.AddNewRole(RoleVM model, string token)
        {
            return await _httpClientService.PostAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.AddNewRole}", model, token);
        }

        async Task<string> IIdentityResponsibilityService.AuthenticateUser(LoginVM model)
        {
            return await _httpClientService.PostAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.AuthenticateUser}", model);
        }

        async Task<string> IIdentityResponsibilityService.DeleteRole(string roleId, string token)
        {
            return await _httpClientService.GetAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.DeleteRole}?roleId={roleId}", token);
        }

        async Task<string> IIdentityResponsibilityService.DeleteStaff(string userId, string token)
        {
            return await _httpClientService.GetAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.DeleteStaff}?userId={userId}", token);
        }

        async Task<string> IIdentityResponsibilityService.GetAllCustomers(string token)
        {
            return await _httpClientService.GetAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.GetAllCustomers}", token);
        }

        async Task<string> IIdentityResponsibilityService.GetAllCustomersByBranchId(long branchId, string token)
        {
            return await _httpClientService.GetAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.GetAllCustomersByBranchId}?branchId={branchId}", token);
        }

        async Task<string> IIdentityResponsibilityService.GetAllUsers(string token)
        {
            return await _httpClientService.GetAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.GetAllUsers}", token);
        }

        async Task<string> IIdentityResponsibilityService.GetRoles(string token)
        {
            return await _httpClientService.GetAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.GetRoles}", token);
        }

        async Task<string> IIdentityResponsibilityService.RegisterUser(StaffVM model, string token)
        {
            return await _httpClientService.PostAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.RegisterUser}", model, token);
        }

        async Task<string> IIdentityResponsibilityService.UpdateRole(RoleVM roleVM, string token)
        {
            return await _httpClientService.PostAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.UpdateRole}", roleVM, token);
        }

        async Task<string> IIdentityResponsibilityService.UpdateUser(StaffVM staffVM, string token)
        {
            return await _httpClientService.PostAsync($"{ApiRoutes.Base.BaseUrl}{ApiRoutes.Identity.UpdateUser}", staffVM, token);
        }
        #endregion

        #region Customer
        public Task<string> AddNewCustomer(UserVM userVM, string v)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}

