using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities;
using Utilities.Enums;
using ViewModels;

namespace LYC.Controllers
{
    [ApiController]
    [Produces("Application/json")]
    public class HomeController : BaseController
    {
        private const string api = "api/Home/";

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet($"{api}{nameof(Ping)}")]
        public Response Ping()
        {
            _logger.LogInformation($" API started successfully {DateTime.UtcNow.ToLongTimeString()}");

            return new Response()
            {
                ResultData = $"Http request is successfull at {DateTime.Now.Date.ToString("dd MMM, yyyy hh:mm tt")}",
                Status = ResponseStatus.OK,
                Message = " API started successfully"
            };
        }

        [HttpGet($"{api}{nameof(GetUserByToken)}")]
        public Response GetUserByToken()
        {
            _logger.LogInformation($" API started successfully {DateTime.UtcNow.ToLongTimeString()}");

            UserJwtVM userJwt = GetAuthorizationHeader();

            return new Response()
            {
                ResultData = userJwt.UserName,
                Status = ResponseStatus.OK,
                Message = ResponseMessages.Successfull
            };
        }
    }
}

