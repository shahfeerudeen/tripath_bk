using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerUserTypesMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUserId { get; set; }

        [BsonElement("CustomerUserRole")]
        public string CustomerUserRole { get; set; } = string.Empty;

        [BsonElement("CustomerUserStatus")]
        public string CustomerUserStatus { get; set; } = "Y";

        [BsonElement("CustomerUserEntryDate")]
        public DateTime CustomerUserEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUserUpdateDate")]
        public DateTime CustomerUserUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
