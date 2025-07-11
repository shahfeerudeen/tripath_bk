using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerServices
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerServiceId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        [BsonElement("IsBank")]
        public bool IsBank { get; set; } = false;

        [BsonElement("IsContainerFreightStation")]
        public bool IsContainerFreightStation { get; set; } = false;

        [BsonElement("IsContainerProvider")]
        public bool IsContainerProvider { get; set; } = false;

        [BsonElement("IsContainerTerminalOperator")]
        public bool IsContainerTerminalOperator { get; set; } = false;

        [BsonElement("IsContainerYard")]
        public bool IsContainerYard { get; set; } = false;

        [BsonElement("IsCustomBroker")]
        public bool IsCustomBroker { get; set; } = false;

        [BsonElement("IsFumigationContractor")]
        public bool IsFumigationContractor { get; set; } = false;

        [BsonElement("IsInlandContainerDepo")]
        public bool IsInlandContainerDepo { get; set; } = false;

        [BsonElement("IsPackingDepot")]
        public bool IsPackingDepot { get; set; } = false;

        [BsonElement("IsUnPackingDep")]
        public bool IsUnPackingDep { get; set; } = false;

        [BsonElement("IsVendor")]
        public bool IsVendor { get; set; } = false;

        [BsonElement("CustomerServiceStatus")]
        public string CustomerServiceStatus { get; set; } = "Y";

        [BsonElement("CustomerServiceEntryDate")]
        public DateTime CustomerServiceEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerServiceCreatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerServiceCreatedBy { get; set; }

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("CustomerServiceUpdateDate")]
        public DateTime CustomerServiceUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
