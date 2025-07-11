using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerServiceMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerServiceMasterId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; } = string.Empty;

        [BsonElement("CustomerServiceContact")]
        public string CustomerServiceContact { get; set; } = string.Empty;

        [BsonElement("CustomerServiceTelephone")]
        public string CustomerServiceTelephone { get; set; } = string.Empty;

        [BsonElement("CustomerServiceMobile")]
        public string CustomerServiceMobile { get; set; } = string.Empty;

        [BsonElement("CustomerServiceEmailId")]
        public string CustomerServiceEmailId { get; set; } = string.Empty;

        [BsonElement("CustomerServiceDepartment")]
        public string CustomerServiceDepartment { get; set; } = string.Empty;

        [BsonElement("CustomerServiceType")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerServiceType { get; set; } = string.Empty;

        [BsonElement("CustomerServiceStatus")]
        public string CustomerServiceStatus { get; set; } = "Y";

        [BsonElement("CustomerServiceEntryDate")]
        public DateTime CustomerServiceEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("CustomerServiceUpdateDate")]
        public DateTime CustomerServiceUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
