using Models.Auth;
using Models.UserModel;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using DAL.Auth;
using DAL.UserRepository;
using System.Text;

namespace BLL.AuthServices
{
    public class Authservice : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthRepository _authRepository;
        private readonly IUserRepository _userRepository;
        public Authservice(IConfiguration configuration, IAuthRepository repository, IUserRepository userRepository)
        {
            _configuration = configuration;
            _authRepository = repository;
            _userRepository = userRepository;
        }
        public async Task<string> Login(LoginForm login)
        {
            Guid id =await _authRepository.Login(login);

            UserFull user = await _userRepository.GetById(id);

            string token = GenerateJwtToken(user);

            return token;
        }

        public async Task<string> Register(AddUser user)
        {
            Guid response = await _authRepository.Register(user);

            UserFull userInfo = await _userRepository.GetById(response);

            string token = GenerateJwtToken(userInfo);

            return token;
        }

        #region
        private string GenerateJwtToken(UserFull user)
        {
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
                new Claim(ClaimTypes.Role , user.Role),
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion
    }
}
