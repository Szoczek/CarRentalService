using MongoDB.Driver;

namespace Infrastructure.Database
{
    public class DatabaseContext
    {
        private readonly IMongoDatabase _database;

        public DatabaseContext()
        {
            MongoDefaults.GuidRepresentation = MongoDB.Bson.GuidRepresentation.Standard;

            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetDatabase("CarRentalService");
        }

        public IMongoCollection<TModel> GetCollection<TModel>()
        {
            return _database.GetCollection<TModel>(typeof(TModel).Name);
        }
    }
}