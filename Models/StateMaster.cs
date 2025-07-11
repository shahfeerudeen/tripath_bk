using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class StateMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? StateId { get; set; } //ObjectId

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CountryId { get; set; } //ObjectId from CountryMaster

        [BsonElement("StateName")]
        public string? StateName { get; set; } //TN

        [BsonElement("StateCode")]
        public string? StateCode { get; set; } //Statecode

        [BsonElement("StateStatus")]
        public string StateStatus { get; set; } = "Y";

        [BsonElement("StateEntryDate")]
        public DateTime StateEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("StateUpdateDate")]
        public DateTime StateUpdateDate { get; set; } = DateTime.UtcNow;
    }
}  
