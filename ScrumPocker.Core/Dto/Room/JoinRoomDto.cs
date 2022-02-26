using System.Text.Json.Serialization;

namespace ScrumPocker.Core.Dto.Room
{
    public class JoinRoomDto
    {
        [JsonIgnore]//from token
        public string UserId { get; set; }
        public string RoomId { get; set; }
        public string RoomPassword { get; set; }
    }
}
