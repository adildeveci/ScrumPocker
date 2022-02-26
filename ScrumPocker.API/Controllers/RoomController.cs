using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumPocker.Core.Constants;
using ScrumPocker.Core.Dto.Room;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrumPocker.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RoomController : CustomBaseController
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<RoomSummaryDto>>>> GetRooms()
        {
            var response = _roomService.GetRooms();
            return ActionResultBase(response);
        }

        [HttpPost]
        [Authorize(Roles = RoleCombination.LoggedUserRoles)]
        public async Task<ActionResult<BaseResponse<RoomSummaryDto>>> CreateRoom([FromBody] CreateRoomDto request)
        {
            request.UserId = GetCurrentUserId();

            var response = _roomService.CreateRoom(request);
            return ActionResultBase(response);
        }

        [HttpPut]
        [Authorize(Roles = RoleCombination.LoggedUserRoles)]
        public async Task<ActionResult<BaseResponse>> JoinRoom([FromBody] JoinRoomDto request)
        {
            request.UserId = GetCurrentUserId();

            var response = _roomService.JoinRoom(request);
            return ActionResultBase(response);
        }

        [HttpPost]
        [Authorize(Roles = RoleCombination.LoggedUserRoles)]
        public async Task<ActionResult<BaseResponse>> LeaveRoom([FromBody] LeaveRoomDto request)
        {
            request.UserId = GetCurrentUserId();

            var response = _roomService.LeaveRoom(request);
            return ActionResultBase(response);
        }

        [HttpDelete]
        [Authorize(Roles = RoleCombination.LoggedUserRoles)]
        public async Task<ActionResult<BaseResponse>> DeleteRoom([FromBody] DeleteRoomDto request)
        {
            request.UserId = GetCurrentUserId();

            var response = _roomService.DeleteRoom(request);
            return ActionResultBase(response);
        }
    }
}
