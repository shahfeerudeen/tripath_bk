using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerCarrier
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerCarrierId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; } = string.Empty;

        [BsonElement("IsAir")]
        public bool IsAir { get; set; } = false;

        [BsonElement("IsSea")]
        public bool IsSea { get; set; } = false;

        [BsonElement("IsRoad")]
        public bool IsRoad { get; set; } = false;

        [BsonElement("IsRail")]
        public bool IsRail { get; set; } = false;

        [BsonElement("Airline")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Airline { get; set; }

        [BsonElement("ShippingLine")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ShippingLine { get; set; }

        [BsonElement("CustomerCarrierStatus")]
        public string CustomerCarrierStatus { get; set; } = "Y";

        [BsonElement("CustomerCarrierEntryDate")]
        public DateTime CustomerCarrierEntryDate { get; set; } = DateTime.UtcNow;
        [BsonElement("CustomerCarrierCreatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerCarrierCreatedBy { get; set; }

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("CustomerCarrierUpdateDate")]
        public DateTime CustomerCarrierUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
