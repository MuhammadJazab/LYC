using LYC.Helpers;
using LYC.Models;
using Utilities;
using Utilities.Enums;
using ViewModels;
using ViewModels.Branch;
using ViewModels.Room;

namespace LYC.Services
{
    public interface IRoomService
    {
        Response AddNewRoom(RoomVM roomVM, UserJwtVM userJwtVM);
        Response DeleteRoom(long roomId, UserJwtVM userJwtVM);
        Response GetRooms();
        Response UpdateRoom(RoomVM roomVM,UserJwtVM userJwtVM);
    }

    class RoomService : IRoomService
    {
        private readonly IUnitOfWork _uow;

        public RoomService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public Response AddNewRoom(RoomVM roomVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var roomsInBranch = _uow.Repository<Room>().GetAll().Where(x => x.BranchId == roomVM.BranchId);
                var existingRoom = roomsInBranch.Where(x =>x.RoomName == roomVM.RoomName && x.RoomNumber == roomVM.RoomNumber).FirstOrDefault();

                if (existingRoom == null)
                {
                    Room room = new Room()
                    {
                        BranchId = roomVM.BranchId,
                        RoomName = roomVM.RoomName,
                        RoomNumber = roomVM.RoomNumber,
                        RoomStatus = roomVM.RoomStatus,
                        RoomType = roomVM.RoomType,
                        CreatedBy = userJwtVM.UserId,
                        CreatedOn = DateTime.Now,
                        AccomodationChoice = roomVM.AccomodationChoice,
                        DeleteState = false
                    };

                    _uow.Repository<Room>().Add(room);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = room,
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

        public Response DeleteRoom(long roomId, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingRoom = _uow.Repository<Room>().Get().Where(x => x.RoomId == roomId && x.DeleteState == false).FirstOrDefault();

                if (existingRoom != null)
                {
                    existingRoom.DeleteState = true;
                    existingRoom.ModifiedOn = DateTime.Now;
                    existingRoom.ModifiedBy = userJwtVM.UserId;

                    _uow.Repository<Room>().Update(existingRoom);
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

        public Response GetRooms()
        {
            Response response;

            try
            {
                var roomList = _uow.Repository<Room>().GetAll().OrderByDescending(x=>x.CreatedOn);
                var branches = roomList.Where(x => x.DeleteState == false).ToList();

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

        public Response UpdateRoom(RoomVM roomVM, UserJwtVM userJwtVM)
        {
            Response response;

            try
            {
                var existingRoom = _uow.Repository<Room>().Get().Where(x => x.RoomId == roomVM.RoomId).FirstOrDefault();

                if (existingRoom != null)
                {
                    existingRoom.BranchId = roomVM.BranchId;
                    existingRoom.RoomName = roomVM.RoomName;
                    existingRoom.RoomNumber = roomVM.RoomNumber;
                    existingRoom.RoomStatus = roomVM.RoomStatus;
                    existingRoom.RoomType = roomVM.RoomType;
                    existingRoom.ModifiedBy = userJwtVM.UserId;
                    existingRoom.ModifiedOn = DateTime.Now;
                    existingRoom.AccomodationChoice = roomVM.AccomodationChoice;

                    _uow.Repository<Room>().Update(existingRoom);
                    _uow.Save();

                    response = new Response()
                    {
                        ResultData = existingRoom,
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
                        Message = $"{nameof(Room)} {ResponseMessages.DataNotFound}"
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

