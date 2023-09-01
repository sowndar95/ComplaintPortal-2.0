using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;

namespace ComplaintPortalEntities
{
    [CollectionName("AppUser")]
    public class ApplicationUser : MongoIdentityUser<Guid>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? RefreshToken { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public DateTimeOffset? RefreshTokenExpiryDate { get; set; }
        public DateTimeOffset? LastPasswordChangedDate { get; set; }
    }
}
