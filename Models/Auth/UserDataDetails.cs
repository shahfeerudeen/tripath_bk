using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class UserDataDetails
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("UserId")]
        public string? UserId { get; set; }

        public string? OrganizationId { get; set; } // renamed for clarity

        [BsonElement("BearerToken")]
        public string? BearerToken { get; set; }

        [BsonElement("UserEntryDate")]
        public DateTime? UserEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("UserUpdateDate")]
        public DateTime? UserUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
