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
using WebApi.DataModel;

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
        public async Task<ActionResult<IEnumerable<UserData>>> GetUsers()
        {
            IMongoCollection<User> query;
            try
            {
                 query = _databaseContext.GetCollection<User>();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            var users = new List<UserData>();
            var user = new UserData();

            try
            {
                await Task.FromResult(IAsyncCursorSourceExtensions
                    .ForEachAsync(query.AsQueryable(), x => users.Add(user.InitFrom(x))));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return users;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserData>> GetUser(Guid guid)
        {
            if (guid == Guid.Empty)
                return BadRequest("Guid must not be empty");

            User query;
            try
            {
                query = await _databaseContext.GetCollection<User>().
                        AsQueryable()
                        .FirstOrDefaultAsync(x => x.UserGuid == guid);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (query == null)
                return BadRequest("No such user");
            var user = new UserData();

            return await Task.FromResult(user.InitFrom(query));
        }

        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] UserData userData)
        {
            if (userData == null)
                return BadRequest("User data must not be empty");

            userData.Guid = Guid.NewGuid().ToString();
            var user = new User();
            try
            {
                await Task.FromResult(_databaseContext.GetCollection<User>()
                    .InsertOneAsync(userData.CopyTo(user)));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok($"User {user.FirstName} {user.LastName} has been created");
        }

        [HttpDelete("{guid:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid guid)
        {
            if (guid == Guid.Empty)
                return BadRequest("Guid must not be empty");

            var filter = Builders<User>.Filter.Where(x => x.UserGuid.Equals(guid));
            if (filter == null)
                return BadRequest("No such user");

            try
            {
                await Task.FromResult(_databaseContext.GetCollection<User>()
                    .DeleteOneAsync(filter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return Ok($"User with guid = {guid} has been deleted");
        }
    }
}
