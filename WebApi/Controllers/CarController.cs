using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.Utils;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {

        private readonly DatabaseContext _databaseContext;

        public CarController()
        {
            _databaseContext = new DatabaseContext();
        }

        [HttpGet("{login:required}")]
        public async Task<ActionResult<IEnumerable<Car>>> GetUserCars(string login)
        {
            try
            {
                var cars = await Task.FromResult(_databaseContext.GetCollection<Document>()
                    .AsQueryable()
                    .Where(x => x.User.Login == login)
                    .Select(x => x.RentedCar)
                    .ToList());
                return Ok(cars);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostCar([FromBody]Car car)
        {
            if (!car.isValid())
                return BadRequest(new { message = "Car is invalid" });

            try
            {
                car.Id = Guid.NewGuid();
                car.VinNumber = car.VinNumber ?? CarUtils.GenerateVinNumber();
                await _databaseContext.GetCollection<Car>().InsertOneAsync(car);
                return Ok(new { message = $"Car {car.Id} created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutCar(Guid id, [FromBody]Car car)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "Id can not be empty!" });
            if (!car.isValid())
                return BadRequest(new { message = "Car is not valid" });
            try
            {
                await _databaseContext.GetCollection<Car>()
                    .FindOneAndReplaceAsync(Builders<Car>.Filter.Where(x => x.Id.Equals(id)), car);
                return Ok(new { message = $"Car {id} updated" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteCar(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "id can not be empty" });

            try
            {
                await _databaseContext.GetCollection<Car>()
                    .FindOneAndDeleteAsync(Builders<Car>.Filter.Where(x => x.Id.Equals(id)));
                return Ok(new { message = $"Car {id} deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
