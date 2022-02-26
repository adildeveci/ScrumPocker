using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ScrumPocker.Core.Configuration;
using ScrumPocker.Core.Constants;
using ScrumPocker.Core.Dto.Token;
using ScrumPocker.Core.Extensions;
using ScrumPocker.Core.Helpers;
using ScrumPocker.Core.Models;
using ScrumPocker.Core.Models.BaseResponse;
using ScrumPocker.Core.StaticDb;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ScrumPocker.Services
{
    public interface IAuthService
    {
        BaseResponse<ClientTokenDto> CreateClientToken(ClientLoginDto clientLoginDto);
        BaseResponse<TokenDto> CreateUserToken(LoginDto loginDto);
        BaseResponse<TokenDto> CreateUnregisteredUserToken(LoginForUnregisteredDto request);
        BaseResponse<TokenDto> CreateUserTokenByRefreshToken(string refreshToken);
    }
    public class AuthService : IAuthService
    {
        private readonly List<Client> _clients;
        private readonly CustomTokenOption _tokenOption;
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration, IOptions<List<Client>> optionsClient, IOptions<CustomTokenOption> optionCustomToken)
        {
            _configuration = configuration;
            _clients = optionsClient.Value;
            _tokenOption = optionCustomToken.Value;
        }
        #region Client Token
        public BaseResponse<ClientTokenDto> CreateClientToken(ClientLoginDto clientLoginDto)
        {
            var client = _clients.SingleOrDefault(x => x.Id == clientLoginDto.ClientId && x.Secret == clientLoginDto.ClientSecret);

            if (client == null)
                return BaseResponse<ClientTokenDto>.Fail("ClientId or ClientSecret not found", 404);

            var token = CreateClientToken(client);
            return BaseResponse<ClientTokenDto>.Success(token);
        }

        private ClientTokenDto CreateClientToken(Client client)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            var securityKey = CustomTokenAuth.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                // issuer: _tokenOption.Issuer, 
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaimsByClient(client),
                 signingCredentials: signingCredentials);
            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,

                AccessTokenExpiration = accessTokenExpiration,
            };

            return tokenDto;
        }

        private static IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString())
            };

            return claims;
        }
        #endregion

        #region User Token

        public BaseResponse<TokenDto> CreateUserToken(LoginDto loginDto)
        {
            if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));

            var user = StaticDbContext.Users.FirstOrDefault(x => x.Email.Equals(loginDto.Email));//TODO: password to hashPassword
            if (user == null)
                return BaseResponse<TokenDto>.Fail("Email or Password is wrong");

            if (!Hashing.CheckHashSHA512(loginDto.Password, user.PasswordHash))
                return BaseResponse<TokenDto>.Fail("Email or Password is wrong");

            var token = GenerateUserToken(user);

            var userRefreshToken = StaticDbContext.UserRefreshTokens.FirstOrDefault(x => x.UserId == user.Id);
            if (userRefreshToken == null)
            {
                StaticDbContext.UserRefreshTokens.Add(new UserRefreshToken { UserId = user.Id, RefreshToken = token.RefreshToken, RefreshTokenExpiration = token.RefreshTokenExpiration });
            }
            else
            {
                userRefreshToken.RefreshToken = token.RefreshToken;
                userRefreshToken.RefreshTokenExpiration = token.RefreshTokenExpiration;
            }

            return BaseResponse<TokenDto>.Success(token);
        }

        public BaseResponse<TokenDto> CreateUnregisteredUserToken(LoginForUnregisteredDto request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var user = new UserModel
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                SurName = request.Surname,
                Role = Role.UnregisteredUser
            };
            StaticDbContext.Users.Add(user);//refresh token alabilmek icin simdilik ekledik sonradan ayristirilabilir

            var token = GenerateUserToken(user);
            StaticDbContext.UserRefreshTokens.Add(new UserRefreshToken { UserId = user.Id, RefreshToken = token.RefreshToken, RefreshTokenExpiration = token.RefreshTokenExpiration });

            return BaseResponse<TokenDto>.Success(token);
        }

        public BaseResponse<TokenDto> CreateUserTokenByRefreshToken(string refreshToken)
        {
            var existRefreshToken = StaticDbContext.UserRefreshTokens.FirstOrDefault(x => x.RefreshToken == refreshToken);

            if (existRefreshToken == null)
                return BaseResponse<TokenDto>.Fail("Refresh token not found", 404);

            var user = StaticDbContext.Users.FirstOrDefault(x => x.Id == existRefreshToken.UserId);

            if (user == null)
                return BaseResponse<TokenDto>.Fail("User Id not found", 404);

            var tokenDto = GenerateUserToken(user);

            existRefreshToken.RefreshToken = tokenDto.RefreshToken;
            existRefreshToken.RefreshTokenExpiration = tokenDto.RefreshTokenExpiration;

            return BaseResponse<TokenDto>.Success(tokenDto);
        }
        private TokenDto GenerateUserToken(UserModel user)
        {
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            var securityKey = CustomTokenAuth.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                //issuer: _tokenOption.Issuer,
                expires: accessTokenExpiration,
                 notBefore: DateTime.Now,
                 claims: GetClaimsByUser(user),
                 signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = GenerateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration
            };

            return tokenDto;
        }
        private string GenerateRefreshToken()
        {
            var numberByte = new Byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }
        private IEnumerable<Claim> GetClaimsByUser(UserModel user)
        {
            var claims = new List<Claim> {
            new Claim(ClaimTypes.NameIdentifier,user.Id),
            new Claim(JwtRegisteredClaimNames.Email, user.Email??string.Empty),
            new Claim(ClaimTypes.Name,user.Name),
            new Claim(ClaimTypes.Surname,user.SurName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, user.Role)
            };

            return claims;
        }

        #endregion
    }
}
