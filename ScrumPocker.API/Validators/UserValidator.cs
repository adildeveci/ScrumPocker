using FluentValidation;
using ScrumPocker.Core.Dto.User;

namespace ScrumPocker.API.Validators
{
    public class UserValidator : AbstractValidator<CreateUserDto>
    { 
            public UserValidator()
            {
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty().Must(x => x.Length >= 6 && x.Length <= 20); 
            } 
    }
}
