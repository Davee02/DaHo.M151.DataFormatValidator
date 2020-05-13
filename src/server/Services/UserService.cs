using DaHo.M151.DataFormatValidator.Abstractions;
using DaHo.M151.DataFormatValidator.Abstractions.Helpers;
using DaHo.M151.DataFormatValidator.Models.ServiceModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DaHo.M151.DataFormatValidator.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IUserRepository _userRepository;

        public UserService(IOptions<AppSettings> appSettings, IUserRepository userRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAndPasswordAsync(username, password);

            // return null if user not found
            if (user == null)
            {
                return null;
            }

            var model = new User { Id = user.Id, Password = user.Password, Role = user.Role, Username = user.Username };

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.Id.ToString()),
                    new Claim(ClaimTypes.Role, model.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            model.Token = tokenHandler.WriteToken(token);

            return model.WithoutPassword();
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _userRepository.GetAllAsync();
            var models = users.Select(x => new User { Id = x.Id, Password = x.Password, Role = x.Role, Username = x.Username });

            return models.WithoutPasswords();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            var model = new User { Id = user.Id, Password = user.Password, Role = user.Role, Username = user.Username };

            return model.WithoutPassword();
        }
    }
}
