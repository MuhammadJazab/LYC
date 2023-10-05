using LYC.Helpers;
using LYC.Models;
using Utilities;
using Utilities.Enums;
using ViewModels;
using ViewModels.Department;

namespace LYC.Services
{
    public interface IDepartmentService
    {
        Response GetDepartments();
        Response AddNewDepartment(DepartmentVM departmentVM, UserJwtVM userJwtVM);
        Response DeleteDepartment(long departmentId, UserJwtVM userJwt);
        Response UpdateDepartment(DepartmentVM departmentVM, UserJwtVM userJwt);
    }

    class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _uow;

        public DepartmentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Response AddNewDepartment(DepartmentVM departmentVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingDepartment = _uow.Repository<Department>().Get().Where(x => string.Equals(x.DepartmentName, departmentVM.DepartmentName)).FirstOrDefault();

                if (existingDepartment == null)
                {
                    Department department = new Department
                    {
                        DepartmentName = departmentVM.DepartmentName,
                        DeletionState = false,
                        CreatedBy = userJwtVM.UserId,
                        CreatedOn = DateTime.Now
                    };

                    _uow.Repository<Department>().Add(department);
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

        public Response GetDepartments()
        {
            Response response;

            try
            {
                var departmentList = _uow.Repository<Department>().GetAll().ToList();

                var departments = departmentList.Where(x => x.DeletionState == false).ToList();

                if (departments?.Count > 0)
                {
                    response = new Response()
                    {
                        ResultData = departments,
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

        public Response DeleteDepartment(long departmentId, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                var existingDepartment = _uow.Repository<Department>().Get().Where(x => x.DepartmentID == departmentId && x.DeletionState == false).FirstOrDefault();

                if (existingDepartment != null)
                {
                    existingDepartment.DeletionState = true;
                    existingDepartment.ModifiedOn = DateTime.Now;
                    existingDepartment.ModifiedBy = userJwt.UserId;

                    _uow.Repository<Department>().Update(existingDepartment);
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

        public Response UpdateDepartment(DepartmentVM departmentVM, UserJwtVM userJwt)
        {
            Response response;

            try
            {
                var existingDepartment = _uow.Repository<Department>().Get().Where(x => x.DepartmentID == departmentVM.DepartmentID).FirstOrDefault();

                if (existingDepartment != null)
                {
                    existingDepartment.DepartmentID = departmentVM.DepartmentID.Value;
                    existingDepartment.DepartmentName = departmentVM.DepartmentName;
                    existingDepartment.ModifiedOn = DateTime.Now;
                    existingDepartment.ModifiedBy = userJwt.UserId;

                    _uow.Repository<Department>().Update(existingDepartment);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = existingDepartment,
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

