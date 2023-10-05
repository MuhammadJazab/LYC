using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Utilities.Constants;
using Utilities;
using ViewModels.Department;
using LYC.Services;

namespace LYC.Controllers
{
    [ApiController]
    [Produces("Application/json")]
    public class DepartmentController : BaseController
    {
        private const string api = "api/Department/";

        private readonly ILogger<DepartmentController> _logger;
        private readonly IDepartmentService _departmentService;

        public DepartmentController(ILogger<DepartmentController> logger, IDepartmentService departmentService)
        {
            _logger = logger;
            _departmentService = departmentService;
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpGet($"{api}{nameof(GetDepartments)}")]
        public Response GetDepartments()
        {
            _logger.LogInformation($"A request is made to {nameof(GetDepartments)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _departmentService.GetDepartments();
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpPost($"{api}{nameof(AddNewDepartment)}")]
        public Response AddNewDepartment([FromBody] DepartmentVM departmentVM)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewDepartment)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _departmentService.AddNewDepartment(departmentVM, userJwt);
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpPost($"{api}{nameof(UpdateDepartment)}")]
        public Response UpdateDepartment([FromBody] DepartmentVM departmentVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdateDepartment)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _departmentService.UpdateDepartment(departmentVM, userJwt);
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},{UserRoles.Accountant}")]
        [HttpGet($"{api}{nameof(DeleteDepartment)}")]
        public Response DeleteDepartment(long departmentId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteDepartment)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _departmentService.DeleteDepartment(departmentId, userJwt);
        }
    }
}

