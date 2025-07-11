using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class UserMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserMasterId { get; set; } //OjectId from DB

        [BsonElement("UserMasterRole")]
        public string? UserMasterRole { get; set; } // User or Organization

        [BsonElement("UserMasterStatus")]
        public string UserMasterStatus { get; set; } = "Y";

        [BsonElement("UserMasterEntryDate")]
        public DateTime UserMasterEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("UserMasterUpdateDate")]
        public DateTime UserMasterUpdateDate { get; set; } = DateTime.UtcNow;
    }
}