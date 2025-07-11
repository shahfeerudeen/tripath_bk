using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class ShippingLineMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ShippingLineId { get; set; }

        [BsonElement("ShippingLineName")]
        public string ShippingLineName { get; set; } = string.Empty;

        [BsonElement("ShippingLineCode")]
        public string ShippingLineCode { get; set; } = string.Empty;

        [BsonElement("ShippingLineStatus")]
        public string ShippingLineStatus { get; set; } = "Y";

        [BsonElement("ShippingLineEntryDate")]
        public DateTime ShippingLineEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("ShippingLineUpdateDate")]
        public DateTime ShippingLineUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
