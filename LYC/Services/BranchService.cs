using LYC.Helpers;
using LYC.Models;
using Utilities;
using ViewModels.Branch;
using ViewModels;
using Utilities.Enums;

namespace LYC.Services
{
    public interface IBranchService
    {
        Response AddNewBranch(BranchVM branchVM, UserJwtVM userJwtVM);
        Response DeleteBranch(string registrationNumber, UserJwtVM userJwtVM);
        Response GetBrancheByRegistrationNumber(string registrationNumber);
        Response GetBranches();
        Response GetBranchFacilitiesByBranchId(long branchId);
        Response UpdateBranch(BranchVM branchVM, UserJwtVM userJwtVM);
    }

    class BranchService : IBranchService
    {
        private readonly IUnitOfWork _uow;

        public BranchService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Response AddNewBranch(BranchVM branchVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingBranch = _uow.Repository<Branch>().Get().Where(x => string.Equals(x.RegistrationNumber, branchVM.RegistrationNumber)).FirstOrDefault();

                if (existingBranch == null)
                {
                    Branch branch = new Branch()
                    {
                        RegistrationNumber = branchVM.RegistrationNumber,
                        BranchName = branchVM.BranchName,
                        Address = branchVM.Address,
                        TelephoneNumber = branchVM.TelephoneNumber,
                        CreatedBy = userJwtVM.UserId,
                        CreatedOn = DateTime.Now,
                        PersonInCharge = branchVM.PersonInCharge,
                        CSTelNumber = branchVM.CSTelNumber,
                        WebSite = branchVM.WebSite,
                        Email = branchVM.Email,
                        DeletionState = false,
                    };

                    _uow.Repository<Branch>().Add(branch);
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
                        Message = $"{nameof(Branch)} {ResponseMessages.AlreadyExist}"
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

        public Response GetBranches()
        {
            Response response;

            try
            {
                var branchList = _uow.Repository<Branch>().GetAll().OrderByDescending(x => x.CreatedOn);

                var branches = branchList.Where(x => x.DeletionState == false).ToList();

                if (branches?.Count > 0)
                {
                    response = new Response()
                    {
                        ResultData = branches,
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

        public Response UpdateBranch(BranchVM branchVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingBranch = _uow.Repository<Branch>().Get().Where(x => string.Equals(x.RegistrationNumber, branchVM.RegistrationNumber)).FirstOrDefault();

                if (existingBranch != null)
                {
                    existingBranch.RegistrationNumber = branchVM.RegistrationNumber;
                    existingBranch.BranchName = branchVM.BranchName;
                    existingBranch.Address = branchVM.Address;
                    existingBranch.TelephoneNumber = branchVM.TelephoneNumber;
                    existingBranch.ModifiedBy = userJwtVM.UserId;
                    existingBranch.ModifiedOn = DateTime.Now;
                    existingBranch.PersonInCharge = branchVM.PersonInCharge;
                    existingBranch.CSTelNumber = branchVM.CSTelNumber;
                    existingBranch.WebSite = branchVM.WebSite;
                    existingBranch.Email = branchVM.Email;
                    existingBranch.DeletionState = false;

                    _uow.Repository<Branch>().Update(existingBranch);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = existingBranch,
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
                        Message = $"{nameof(Branch)} {ResponseMessages.DataNotFound}"
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

        public Response DeleteBranch(string registrationNumber, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingBranch = _uow.Repository<Branch>().Get().Where(x => x.RegistrationNumber == registrationNumber && x.DeletionState == false).FirstOrDefault();

                if (existingBranch != null)
                {
                    existingBranch.DeletionState = true;
                    existingBranch.ModifiedOn = DateTime.Now;
                    existingBranch.ModifiedBy = userJwtVM.UserId;

                    _uow.Repository<Branch>().Update(existingBranch);
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

        public Response GetBrancheByRegistrationNumber(string registrationNumber)
        {
            Response response;

            try
            {
                var branch = _uow.Repository<Branch>().Get();

                var filteredBranch = branch.Where(x => x.RegistrationNumber == registrationNumber && x.DeletionState == false).FirstOrDefault();

                if (filteredBranch != null)
                {
                    response = new Response()
                    {
                        ResultData = filteredBranch,
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

        public Response GetBranchFacilitiesByBranchId(long branchId)
        {
            Response response;

            try
            {
                var branch = _uow.Repository<Branch>().Get();
                var room = _uow.Repository<Room>().GetAll();

                var filteredBranch = branch.Where(x => x.BranchId == branchId && x.DeletionState == false).FirstOrDefault();

                if (filteredBranch != null)
                {
                    var filteredRooms = room.Where(x => x.BranchId == filteredBranch.BranchId && x.AccomodationChoice == EnumAccomodationChoice.Facility).ToList();

                    response = new Response()
                    {
                        ResultData = filteredRooms,
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

