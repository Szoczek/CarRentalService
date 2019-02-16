using Infrastructure.Database;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.DataModel;
using WebApi.Helpers;

namespace WebApi.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(Credentials credentials);
    }

    public class UserService : IUserService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _databaseContext = new DatabaseContext();
            _appSettings = appSettings.Value;
        }

        public async  Task<User> Authenticate(Credentials credentials)
        {
            var user = _databaseContext.GetCollection<User>().AsQueryable()
                .FirstOrDefaultAsync(x => x.Login.Equals(credentials.Login) && x.Password.Equals(credentials.Password)).Result;

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            await _databaseContext.GetCollection<User>()
                .FindOneAndReplaceAsync(Builders<User>.Filter
                .Where(x => x.Login == credentials.Login && x.Password == credentials.Password), user);

            return user;
        }
    }
}
