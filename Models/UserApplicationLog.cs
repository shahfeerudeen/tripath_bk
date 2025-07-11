using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class UserApplicationLog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserLogId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; } // ObjectId from UserManagement

        [BsonElement("IPAddress")]
        public string? IPAddress { get; set; }

        [BsonElement("LogInTime")]
        public DateTime LogInTime { get; set; } = DateTime.UtcNow;

        [BsonElement("LogoutTime")]
        public DateTime LogoutTime { get; set; } = DateTime.UtcNow;

        [BsonElement("UserLogStatus")]
        public string UserLogStatus { get; set; } = "Y"; // Active by default

        [BsonElement("UserLogEntryDate")]
        public DateTime UserLogEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("UserLogUpdateDate")]
        public DateTime UserLogUpdateDate { get; set; } = DateTime.UtcNow;

        [BsonElement("UserLogCreatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserLogCreatedBy { get; set; }

        [BsonElement("UserLogUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserLogUpdatedBy { get; set; }

        [BsonElement("UserLogDeletedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserLogDeletedBy { get; set; }
    }
}
