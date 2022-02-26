using System;

namespace ScrumPocker.Core.Dto.Room
{
    public class RoomSummaryDto
    {
        public string Id { get; set; } 
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpriDate { get; set; }
        public bool IsPublic { get; set; }
    }
}
