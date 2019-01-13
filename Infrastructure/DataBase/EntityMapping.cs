﻿using Model;
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
                x.SetIgnoreExtraElements(true);
                x.MapIdMember(y => y.getOid());
            });

            BsonClassMap.RegisterClassMap<Car>(x =>
            {
                x.AutoMap();
                x.SetIgnoreExtraElements(true);
                x.MapIdMember(y => y.getOid());
            });
        }
    }
}