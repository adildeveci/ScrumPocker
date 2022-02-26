using System;
using System.Text.Json.Serialization;

namespace ScrumPocker.Core.Dto.Room
{
    public class LeaveRoomDto
    {
        [JsonIgnore]//from token
        public string UserId { get; set; }
        public Guid RoomGuid { get; set; }
    }
}
