using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumPocker.Core.Dto.Token;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Services;
using System.Threading.Tasks;

namespace ScrumPocker.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : CustomBaseController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<BaseResponse<ClientTokenDto>>> CreateClientToken(ClientLoginDto request)
        {
            var response = _authService.CreateClientToken(request);
            return ActionResultBase(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<TokenDto>>> CreateUserToken([FromBody] LoginDto request)
        {
            var response = _authService.CreateUserToken(request);
            return ActionResultBase(response);
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<TokenDto>>> CreateUnregisteredUserToken([FromBody] LoginForUnregisteredDto request)
        {
            var response = _authService.CreateUnregisteredUserToken(request);
            return ActionResultBase(response);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<BaseResponse<TokenDto>>> CreateUserTokenByRefreshToken([FromBody] RefreshTokenDto request)
        {
            var response = _authService.CreateUserTokenByRefreshToken(request.RefreshToken);
            return ActionResultBase(response);
        }

    }
}
