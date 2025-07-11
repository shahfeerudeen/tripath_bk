using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerShipper
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerShipperId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        [BsonElement("CartageHandlerAir")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CartageHandlerAir { get; set; }

        [BsonElement("CartageHandlerLCL")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CartageHandlerLCL { get; set; }

        [BsonElement("CartageHandlerFCL")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CartageHandlerFCL { get; set; }

        [BsonElement("CustomerServiceSea")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerServiceSea { get; set; }

        [BsonElement("CustomerServiceAir")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerServiceAir { get; set; }

        [BsonElement("CartageCoordinatorSea")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CartageCoordinatorSea { get; set; }

        [BsonElement("CartageCoordinatorAir")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CartageCoordinatorAir { get; set; }

        [BsonElement("AccountManagerSea")]
        public string? AccountManagerSea { get; set; }

        [BsonElement("AccountManagerAir")]
        public string? AccountManagerAir { get; set; }

        [BsonElement("AccountManagerLand")]
        public string? AccountManagerLand { get; set; }

        [BsonElement("CustomerShipperStatus")]
        public string CustomerShipperStatus { get; set; } = "Y";
        [BsonElement("CustomerShippeCreatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerShippeCreatedBy { get; set; }

        [BsonElement("CustomerShipperEntryDate")]
        public DateTime CustomerShipperEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("CustomerShipperUpdateDate")]
        public DateTime CustomerShipperUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
