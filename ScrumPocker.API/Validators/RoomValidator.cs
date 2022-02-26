using FluentValidation;
using ScrumPocker.Core.Constants;
using ScrumPocker.Core.Dto.Room;

namespace ScrumPocker.API.Validators
{
    public class RoomValidator : AbstractValidator<CreateRoomDto>
    {
        public RoomValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.HourExpireIn).GreaterThanOrEqualTo(GeneralConstant.MinRoomHourExpireIn).LessThanOrEqualTo(GeneralConstant.MaxRoomHourExpireIn);
            RuleFor(x => x.Password).Must(x => x.Length >= 6 && x.Length <= 20).When(x => !x.IsPublic);//private odalarda sifre zorunlu
            RuleFor(x => x.VotingDefinition).NotNull();
            RuleFor(x => x.VotingDefinition.Name).NotEmpty();
            RuleFor(x => x.VotingDefinition.Values).Must(x => x.Count >= 2).WithMessage("VotingDefinition Values must have min 2 item");//en az 2 puan olmali
        }
    }
}
