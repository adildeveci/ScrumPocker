using System;

namespace ScrumPocker.Core.Dto.Room
{
    public class JoinRoomDto
    {
        public Guid RoomGuid { get; set; }
        public string RoomPassword { get; set; }
    }
}
