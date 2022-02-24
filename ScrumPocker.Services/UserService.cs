
using ScrumPocker.Core.Dto.User;
using ScrumPocker.Core.Models;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Core.StaticDb;
using System;
using System.Linq;

namespace ScrumPocker.Services
{
    public interface IUserService
    {
        BaseResponse<UserModel> CreateUser(CreateUserDto userDto);
    }
    public class UserService: IUserService
    {
        public BaseResponse<UserModel> CreateUser(CreateUserDto userDto)
        {
            if (StaticDbContext.Users.Any(x => x.Email == userDto.Email))
                return BaseResponse<UserModel>.Fail("Girilen mail adresi zaten sistemimizde kayıtlı");

            var user = new UserModel()
            {
                Id = Guid.NewGuid().ToString(),
                Email = userDto.Email,
                Name = userDto.Name,
                SurName = userDto.SurName,
                PasswordHash = userDto.Password,//TODO: hash'li halini kaydet 
                Role=Role.User
            };
            StaticDbContext.Users.Add(user);
            return BaseResponse<UserModel>.Success(user);
        }
    }
}
