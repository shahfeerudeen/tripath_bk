using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerConsignee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerConsigneeId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        [BsonElement("DeliveryHandlerAir")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeliveryHandlerAir { get; set; }

        [BsonElement("DeliveryHandlerLCL")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeliveryHandlerLCL { get; set; }

        [BsonElement("DeliveryHandlerFCL")] 
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeliveryHandlerFCL { get; set; }

        [BsonElement("DeliveryCoordinatorSea")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeliveryCoordinatorSea { get; set; }

        [BsonElement("DeliveryCoordinatorAir")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeliveryCoordinatorAir { get; set; }

        [BsonElement("CustomerServiceSea")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerServiceSea { get; set; }

        [BsonElement("CustomerServiceAir")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerServiceAir { get; set; }

        [BsonElement("AccountManagerAir")]
        public string? AccountManagerAir { get; set; }

        [BsonElement("AccountManagerSea")]
        public string? AccountManagerSea { get; set; }

        [BsonElement("AccountManagerLand")]
        public string? AccountManagerLand { get; set; }

        [BsonElement("CurrencyUpliftAir")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal CurrencyUpliftAir { get; set; }

        [BsonElement("CurrencyUpliftSea")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal CurrencyUpliftSea { get; set; }

        [BsonElement("CustomerConsigneeStatus")]
        public string CustomerConsigneeStatus { get; set; } = "Y";

        [BsonElement("CustomerConsigneeCreatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerConsigneeCreatedBy { get; set; }

        [BsonElement("CustomerConsigneeEntryDate")]
        public DateTime CustomerConsigneeEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("CustomerConsigneeUpdateDate")]
        public DateTime CustomerConsigneeUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
