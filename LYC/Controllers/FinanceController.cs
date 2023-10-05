using LYC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Utilities.Constants;
using Utilities;
using ViewModels.Finance;

namespace LYC.Controllers
{
    [ApiController]
    [Produces("Application/json")]
    public class FinanceController : BaseController
    {
        private const string api = "api/Finance/";

        private readonly ILogger<FinanceController> _logger;
        private readonly IFinanceService _financeService;

        public FinanceController(ILogger<FinanceController> logger, IFinanceService financeService)
        {
            _logger = logger;
            _financeService = financeService;
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},{UserRoles.Accountant}")]
        [HttpGet($"{api}{nameof(GetFinances)}")]
        public Response GetFinances()
        {
            _logger.LogInformation($"A request is made to {nameof(GetFinances)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _financeService.GetFinances();
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},{UserRoles.Accountant}")]
        [HttpPost($"{api}{nameof(AddNewFinance)}")]
        public Response AddNewFinance([FromBody] FinanceVM FinanceVM)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewFinance)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _financeService.AddNewFinance(FinanceVM, userJwt);
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},{UserRoles.Accountant}")]
        [HttpPost($"{api}{nameof(EditFinance)}")]
        public Response EditFinance([FromBody] FinanceVM FinanceVM)
        {
            _logger.LogInformation($"A request is made to {nameof(EditFinance)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _financeService.EditFinance(FinanceVM, userJwt);
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin},{UserRoles.Accountant}")]
        [HttpGet($"{api}{nameof(DeleteFinance)}")]
        public Response DeleteFinance(long financeId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteFinance)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _financeService.DeleteFinance(financeId, userJwt);
        }
    }
}

