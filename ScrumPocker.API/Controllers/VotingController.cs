using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScrumPocker.Core.Constants;
using ScrumPocker.Core.Dto.Voting;
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
        public async Task<ActionResult<BaseResponse<List<VotingDefinition>>>> GetVotingDefinitions()
        {
            var response = _votingService.GetVotingDefinitions();
            return ActionResultBase(response);
        }

        [HttpPut]
        [Authorize(Roles = RoleCombination.LoggedUserRoles)]
        public async Task<ActionResult<BaseResponse>> Vote([FromBody] VoteRequestDto request)
        {
            request.UserId = GetCurrentUserId();

            var response = _votingService.Vote(request);
            return ActionResultBase(response);
        }

        [HttpPost]
        [Authorize(Roles = RoleCombination.LoggedUserRoles)]
        public async Task<ActionResult<BaseResponse>> RevealCard([FromBody] RevealCardRequestDto request)
        {
            request.UserId = GetCurrentUserId();

            var response = _votingService.RevealCard(request);
            return ActionResultBase(response);
        }

        [HttpPost]
        [Authorize(Roles = RoleCombination.LoggedUserRoles)]
        public async Task<ActionResult<BaseResponse>> StartNewVoting([FromBody] StartNewVotingRequestDto request)
        {
            request.UserId = GetCurrentUserId();

            var response = _votingService.StartNewVoting(request);
            return ActionResultBase(response);
        }
    }
}
