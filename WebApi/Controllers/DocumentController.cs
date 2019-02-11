using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public DocumentController()
        {
            _databaseContext = new DatabaseContext();
        }

        [HttpGet("{login:required}")]
        public async Task<ActionResult<IEnumerable<Document>>> GetUserDocuments(string login)
        {
            try
            {
                var documents = await Task.FromResult(_databaseContext.GetCollection<Document>()
                    .AsQueryable().Where(x => x.User.Login == login)
                    .ToList());
                return Ok(documents);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        [HttpPost]
        public async Task<ActionResult> PostDocument([FromBody] Document document)
        {
            if (!document.isValid())
                return BadRequest(new { message = "Document is not valid" });

            try
            {
                document.Id = Guid.NewGuid();

                await _databaseContext.GetCollection<Document>()
                .InsertOneAsync(document);
                return Ok(new { message = $"Document {document.Id} created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:Guid}")]
        public async Task<ActionResult> PutDocument(Guid id, [FromBody] Document document)
        {
            if (!document.isValid())
                return BadRequest(new { message = "Document is invalid" });

            try
            {
                await _databaseContext.GetCollection<Document>()
                     .FindOneAndReplaceAsync(Builders<Document>
                     .Filter.Where(x => x.Id.Equals(id)), document);
                return Ok(new { message = $"Document {id} modified" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteDocument(Guid id)
        {
            try
            {

                await _databaseContext.GetCollection<Document>()
                  .FindOneAndDeleteAsync(Builders<Document>
                  .Filter.Where(x => x.Id.Equals(id)));

                return Ok(new { message = $"Document {id} deleted" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
