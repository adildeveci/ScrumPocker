using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumPocker.Core.Models;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrumPocker.API.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VotingController : CustomBaseController
    {
        private readonly IVotingService _votingService;
        public VotingController(IVotingService votingService)
        {
            _votingService = votingService;
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<Voting>>>> GetDefaultVoting()
        {
            var response = _votingService.GetDefaultVoting();
            return ActionResultBase(response);
        }
    }
}
