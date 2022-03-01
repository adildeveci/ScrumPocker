using System.Text.Json.Serialization;

namespace ScrumPocker.Core.Dto.Voting
{
    public class RevealCardRequestDto
    { 
        [JsonIgnore]//from token
        public string UserId { get; set; }
        public string RoomId { get; set; } 
    }
}
