using System;

namespace ScrumPocker.Core.Models
{
    public class UserRefreshToken
    {
        public string UserId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }
    }
}