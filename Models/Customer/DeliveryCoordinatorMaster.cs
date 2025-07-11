using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class DeliveryCoordinatorMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeliveryCoordinatorId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; } = string.Empty;

        [BsonElement("DeliveryCoordinatorContact")]
        public string DeliveryCoordinatorContact { get; set; } = string.Empty;

        [BsonElement("DeliveryCoordinatorTelephone")]
        public string DeliveryCoordinatorTelephone { get; set; } = string.Empty;

        [BsonElement("DeliveryCoordinatorMobile")]
        public string DeliveryCoordinatorMobile { get; set; } = string.Empty;

        [BsonElement("DeliveryCoordinatorEmailId")]
        public string DeliveryCoordinatorEmailId { get; set; } = string.Empty;

        [BsonElement("DeliveryCoordinatorDepartment")]
        public string DeliveryCoordinatorDepartment { get; set; } = string.Empty;

        [BsonElement("DeliveryCoordinatorType")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string DeliveryCoordinatorType { get; set; } = string.Empty;

        [BsonElement("DeliveryCoordinatorStatus")]
        public string DeliveryCoordinatorStatus { get; set; } = "Y";

        [BsonElement("DeliveryCoordinatorEntryDate")]
        public DateTime DeliveryCoordinatorEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("DeliveryCoordinatorUpdateDate")]
        public DateTime DeliveryCoordinatorUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
