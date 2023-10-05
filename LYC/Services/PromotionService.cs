using LYC.Helpers;
using LYC.Models;
using Utilities;
using Utilities.Enums;
using ViewModels;
using ViewModels.Promotion;

namespace LYC.Services
{
    public interface IPromotionService
    {
        Response AddNewPromotion(PromotionVM promotionVM, UserJwtVM userJwt);
        Response GetPromotions();
        Response UpdatePromotion(PromotionVM promotionVM, UserJwtVM userJwt);
        Response DeletePromotion(long promotionId, UserJwtVM userJwt);
    }

    class PromotionService : IPromotionService
    {
        private readonly IUnitOfWork _uow;

        public PromotionService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Response AddNewPromotion(PromotionVM promotionVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingPromotion = _uow.Repository<Promotion>().Get().Where(x => string.Equals(x.PromotionName, promotionVM.PromotionName)).FirstOrDefault();

                if (existingPromotion == null)
                {
                    Promotion promotion = new Promotion()
                    {
                        PromotionName = promotionVM.PromotionName,
                        Discount = promotionVM.Discount,
                        DiscountType = promotionVM.DiscountType,
                        PromotionImage = promotionVM.PromotionImage,
                        ServiceId = promotionVM.ServiceId,
                        ProductId = promotionVM.ProductId,
                        ExpiryDate = promotionVM.ExpiryDate,
                        CreatedBy = userJwtVM.UserId,
                        CreatedOn = DateTime.Now,
                        DeletionState = false
                    };

                    _uow.Repository<Promotion>().Add(promotion);
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
                        Message = $"{nameof(Promotion)} {ResponseMessages.AlreadyExist}"
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

        public Response DeletePromotion(long promotionId, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingPromotion = _uow.Repository<Promotion>().Get().Where(x => x.PromotionId == promotionId && x.DeletionState == false).FirstOrDefault();

                if (existingPromotion != null)
                {
                    existingPromotion.DeletionState = true;
                    existingPromotion.ModifiedOn = DateTime.Now;
                    existingPromotion.ModifiedBy = userJwtVM.UserId;

                    _uow.Repository<Promotion>().Update(existingPromotion);
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

        public Response GetPromotions()
        {
            Response response;

            try
            {
                var promotions = _uow.Repository<Promotion>().GetAll();

                var promotionList = promotions.Where(x => x.DeletionState == false).OrderByDescending(x => x.CreatedOn).ToList();

                if (promotionList?.Count > 0)
                {
                    response = new Response()
                    {
                        ResultData = promotionList,
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

        public Response UpdatePromotion(PromotionVM promotionVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingPromotion = _uow.Repository<Promotion>().Get().Where(x => x.PromotionId == promotionVM.PromotionId).FirstOrDefault();

                if (existingPromotion != null)
                {
                    existingPromotion.PromotionName = promotionVM.PromotionName;
                    existingPromotion.Discount = promotionVM.Discount;
                    existingPromotion.DiscountType = promotionVM.DiscountType;
                    existingPromotion.PromotionId = promotionVM.PromotionId.Value;
                    existingPromotion.PromotionImage = promotionVM.PromotionImage;
                    existingPromotion.ServiceId = promotionVM.ServiceId;
                    existingPromotion.ExpiryDate = promotionVM.ExpiryDate;
                    existingPromotion.ModifiedBy = userJwtVM.UserId;
                    existingPromotion.ModifiedOn = DateTime.Now;

                    _uow.Repository<Promotion>().Update(existingPromotion);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = existingPromotion,
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
                        Message = $"{nameof(Promotion)} {ResponseMessages.DataNotFound}"
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

