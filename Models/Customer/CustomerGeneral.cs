using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerGeneral
    {
        [BsonElement("CustomerAddressId")]
        public required string CustomerAddressId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string CustomerId { get; set; }

        [BsonElement("CustomerGeneralStatus")]
        public string CustomerGeneralStatus { get; set; } = "Y";

        [BsonElement("CustomerGeneralEntryDate")]
        public DateTime CustomerGeneralEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string CustomerUpdatedBy { get; set; }

        [BsonElement("CustomerGeneralUpdateDate")]
        public DateTime CustomerGeneralUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
