using ScrumPocker.Core.Models;
using System.Text.Json.Serialization;

namespace ScrumPocker.Core.Dto.Room
{
    public class CreateRoomDto
    {

        [JsonIgnore]//from token
        public string UserId { get; set; }

        public string Name { get; set; }
        public bool IsPublic { get; set; }
        public int HourExpireIn { get; set; }
        public string Password { get; set; }
        public Voting Voiting { get; set; }
    }
}
