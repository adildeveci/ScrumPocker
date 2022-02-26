using System.Text.Json.Serialization;

namespace ScrumPocker.Core.Dto.Voting
{
    public class VoteRequestDto
    { 
        [JsonIgnore]//from token
        public string UserId { get; set; }
        public string RoomId { get; set; }
        public string Value { get; set; }
    }
}
