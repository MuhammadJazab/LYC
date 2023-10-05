using LYC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Utilities.Constants;
using Utilities;
using ViewModels.Product;

namespace LYC.Controllers
{
    [ApiController]
    [Produces("Application/json")]
    public class ProductController : BaseController
    {
        private const string api = "api/Product/";

        private readonly ILogger<ProductController> _logger;

        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(GetProducts)}")]
        public Response GetProducts()
        {
            _logger.LogInformation($"A request is made to {nameof(GetProducts)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _productService.GetProductes();
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(AddNewProduct)}")]
        public Response AddNewProduct([FromBody] ProductVM ProductVM)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewProduct)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _productService.AddNewProduct(ProductVM, userJwt);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpPost($"{api}{nameof(UpdateProduct)}")]
        public Response UpdateProduct([FromBody] ProductVM ProductVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdateProduct)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _productService.UpdateProduct(ProductVM, userJwt);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(DeleteProduct)}")]
        public Response DeleteProduct(long productId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteProduct)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _productService.DeleteProduct(productId, userJwt);
        }

        [Authorize(Roles = UserRoles.SuperAdmin)]
        [HttpGet($"{api}{nameof(GetProductImage)}")]
        public Response GetProductImage(long productId)
        {
            _logger.LogInformation($"A request is made to {nameof(GetProductImage)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _productService.GetProductImage(productId);
        }
    }
}

