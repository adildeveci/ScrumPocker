using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 
using ScrumPocker.Core.Dto.User;
using ScrumPocker.Core.Models;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Services;
using System.Threading.Tasks;

namespace ScrumPocker.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : CustomBaseController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<UserModel>>> CreateUser(CreateUserDto userDto)
        {
            var response = _userService.CreateUser(userDto);
            return ActionResultBase(response);
        }
    }
}
