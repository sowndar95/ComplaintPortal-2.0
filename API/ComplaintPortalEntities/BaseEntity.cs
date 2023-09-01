using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ComplaintPortalEntities
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            CollectionName = GetType().Name;
        }

        public Guid Id { get; set; }

        [BsonIgnore]
        [JsonIgnore]
        public string CollectionName { get; private set; }
    }
}
