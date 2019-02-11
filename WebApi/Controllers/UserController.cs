using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WebApi.DataModel;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _databaseContext = new DatabaseContext();
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<User> LoginUser([FromBody]Credentials credentials)
        {
            if (!credentials.IsValid())
                return BadRequest(new { message = "login or password is invalid" });

            try
            {
                var user = _userService.Authenticate(credentials);

                if (user == null)
                    return BadRequest(new { message = "login or password is incorrect" });
                else
                    return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPut("{login}")]
        public async Task<ActionResult<User>> PutUser(string login, [FromBody]User user)
        {
            if (!user.IsValid())
                return BadRequest(new { message = "user informations are invalid" });

            try
            {
                await _databaseContext.GetCollection<User>()
                    .FindOneAndReplaceAsync(Builders<User>.Filter.Where(x => x.Login.Equals(user.Login)), user);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{login}")]
        public async Task<ActionResult<User>> GetUser(string login)
        {
            try
            {
                var user = await _databaseContext.GetCollection<User>()
                        .AsQueryable()
                        .FirstOrDefaultAsync(x => x.Login.Equals(login));
                if (user == null)
                    return BadRequest(new { message = "User not found" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult> PostUser([FromBody] User user)
        {
            if (!user.IsValid())
                return BadRequest(new { message = "User informations are invalid" });

            try
            {
                var users = _databaseContext.GetCollection<User>();

                if (users.AsQueryable().AnyAsync(x => x.Login == user.Login).Result)
                    return BadRequest(new { message = "Login already taken" });

                user.Id = Guid.NewGuid();
                await _databaseContext.GetCollection<User>()
                  .InsertOneAsync(user);
                return Ok(new { message = $"{user.FirstName} {user.LastName} created" });
            }
            catch (Exception e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpDelete("{login}")]
        public async Task<ActionResult> DeleteUser(string login)
        {
            try
            {
                await _databaseContext.GetCollection<User>()
                   .DeleteOneAsync(Builders<User>.Filter.Where(x => x.Login.Equals(login)));
                return Ok(new { message = $"User {login} deleted!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
