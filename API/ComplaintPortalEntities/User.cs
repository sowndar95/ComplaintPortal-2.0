using ComplaintPortalEntities;
using MongoDB.Bson.Serialization.Attributes;

namespace ComplaintPortalEntities
{
    public class User : BaseEntity
    {     
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
        public string Aadhar { get; set; } = string.Empty;
        public Guid Role { get; set; } = new();
    }
}