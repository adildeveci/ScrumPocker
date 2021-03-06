using System.Text.Json.Serialization;

namespace ScrumPocker.Core.Dto.Room
{
    public class DeleteRoomDto
    {
        [JsonIgnore]//from token
        public string UserId { get; set; }
        public string RoomId { get; set; }
    }
}
