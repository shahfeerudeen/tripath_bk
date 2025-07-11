using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CityMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CityId { get; set; } //ObjectId

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? StateId { get; set; } //ObjectId  from StateMaster

        [BsonElement("CityName")]
        public string? CityName { get; set; } //CityName

        [BsonElement("CityCode")]
        public string? CityCode { get; set; } //CityCode

        [BsonElement("Functions")]
        public string? Functions { get; set; } // code 12_34.

        [BsonElement("IATA")] //Airport code
        public string? IATA { get; set; }

        [BsonElement("CityStatus")]
        public string CityStatus { get; set; } = "Y";

        [BsonElement("CityEntryDate")]
        public DateTime CityEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CityUpdateDate")]
        public DateTime CityUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
