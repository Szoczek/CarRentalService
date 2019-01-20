using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using MongoDB.Driver;
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
            var query = _databaseContext.GetCollection<User>();
            List<UserData> users = new List<UserData>();
            UserData user = new UserData();

            await Task.FromResult(query.AsQueryable().ForEachAsync(x => users.Add(user.InitFrom(x))));

            return users;
        }

        [HttpGet("{id: int}")]
        public async Task<ActionResult<UserData>> GetUser(int id)
        {
            var query = _databaseContext.GetCollection<User>().
                AsQueryable()
                .FirstOrDefault(x => x.Oid == id);

            UserData user = new UserData();

            return await Task.FromResult(user.InitFrom(query));
        }

        [HttpPost]
        public async void PostUser([FromBody] UserData userData)
        {
            userData.Guid = Guid.NewGuid();
            User user = new User();
            await Task.FromResult(_databaseContext.GetCollection<User>().InsertOneAsync(userData.CopyTo(user)));
        }

        [HttpDelete("{id: int}")]
        public async void DeleteUser(int id)
        {
            var filter = Builders<User>.Filter.Where(x => x.Oid == id);
            await Task.FromResult(_databaseContext.GetCollection<User>().DeleteOneAsync(filter));
        }
    }
}
