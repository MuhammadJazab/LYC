using LYC.Helpers;
using LYC.Models;
using Utilities;
using Utilities.Enums;
using ViewModels;
using ViewModels.Package;

namespace LYC.Services
{
    public interface IPackageService
    {
        Response AddNewPackage(PackageVM packageVM, UserJwtVM userJwtVM);
        Response DeletePackage(long packageId, UserJwtVM userJwtVM);
        Response GetPackages();
        Response UpdatePackage(PackageVM packageVM, UserJwtVM userJwtVM);
    }

    class PackageService: IPackageService
    {
        private readonly IUnitOfWork _uow;

        public PackageService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Response AddNewPackage(PackageVM packageVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingPackage = _uow.Repository<Package>().Get().Where(x => string.Equals(x.PackageNumber, packageVM.PackageNumber)).FirstOrDefault();

                if (existingPackage == null)
                {
                    Package package = new Package()
                    {
                        PackageNumber = packageVM.PackageNumber,
                        PackageName = packageVM.PackageName,
                        StayPeriodDays = packageVM.StayPeriodDays,
                        PackageType = packageVM.PackageType,
                        PackageCost = packageVM.PackageCost,
                        CreatedBy = userJwtVM.UserId,
                        CreatedOn = DateTime.Now,
                        DeletionState = false
                    };

                    _uow.Repository<Package>().Add(package);
                    _uow.Save();

                    var existingService = _uow.Repository<Service>().Get().Where(x => x.ServiceId == packageVM.ServiceId).FirstOrDefault();

                    if (existingService != null)
                    {
                        existingService.PackageId = package.PackageId;
                        existingService.ModifiedBy = userJwtVM.UserId;
                        existingService.ModifiedOn = DateTime.Now;

                        _uow.Repository<Service>().Update(existingService);
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
                            Status = ResponseStatus.PartialSuccessfull,
                            Message = ResponseMessages.SuccessfullWithError
                        };
                    }
                }
                else
                {
                    response = new Response()
                    {
                        ResultData = null,
                        Status = ResponseStatus.Restrected,
                        Message = $"{nameof(Package)} {ResponseMessages.AlreadyExist}"
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

        public Response DeletePackage(long packageId, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingPackage = _uow.Repository<Package>().Get().Where(x => x.PackageId == packageId && x.DeletionState == false).FirstOrDefault();

                if (existingPackage != null)
                {
                    existingPackage.DeletionState = true;
                    existingPackage.ModifiedOn = DateTime.Now;
                    existingPackage.ModifiedBy = userJwtVM.UserId;

                    _uow.Repository<Package>().Update(existingPackage);
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

        public Response GetPackages()
        {
            Response response;

            try
            {
                List<PackageVM> packageVMs = new List<PackageVM>();

                var packages = _uow.Repository<Package>().GetAll();
                var services = _uow.Repository<Service>().GetAll();

                var packageList = packages.Where(x => x.DeletionState == false).OrderByDescending(x => x.CreatedOn).ToList();

                if (packageList?.Count > 0)
                {
                    foreach (var package in packageList)
                    {
                        var service = services.Where(x => x.PackageId == package.PackageId).FirstOrDefault();

                        packageVMs.Add(new PackageVM
                        {
                            PackageCost = package.PackageCost,
                            PackageId = package.PackageId,
                            PackageName = package.PackageName,
                            PackageNumber = package.PackageNumber,
                            PackageType = package.PackageType,
                            StayPeriodDays = package.StayPeriodDays,
                            ServiceId = service?.ServiceId
                        });
                    }

                    response = new Response()
                    {
                        ResultData = packageVMs,
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

        public Response UpdatePackage(PackageVM packageVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingPackage = _uow.Repository<Package>().Get().Where(x => string.Equals(x.PackageNumber, packageVM.PackageNumber)).FirstOrDefault();

                if (existingPackage != null)
                {
                    existingPackage.PackageNumber = packageVM.PackageNumber;
                    existingPackage.PackageName = packageVM.PackageName;
                    existingPackage.PackageCost = packageVM.PackageCost;
                    existingPackage.ModifiedBy = userJwtVM.UserId;
                    existingPackage.ModifiedOn = DateTime.Now;

                    _uow.Repository<Package>().Update(existingPackage);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = existingPackage,
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
                        Message = $"{nameof(Package)} {ResponseMessages.DataNotFound}"
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

