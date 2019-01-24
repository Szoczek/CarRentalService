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
            IMongoQueryable<User> query;
            try
            {
                query = _databaseContext.GetCollection<User>().AsQueryable();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            List<UserData> users = new List<UserData>();
            try
            {
                //await Task.FromResult(IAsyncCursorSourceExtensions
                //   .ForEachAsync(query, x => users.Add(new UserData().InitFrom(x))));
                foreach (var user in query)
                {
                    users.Add(new UserData().InitFrom(user));
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            return await Task.FromResult(users);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<UserData>> GetUser(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id must not be empty");

            User query;
            try
            {
                query = await _databaseContext.GetCollection<User>()
                        .AsQueryable()
                        .FirstOrDefaultAsync(x => x.Id.Equals(id));
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

            userData.Id = Guid.NewGuid();
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

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id must not be empty");

            var filter = Builders<User>.Filter.Where(x => x.Id.Equals(id));
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

            return Ok($"User with id = {id} has been deleted");
        }
    }
}
