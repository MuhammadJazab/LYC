using LYC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Utilities.Constants;
using Utilities;
using ViewModels.Room;

namespace LYC.Controllers
{
    [ApiController]
    [Produces("Application/json")]
    public class RoomController : BaseController
    {
        private const string api = "api/Room/";

        private readonly ILogger<RoomController> _logger;
        private readonly IRoomService _roomService;

        public RoomController(ILogger<RoomController> logger, IRoomService roomService)
        {
            _logger = logger;
            _roomService = roomService;
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpGet($"{api}{nameof(GetRooms)}")]
        public Response GetRooms()
        {
            _logger.LogInformation($"A request is made to {nameof(GetRooms)} at {DateTime.UtcNow.ToLongTimeString()}");

            return _roomService.GetRooms();
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpPost($"{api}{nameof(AddNewRoom)}")]
        public Response AddNewRoom([FromBody] RoomVM roomVM)
        {
            _logger.LogInformation($"A request is made to {nameof(AddNewRoom)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _roomService.AddNewRoom(roomVM, userJwt);
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpPost($"{api}{nameof(UpdateRoom)}")]
        public Response UpdateRoom([FromBody] RoomVM roomVM)
        {
            _logger.LogInformation($"A request is made to {nameof(UpdateRoom)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _roomService.UpdateRoom(roomVM, userJwt);
        }

        [Authorize(Roles = $"{UserRoles.SuperAdmin},{UserRoles.Admin}")]
        [HttpGet($"{api}{nameof(DeleteRoom)}")]
        public Response DeleteRoom(long roomId)
        {
            _logger.LogInformation($"A request is made to {nameof(DeleteRoom)} at {DateTime.UtcNow.ToLongTimeString()}");

            var userJwt = GetAuthorizationHeader();
            return _roomService.DeleteRoom(roomId, userJwt);
        }
    }
}

