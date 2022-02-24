using Microsoft.AspNetCore.Identity;

namespace ScrumPocker.Core.Models
{
    public class UserModel : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Role { get; set; }
    }
}
