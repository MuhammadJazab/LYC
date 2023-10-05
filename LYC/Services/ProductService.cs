using LYC.Helpers;
using LYC.Models;
using Utilities;
using Utilities.Enums;
using ViewModels;
using ViewModels.Product;

namespace LYC.Services
{
    public interface IProductService
    {
        Response AddNewProduct(ProductVM productVM, UserJwtVM userJwtVM);
        Response DeleteProduct(long productId, UserJwtVM userJwtVM);
        Response GetProductes();
        Response GetProductImage(long productId);
        Response UpdateProduct(ProductVM productVM, UserJwtVM userJwtVM);
    }

    class ProductService : IProductService
    {
        private readonly IUnitOfWork _uow;

        public ProductService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Response AddNewProduct(ProductVM productVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingProduct = _uow.Repository<Product>().Get().Where(x => string.Equals(x.ProductNumber, productVM.ProductNumber)).FirstOrDefault();

                if (existingProduct == null)
                {
                    Product product = new Product()
                    {
                        ProductNumber = productVM.ProductNumber,
                        ProductName = productVM.ProductName,
                        ProductStatus = productVM.ProductStatus,
                        DisplayCost = productVM.DisplayCost,
                        ProductImage = productVM.ProductImage,
                        ProductCost = productVM.ProductCost,
                        CreatedBy = userJwtVM.UserId,
                        CreatedOn = DateTime.Now,
                        DeletionState = false
                    };

                    _uow.Repository<Product>().Add(product);
                    _uow.Save();

                    ProductBranchAssociation productBranchAssociation = new ProductBranchAssociation
                    {
                        ProductId = product.ProductId,
                        BranchId = productVM.BranchId.Value,
                        CreatedBy = userJwtVM.UserId,
                        CreatedOn = DateTime.Now
                    };

                    _uow.Repository<ProductBranchAssociation>().Add(productBranchAssociation);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.OK,
                        Message = ResponseMessages.Successfull
                    };
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = $"{nameof(Product)} {ResponseMessages.AlreadyExist}"
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public Response DeleteProduct(long productId, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingProduct = _uow.Repository<Product>().Get().Where(x => x.ProductId == productId && x.DeletionState == false).FirstOrDefault();

                if (existingProduct != null)
                {
                    existingProduct.DeletionState = true;
                    existingProduct.ModifiedOn = DateTime.Now;
                    existingProduct.ModifiedBy = userJwtVM.UserId;

                    _uow.Repository<Product>().Update(existingProduct);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.OK,
                        Message = ResponseMessages.Successfull
                    };
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public Response GetProductImage(long productId)
        {
            Response response;

            try
            {
                var productsImage = _uow.Repository<Product>().Get().Where(x => x.ProductId == productId).Select(x=>x.ProductImage).FirstOrDefault(); ;

                if (!string.IsNullOrEmpty(productsImage))
                {
                    response = new Response()
                    {
                        ResultData = productsImage,
                        Status = ResponseStatus.OK,
                        Message = ResponseMessages.Successfull
                    };
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public Response GetProductes()
        {
            Response response;

            try
            {
                var products = _uow.Repository<Product>().GetAll();

                var productList = products.Where(x => x.DeletionState == false).Select(x=> new 
                {
                    x.ProductId,
                    x.ProductName,
                    x.DisplayCost,
                    x.ProductCost,
                    x.ProductStatus,
                    x.ProductNumber,
                    x.CreatedOn
                }).OrderByDescending(x=>x.CreatedOn).ToList();

                if (productList?.Count > 0)
                {
                    List<ProductVM> productVMList = new List<ProductVM>();

                    foreach (var item in productList)
                    {
                        var productBranchAssociation = _uow.Repository<ProductBranchAssociation>().Get().Where(x => x.ProductId == item.ProductId).FirstOrDefault();

                        ProductVM product = new ProductVM
                        {
                            ProductId = item.ProductId,
                            DisplayCost = item.DisplayCost,
                            ProductCost = item.ProductCost,
                            ProductName = item.ProductName,
                            ProductStatus = item.ProductStatus,
                            ProductNumber = item.ProductNumber,
                            BranchId = productBranchAssociation?.BranchId
                        };

                        productVMList.Add(product);
                    }

                    response = new Response()
                    {
                        ResultData = productVMList,
                        Status = ResponseStatus.OK,
                        Message = ResponseMessages.Successfull
                    };
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = ResponseMessages.DataNotFound
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }

        public Response UpdateProduct(ProductVM productVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingProduct = _uow.Repository<Product>().Get().Where(x => string.Equals(x.ProductNumber, productVM.ProductNumber)).FirstOrDefault();

                if (existingProduct != null)
                {
                    existingProduct.ProductNumber = productVM.ProductNumber;
                    existingProduct.ProductName = productVM.ProductName;
                    existingProduct.ProductStatus = productVM.ProductStatus;
                    existingProduct.ProductImage = productVM.ProductImage;
                    existingProduct.DisplayCost = productVM.DisplayCost;
                    existingProduct.ProductCost = productVM.ProductCost;
                    existingProduct.ModifiedBy = userJwtVM.UserId;
                    existingProduct.ModifiedOn = DateTime.Now;

                    _uow.Repository<Product>().Update(existingProduct);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = existingProduct,
                        Status = ResponseStatus.OK,
                        Message = ResponseMessages.Successfull
                    };
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = $"{nameof(Product)} {ResponseMessages.DataNotFound}"
                    };
                }
            }
            catch (Exception ex)
            {
                response = new Response()
                {
                    Message = ex.Message,
                    ResultData = ex,
                    Status = ResponseStatus.Error
                };
            }

            return response;
        }
    }
}

