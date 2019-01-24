using Model;
using MongoDB.Bson.Serialization;

namespace Infrastructure.Database
{
    public static class EntityMappings
    {
        public static void Map()
        {
            BsonClassMap.RegisterClassMap<User>(x =>
            {
                x.AutoMap();
                x.MapIdMember(y => y.Id);
                x.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Car>(x =>
            {
                x.AutoMap();
                x.MapIdMember(y => y.CarGuid);
                x.SetIgnoreExtraElements(true);
            });

            BsonClassMap.RegisterClassMap<Document>(x =>
            {
                x.AutoMap();
                x.MapIdMember(y => y.Id);
                x.SetIgnoreExtraElements(true);
            });
        }
    }
}