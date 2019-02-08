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
    [Route("api/car")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly DatabaseContext _databaseContext;

        public CarController()
        {
            _databaseContext = new DatabaseContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            try
            {
                return await Task.FromResult(_databaseContext.GetCollection<Car>()
                        .AsQueryable()
                        .ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("login:required")]
        public async Task<ActionResult<IEnumerable<Car>>> GetUserCars(string login)
        {
            if (string.IsNullOrEmpty(login))
                return BadRequest("Login can not be empty");

            try
            {
                return await Task.FromResult(_databaseContext.GetCollection<Document>()
                    .AsQueryable()
                    .Where(x => x.User.Login == login)
                    .Select(x => x.RentedCar)
                    .ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Car>> GetCar(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id must not be empty!");

            try
            {
                return await _databaseContext.GetCollection<Car>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]Car car)
        {
            if (!car.isValid())
                return BadRequest("Car is invalid!");

            try
            {
                await _databaseContext.GetCollection<Car>().InsertOneAsync(car);
                return Ok($"Car {car.Id} created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutCar(Guid id, [FromBody]Car car)
        {
            if (id == Guid.Empty)
                return BadRequest("Id can not be empty!");
            if (!car.isValid())
                return BadRequest("Car is not valid");
            try
            {
                await _databaseContext.GetCollection<Car>()
                    .FindOneAndReplaceAsync(Builders<Car>.Filter.Where(x => x.Id.Equals(id)), car);
                return Ok($"Car {id} updated");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("id can not be empty!");

            try
            {
                await _databaseContext.GetCollection<Car>()
                    .FindOneAndDeleteAsync(Builders<Car>.Filter.Where(x => x.Id.Equals(id)));
                return Ok($"Car {id} deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
