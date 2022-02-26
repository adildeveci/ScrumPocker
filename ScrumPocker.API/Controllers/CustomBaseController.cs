using Microsoft.AspNetCore.Mvc;
using ScrumPocker.Core.Models.BaseResponse;
using System;
using System.Linq;

namespace ScrumPocker.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public ActionResult<BaseResponse> ActionResultBase(BaseResponse response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        [NonAction]
        public ActionResult<BaseResponse<T>> ActionResultBase<T>(BaseResponse<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }

        /// <summary>
        /// istegin geldigi token uzerindeki NameIdentifier bilgisini doner
        /// </summary>
        /// <exception cref="NullReferenceException">This exception is thrown if not found ClaimTypes.NameIdentifier</exception>
        /// <returns>ClaimTypes.NameIdentifier</returns>
        [NonAction]
        public string GetCurrentUserId()
        {
            return base.User.Identities.First().Claims.First(x => x.Type == System.Security.Claims.ClaimTypes.NameIdentifier).Value;
        }
    }
}
