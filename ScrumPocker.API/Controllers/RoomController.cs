using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumPocker.Core.Dto.Room;
using ScrumPocker.Core.Models;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Services;
using System.Collections.Generic;
using System.Linq;
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

        [HttpPost]
        [Authorize(Roles = Role.User + "," + Role.UnregisteredUser)]
        public async Task<ActionResult<BaseResponse<Room>>> CreateRoom([FromBody] CreateRoomDto roomDto)
        {
            //CreatedUserId from token
            roomDto.CreatedUserId = base.User.Identities.First().Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            var response = _roomService.CreateRoom(roomDto);
            return ActionResultBase(response);
        }


        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<Room>>>> GetRooms()
        {
            var response = _roomService.GetRooms();
            return ActionResultBase(response);
        }

        [HttpPost]
        [Authorize(Roles = Role.User)]
        public async Task<ActionResult<BaseResponse>> JoinRoom([FromBody] JoinRoomDto request)
        {
            var response = _roomService.JoinRoom(request);
            return ActionResultBase(response);
        }
    }
}
