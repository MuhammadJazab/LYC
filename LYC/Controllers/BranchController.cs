using LYC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;
using ViewModels.Branch;
using static Utilities.Constants;

namespace LYC.Controllers
{
    [ApiController]
    [Produces("Application/json")]
    public class BranchController : BaseController
    {
        private const string api = "api/Branch/";

        private readonly ILogger<BranchController> _logger;
        private readonly IBranchService _branchService;

        public BranchController(ILogger<BranchController> logger, IBranchService branchService)
        {
            _logger = logger;
            _branchService = branchService;
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(GetBranches)}")]
        public Response GetBranches()
        {
            _logger.LogInformation($"A request is made to {nameof(GetBranches)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _branchService.GetBranches();
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(GetBranchByRegistrationNumber)}")]
        public Response GetBranchByRegistrationNumber(string registrationNumber)
        {
            _logger.LogInformation($"A request is made to {nameof(GetBranchByRegistrationNumber)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _branchService.GetBrancheByRegistrationNumber(registrationNumber);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(GetBranchFacilitiesByBranchId)}")]
        public Response GetBranchFacilitiesByBranchId(long branchId)
        {
            _logger.LogInformation($"A request is made to {nameof(GetBranchFacilitiesByBranchId)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _branchService.GetBranchFacilitiesByBranchId(branchId);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(AddNewBranch)}")]
        public Response AddNewBranch([FromBody] BranchVM branchVM)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewBranch)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _branchService.AddNewBranch(branchVM, userJwt);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(UpdateBranch)}")]
        public Response UpdateBranch([FromBody] BranchVM branchVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdateBranch)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _branchService.UpdateBranch(branchVM, userJwt);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(DeleteBranch)}")]
        public Response DeleteBranch(string registrationNumber)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteBranch)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _branchService.DeleteBranch(registrationNumber, userJwt);
        }
    }
}

