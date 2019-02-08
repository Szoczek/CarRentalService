using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace WebApi.Controllers
{
    [Route("api/Document")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly DatabaseContext _databaseContext;

        public DocumentController()
        {
            _databaseContext = new DatabaseContext();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetDocuments()
        {
            try
            {
                return await Task.FromResult(_databaseContext.GetCollection<Document>()
                    .AsQueryable()
                    .ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("login:required")]
        public async Task<ActionResult<IEnumerable<Document>>> GetUserDocuments(string login)
        {
            if (string.IsNullOrEmpty(login))
                return BadRequest("Incorrect login!");
            try
            {
                return await Task.FromResult(_databaseContext.GetCollection<Document>()
                    .AsQueryable().Where(x => x.User.Login == login)
                    .ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<Document>> GetDocument(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id must not be empty!");
            try
            {
                return await _databaseContext.GetCollection<Document>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id.Equals(id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostDocument([FromBody] Document document)
        {
            if (!document.isValid())
                return BadRequest("Document is not valid!");

            try
            {
                document.Id = Guid.NewGuid();

                await _databaseContext.GetCollection<Document>()
                .InsertOneAsync(document);
                return Ok($"Document {document.Id} created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("id:Guid")]
        public async Task<ActionResult> PutDocument(Guid id, [FromBody] Document document)
        {
            if (id == Guid.Empty)
                return BadRequest("Id must not be empty!");
            if (!document.isValid())
                return BadRequest("Document is invalid!");

            try
            {
                await _databaseContext.GetCollection<Document>()
                     .FindOneAndReplaceAsync(Builders<Document>
                     .Filter.Where(x => x.Id.Equals(id)), document);
                return Ok($"Document {id} modified");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteDocument(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id must not be empty!");
            try
            {

                await _databaseContext.GetCollection<Document>()
                  .FindOneAndDeleteAsync(Builders<Document>
                  .Filter.Where(x => x.Id.Equals(id)));

                return Ok($"Document {id} deleted!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
