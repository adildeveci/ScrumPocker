using ScrumPocker.Core.Models;
using System.Collections.Generic;

namespace ScrumPocker.Core.StaticDb
{
    public static class StaticDbContext
    {
        public static List<Room> Rooms { get; set; } = new List<Room>();
        public static List<UserModel> Users { get; set; } = new List<UserModel>();
        public static List<UserRefreshToken> UserRefreshTokens { get; set; } = new List<UserRefreshToken>();

    }
}
