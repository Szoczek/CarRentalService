using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Model;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using WebApi.DataModel;

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
        public async Task<ActionResult<IEnumerable<DocumentData>>> GetDocuments()
        {
            IMongoQueryable<Document> query;

            try
            {
                query = _databaseContext.GetCollection<Document>().AsQueryable();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            List<DocumentData> documents = new List<DocumentData>();

            try
            {
                foreach (var document in query)
                {
                    documents.Add(new DocumentData().InitFrom(document));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return await Task.FromResult(documents);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<DocumentData>> GetDocument(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id must not be empty!");
            try
            {
                return await Task.FromResult(new DocumentData()
                    .InitFrom(_databaseContext.GetCollection<Document>()
                    .AsQueryable()
                    .FirstOrDefaultAsync(x => x.Id.Equals(id))));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostDocument([FromBody] DocumentData document)
        {
            if (document == null)
                return BadRequest("Document must not be empty");

            try
            {
                return await Task
                    .FromResult(Ok(_databaseContext
                    .GetCollection<Document>()
                    .InsertOneAsync(document.CopyTo(new Document()))
                    .IsCompleted.ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            //return Ok($"Document {document.Id} has been created.");
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> DeleteDocument(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Id must not be empty!");
            try
            {
                return await Task
                    .FromResult(Ok(_databaseContext
                    .GetCollection<Document>()
                    .FindOneAndDeleteAsync(Builders<Document>
                    .Filter.Where(x => x.Id.Equals(id)))
                    .IsCompleted.ToString()));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
