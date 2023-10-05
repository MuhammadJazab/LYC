using LYC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Utilities.Constants;
using Utilities;
using ViewModels.Package;

namespace LYC.Controllers
{
    [ApiController]
    [Produces("Application/json")]
    public class PackageController : BaseController
    {
        private const string api = "api/Package/";

        private readonly ILogger<PackageController> _logger;

        private readonly IPackageService _packageService;

        public PackageController(ILogger<PackageController> logger, IPackageService packageService)
        {
            _logger = logger;
            _packageService = packageService;
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(GetPackages)}")]
        public Response GetPackages()
        {
            _logger.LogInformation($"A request is made to {nameof(GetPackages)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _packageService.GetPackages();
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(AddNewPackage)}")]
        public Response AddNewPackage([FromBody] PackageVM packageVM)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewPackage)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwtVM = GetAuthorizationHeader();
            return _packageService.AddNewPackage(packageVM, userJwtVM);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(UpdatePackage)}")]
        public Response UpdatePackage([FromBody] PackageVM packageVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdatePackage)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwtVM = GetAuthorizationHeader();
            return _packageService.UpdatePackage(packageVM, userJwtVM);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(DeletePackage)}")]
        public Response DeletePackage(long packageId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeletePackage)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwtVM = GetAuthorizationHeader();
            return _packageService.DeletePackage(packageId, userJwtVM);
        }
    }
}

