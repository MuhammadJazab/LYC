using LYC.Helpers;
using LYC.Models;
using Utilities;
using Utilities.Enums;
using ViewModels;
using ViewModels.Finance;
using LYC.Models.Identity;

namespace LYC.Services
{
    public interface IFinanceService
    {
        Response AddNewFinance(FinanceVM financeVM, UserJwtVM userJwt);
        Response DeleteFinance(long financeId, UserJwtVM userJwt);
        Response EditFinance(FinanceVM financeVM, UserJwtVM userJwt);
        Response GetFinances();
    }

    class FinanceService : IFinanceService
    {
        private readonly IUnitOfWork _uow;

        public FinanceService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Response AddNewFinance(FinanceVM financeVM, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                Finance finance = new Finance
                {
                    UserId = financeVM.UserId,
                    AccountsEntry = financeVM.AccountsEntry,
                    Cost = financeVM.Cost,
                    ItemName = financeVM.ItemName,
                    ItemType = financeVM.ItemType,
                    ItemId = financeVM.ItemId,
                    Qty = financeVM.Qty,
                    DeletionState = false,
                    CreatedBy = userJwt.UserId,
                    CreatedOn = DateTime.Now
                };

                _uow.Repository<Finance>().Add(finance);
                _uow.Save();

                response = new Response()
                {
                    ResultData = null,
                    Status = ResponseStatus.OK,
                    Message = ResponseMessages.Successfull
                };
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

        public Response DeleteFinance(long financeId, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                var existingFinance = _uow.Repository<Finance>().Get().Where(x => x.FinanceId == financeId && x.DeletionState == false).FirstOrDefault();

                if (existingFinance != null)
                {
                    existingFinance.DeletionState = true;
                    existingFinance.ModifiedOn = DateTime.Now;
                    existingFinance.ModifiedBy = userJwt.UserId;

                    _uow.Repository<Finance>().Update(existingFinance);
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

        public Response EditFinance(FinanceVM financeVM, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                var existingFinance = _uow.Repository<Finance>().Get().Where(x => x.FinanceId == financeVM.FinanceId).FirstOrDefault();

                if (existingFinance != null)
                {
                    existingFinance.UserId = financeVM.UserId;
                    existingFinance.AccountsEntry = financeVM.AccountsEntry;
                    existingFinance.Cost = financeVM.Cost;
                    existingFinance.ItemName = financeVM.ItemName;
                    existingFinance.ItemType = financeVM.ItemType;
                    existingFinance.ItemId = financeVM.ItemId;
                    existingFinance.Qty = financeVM.Qty;
                    existingFinance.ModifiedBy = userJwt.UserId;
                    existingFinance.ModifiedOn = DateTime.Now;

                    _uow.Repository<Finance>().Update(existingFinance);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = existingFinance,
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
                        Message = $"{nameof(Finance)} {ResponseMessages.DataNotFound}"
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

        public Response GetFinances()
        {
            Response response;

            try
            {
                List<FinanceVM> financeVMs = new List<FinanceVM>();

                var financeList = _uow.Repository<Finance>().GetAll().OrderByDescending(x => x.CreatedOn);

                var finances = financeList.Where(x => x.DeletionState == false).ToList();

                if (finances?.Count > 0)
                {
                    var userList = _uow.Repository<ApplicationUser>().GetAll();

                    foreach (var finance in finances)
                    {
                        var users = userList.Where(x => x.Id == finance.UserId).ToList();

                        foreach (var user in users)
                        {
                            FinanceVM financeVM = new FinanceVM
                            {
                                FinanceId = finance.FinanceId,
                                AccountsEntry = finance.AccountsEntry,
                                Cost = finance.Cost,
                                ItemId = finance.ItemId,
                                ItemName = finance.ItemName,
                                ItemType = finance.ItemType,
                                Qty = finance.Qty,
                                UserId = user.Id,
                                UserName = user.UserName
                            };

                            financeVMs.Add(financeVM);
                        }
                    }

                    response = new Response()
                    {
                        ResultData = financeVMs,
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
    }
}

