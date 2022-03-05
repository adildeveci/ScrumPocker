using ScrumPocker.Core.Models;
using System;
using System.Collections.Generic;

namespace ScrumPocker.Core.Dto.Room
{
    public class RoomDetailDto
    {
        public RoomDetailDto()
        {
            UserAndVotes = new List<UserAndVoteDto>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpriDate { get; set; }
        public bool IsPublic { get; set; }
        public bool WasRevealed { get; set; }//puanlar gosterildi mi bilgisi
        public VotingDefinition VotingDefinition { get; set; }//simdilik detail icinde donelim, ilerde her response icinde donmeyecek sekilde ayarlayabilriz
        public List<UserAndVoteDto> UserAndVotes { get; set; }
    }
    public class UserAndVoteDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string VoteValue { get; set; }
    }
}
