using System.Collections.Generic;
using LYC.Helpers;
using LYC.Models;
using Utilities;
using Utilities.Enums;
using ViewModels;
using ViewModels.Room;
using ViewModels.Service;

namespace LYC.service
{
    public interface IServicesService
    {
        Response GetServices();
        Response AddNewService(ServiceVM serviceVM, UserJwtVM userJwtVM);
        Response DeleteService(long serviceId, UserJwtVM userJwtVM);
        Response UpdateService(ServiceVM serviceVM, UserJwtVM userJwtVM);
    }

    class ServicesService : IServicesService
    {
        private readonly IUnitOfWork _uow;

        public ServicesService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Response AddNewService(ServiceVM serviceVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingService = _uow.Repository<Service>().Get().Where(x => string.Equals(x.ServiceName, serviceVM.ServiceName)).FirstOrDefault();

                if (existingService == null)
                {
                    Service service = new Service()
                    {
                        ServiceName = serviceVM.ServiceName,
                        BranchId = serviceVM.BranchId,
                        StartingDateTime = serviceVM.StartingDateTime,
                        EndingDateTime = serviceVM.EndingDateTime,
                        MaxOccupants = serviceVM.MaxOccupants,
                        ServiceCost = serviceVM.ServiceCost,
                        CreatedBy = userJwtVM.UserId,                        
                        CreatedOn = DateTime.Now,
                        DeletionState = false
                    };

                    _uow.Repository<Service>().Add(service);
                    _uow.Save();

                    foreach (var room in serviceVM.Rooms)
                    {
                        RoomServiceAssociation roomServiceAssociation = new RoomServiceAssociation
                        {
                            ServiceId = service.ServiceId,
                            RoomId = room.RoomId,
                            BranchId = serviceVM.BranchId,
                            CreatedBy = userJwtVM.UserId,
                            CreatedOn = DateTime.Now
                        };

                        _uow.Repository<RoomServiceAssociation>().Add(roomServiceAssociation);
                    }

                    foreach (var day in serviceVM.Days)
                    {
                        ServiceDayAssociation serviceDayAssociation = new ServiceDayAssociation
                        {
                            DayId = day,
                            ServiceId = service.ServiceId,
                            CreatedBy = userJwtVM.UserId,
                            CreatedOn = DateTime.Now
                        };

                        _uow.Repository<ServiceDayAssociation>().Add(serviceDayAssociation);
                    }

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
                        Message = $"{nameof(Service)} {ResponseMessages.AlreadyExist}"
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

        public Response DeleteService(long serviceId, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingService = _uow.Repository<Service>().Get().Where(x => x.ServiceId == serviceId && x.DeletionState == false).FirstOrDefault();

                if (existingService != null)
                {
                    existingService.DeletionState = true;
                    existingService.ModifiedOn = DateTime.Now;
                    existingService.ModifiedBy = userJwtVM.UserId;

                    _uow.Repository<Service>().Update(existingService);

                    var daysOfService = _uow.Repository<ServiceDayAssociation>().GetAll().Where(x => x.ServiceId == serviceId).ToList();

                    foreach (var serviceOnDay in daysOfService)
                    {
                        serviceOnDay.DeletionState = true;
                        serviceOnDay.ModifiedOn = DateTime.Now;
                        serviceOnDay.ModifiedBy = userJwtVM.UserId;

                        _uow.Repository<ServiceDayAssociation>().Update(serviceOnDay);
                    }

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

        public Response GetServices()
        {
            Response response;

            try
            {
                List<ServiceVM> serviceVMs = new List<ServiceVM>();
                List<RoomVM> roomVMs = new List<RoomVM>();

                var services = _uow.Repository<Service>().GetAll();
                var branches = _uow.Repository<Branch>().GetAll();
                var rooms = _uow.Repository<Room>().GetAll();
                var days = _uow.Repository<ServiceDayAssociation>().GetAll();

                var serviceList = services.Where(x => x.DeletionState == false).OrderByDescending(x => x.CreatedOn).ToList();

                if (serviceList?.Count > 0)
                {
                    foreach (var item in serviceList)
                    {
                        var branch = branches.Where(x => x.BranchId == item.BranchId).FirstOrDefault();

                        if (branch != null)
                        {
                            string roomName = string.Empty;

                            var roomList = rooms.Where(x => x.BranchId == branch.BranchId && x.AccomodationChoice == EnumAccomodationChoice.Facility).OrderByDescending(x => x.CreatedOn).ToList();

                            if (roomList != null)
                            {
                                foreach (var roomItem in roomList)
                                {
                                    RoomVM roomVM = new RoomVM
                                    {
                                        RoomId = roomItem.RoomId,
                                        RoomName = roomItem.RoomName
                                    };

                                    roomVMs.Add(roomVM);
                                    roomName += $"{roomItem.RoomName}, " ;
                                }
                            }

                            var dayList = days.Where(x => x.ServiceId == item.ServiceId && x.DeletionState == false).Select(x=>x.DayId).ToList();

                            if (dayList.Count() > 0)
                            {
                                ServiceVM serviceVM = new ServiceVM
                                {
                                    BranchId = branch.BranchId,
                                    ServiceId = item.ServiceId,
                                    BranchName = branch.BranchName,
                                    ServiceName = item.ServiceName,
                                    ServiceStatus= item.ServiceStatus,
                                    ServiceCost = item.ServiceCost,
                                    StartingDateTime = item.StartingDateTime,
                                    EndingDateTime = item.EndingDateTime,
                                    MaxOccupants = item.MaxOccupants,
                                    Days = dayList,
                                    Rooms = roomVMs,
                                    RoomNames = roomName
                                };

                                serviceVMs.Add(serviceVM);
                            }
                        }
                    }

                    response = new Response()
                    {
                        ResultData = serviceVMs,
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

        public Response UpdateService(ServiceVM roomVM, UserJwtVM userJwtVM)
        {
            throw new NotImplementedException();
        }
    }
}

