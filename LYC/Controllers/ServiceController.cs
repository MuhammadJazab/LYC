
using static Utilities.Constants;
using Utilities;
using ViewModels.Service;
using LYC.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LYC.Controllers
{
    [ApiController]
    [Produces("Application/json")]
    public class ServiceController : BaseController
    {
        private const string api = "api/Service/";

        private readonly ILogger<ServiceController> _logger;

        private readonly IServicesService _servicesService;

        public ServiceController(ILogger<ServiceController> logger, IServicesService servicesService)
        {
            _logger = logger;
            _servicesService = servicesService;
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpGet($"{api}{nameof(GetServices)}")]
        public Response GetServices()
        {
            _logger.LogInformation($"A request is made to {nameof(GetServices)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _servicesService.GetServices();
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpPost($"{api}{nameof(AddNewService)}")]
        public Response AddNewService([FromBody] ServiceVM serviceVM)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewService)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwtVM = GetAuthorizationHeader();
            return _servicesService.AddNewService(serviceVM, userJwtVM);
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpPost($"{api}{nameof(UpdateService)}")]
        public Response UpdateService([FromBody] ServiceVM serviceVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdateService)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwtVM = GetAuthorizationHeader();
            return _servicesService.UpdateService(serviceVM, userJwtVM);
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpGet($"{api}{nameof(DeleteService)}")]
        public Response DeleteService(long serviceId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteService)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwtVM = GetAuthorizationHeader();
            return _servicesService.DeleteService(serviceId, userJwtVM);
        }
    }
}

