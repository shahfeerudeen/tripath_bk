using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class AirLineMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? AirLineId { get; set; }

        [BsonElement("AirlineName")]
        public string AirlineName { get; set; } = string.Empty;

        [BsonElement("AirLineCode")]
        public string AirLineCode { get; set; } = string.Empty;

        [BsonElement("AirlinePrefix")]
        public string AirlinePrefix { get; set; } = string.Empty;

        [BsonElement("IsAirlineCheckDigit")]
        public string IsAirlineCheckDigit { get; set; } = "Y"; // Default to "N" or use bool if needed

        [BsonElement("AirLineStatus")]
        public string AirLineStatus { get; set; } = "Y";

        [BsonElement("AirLineEntryDate")]
        public DateTime AirLineEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("AirLineUpdateDate")]
        public DateTime AirLineUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
