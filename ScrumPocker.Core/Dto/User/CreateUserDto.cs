using System.ComponentModel.DataAnnotations;

namespace ScrumPocker.Core.Dto.User
{
    public class CreateUserDto
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
