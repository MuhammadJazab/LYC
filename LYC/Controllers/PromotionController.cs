using LYC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Utilities.Constants;
using Utilities;
using ViewModels.Promotion;

namespace LYC.Controllers
{
    [ApiController]
    [Produces("Application/json")]
    public class PromotionController : BaseController
    {
        private const string api = "api/Promotion/";

        private readonly ILogger<PromotionController> _logger;

        private readonly IPromotionService _promotionService;

        public PromotionController(ILogger<PromotionController> logger, IPromotionService promotionService)
        {
            _logger = logger;
            _promotionService = promotionService;
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(GetPromotions)}")]
        public Response GetPromotions()
        {
            _logger.LogInformation($"A request is made to {nameof(GetPromotions)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _promotionService.GetPromotions();
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(AddNewPromotion)}")]
        public Response AddNewPromotion([FromBody] PromotionVM promotionVM)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewPromotion)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _promotionService.AddNewPromotion(promotionVM, userJwt);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(UpdatePromotion)}")]
        public Response UpdatePromotion([FromBody] PromotionVM promotionVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdatePromotion)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _promotionService.UpdatePromotion(promotionVM, userJwt);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(DeletePromotion)}")]
        public Response DeletePromotion(long promotionId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeletePromotion)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _promotionService.DeletePromotion(promotionId, userJwt);
        }
    }
}