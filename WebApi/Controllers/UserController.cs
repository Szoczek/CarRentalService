using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace WebApi.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public UserController()
        {
            _databaseContext = new DatabaseContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            try
            {
                return await Task.FromResult(_databaseContext.GetCollection<User>()
                    .AsQueryable()
                    .ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/user/login")]
        [HttpPost]
        public ActionResult<string> LoginUser([FromBody]Credentials credentials)
        {
            if (string.IsNullOrWhiteSpace(credentials.Login) || string.IsNullOrWhiteSpace(credentials.Password))
                return BadRequest("Login and password cannot be empty!");

            User user;
            try
            {
                user = _databaseContext.GetCollection<User>().AsQueryable()
                    .FirstOrDefaultAsync(x => x.Login == credentials.Login && x.Password == credentials.Password).Result;

                if (user == null)
                    return BadRequest("Wrong login or password");
                else
                    return user.Login;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("login:required")]
        public async Task<ActionResult> PutUser(string login, [FromBody]User user)
        {
            if (login == string.Empty || user == null)
                return login == string.Empty && user == null
                    ? BadRequest("Login must not be empty and user data must not be empty")
                    : login == string.Empty ? BadRequest("Login must not be empty") : BadRequest("User data must not be empty");
            try
            {
                await _databaseContext.GetCollection<User>()
                    .FindOneAndReplaceAsync(Builders<User>.Filter.Where(x => x.Login.Equals(login)), user);
                return Ok($"User {login} modified");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUser(string login)
        {
            if (login == string.Empty)
                return BadRequest("Login must not be empty");
            try
            {
                return await _databaseContext.GetCollection<User>()
                       .AsQueryable()
                       .FirstOrDefaultAsync(x => x.Login.Equals(login));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostUser([FromBody] User user)
        {
            if (!user.IsValid())
                return BadRequest("User information invalid");

            try
            {
                var users = _databaseContext.GetCollection<User>();

                if (users.AsQueryable().AnyAsync(x => x.Login == user.Login).Result)
                    return BadRequest("Login already taken!");

                user.Id = Guid.NewGuid();
                await _databaseContext.GetCollection<User>()
                  .InsertOneAsync(user);
                return Ok($"User {user.FirstName} {user.LastName} created");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("login:required")]
        public async Task<ActionResult> DeleteUser(string login)
        {
            if (login == string.Empty)
                return BadRequest("Login must not be empty");

            try
            {
                await _databaseContext.GetCollection<User>()
                   .DeleteOneAsync(Builders<User>.Filter.Where(x => x.Login.Equals(login)));
                return Ok($"User {login} deleted!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
