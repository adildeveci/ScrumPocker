using Microsoft.AspNetCore.Mvc;
using ScrumPocker.Core.Models.BaseResponse;

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
    }
}
