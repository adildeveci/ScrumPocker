﻿using System;
using System.Text.Json.Serialization;

namespace ScrumPocker.Core.Dto.Room
{
    public class JoinRoomDto
    {
        [JsonIgnore]//from token
        public string UserId { get; set; }
        public Guid RoomGuid { get; set; }
        public string RoomPassword { get; set; }
    }
}
