using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CartageCoordinatorMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CartageCoordinatorId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; } = string.Empty;

        [BsonElement("CartageCoordinatorContact")]
        public string CartageCoordinatorContact { get; set; } = string.Empty;

        [BsonElement("CartageCoordinatorTelephone")]
        public string CartageCoordinatorTelephone { get; set; } = string.Empty;

        [BsonElement("CartageCoordinatorMobile")]
        public string CartageCoordinatorMobile { get; set; } = string.Empty;

        [BsonElement("CartageCoordinatorEmailId")]
        public string CartageCoordinatorEmailId { get; set; } = string.Empty;

        [BsonElement("CartageCoordinatorDepartment")]
        public string CartageCoordinatorDepartment { get; set; } = string.Empty;

        [BsonElement("CartageCoordinatorType")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CartageCoordinatorType { get; set; } = string.Empty;

        [BsonElement("CartageCoordinatorStatus")]
        public string CartageCoordinatorStatus { get; set; } = "Y";

        [BsonElement("CartageCoordinatorEntryDate")]
        public DateTime CartageCoordinatorEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("CartageCoordinatorUpdateDate")]
        public DateTime CartageCoordinatorUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
