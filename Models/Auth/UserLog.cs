using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace tripath.Models
{
    public class UserLog
    {
        [BsonId]
          [BsonElement("_id")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }
        public string? IPAddress { get; set; }
        public string? UserLogStatus { get; set; } // "I" or "O"

        public DateTime? UserLoginTime { get; set; }

        public DateTime? UserLogoutTime { get; set; }
        public DateTime UserLogEntryDate { get; set; }

        [BsonElement("UserLogUpdateDate")]
        public DateTime UserLogUpdateDate { get; set; }
    }
}