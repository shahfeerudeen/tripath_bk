using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class DeliveryHandlerMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeliveryHandlerId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; } = string.Empty;

        [BsonElement("DeliveryHandlerName")]
        public string DeliveryHandlerName { get; set; } = string.Empty;

        [BsonElement("DeliveryHandlerAlias")]
        public string DeliveryHandlerAlias { get; set; } = string.Empty;

        [BsonElement("DeliveryHandlerEntityType")]
        public string DeliveryHandlerEntityType { get; set; } = string.Empty;

        [BsonElement("DeliveryHandlerType")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DeliveryHandlerType { get; set; } = string.Empty;

        [BsonElement("DeliveryHandlerStatus")]
        public string DeliveryHandlerStatus { get; set; } = "Y";

        [BsonElement("DeliveryHandlerEntryDate")]
        public DateTime DeliveryHandlerEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("DeliveryHandlerUpdateDate")]
        public DateTime DeliveryHandlerUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
