using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerAddress
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerAddressId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        [BsonElement("CustomerBranchName")]
        public required string CustomerBranchName { get; set; }

        [BsonElement("CustomerAddressLine")]
        public string? CustomerAddressLine { get; set; }

        [BsonElement("CustomerAddressTelephone")]
        public string? CustomerAddressTelephone { get; set; }

        [BsonElement("CustomerAddressWebsite")]
        public string? CustomerAddressWebsite { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [BsonElement("CustomerAddressEmailAddress")]
        public string? CustomerAddressEmailAddress { get; set; }

        [BsonElement("CustomerAddressSalesPerson")]
        public string? CustomerAddressSalesPerson { get; set; }

        [BsonElement("CustomerAddressCollectionExec")]
        public string? CustomerAddressCollectionExec { get; set; }

        [BsonElement("CustomerAddressTaxableType")]
        public string? CustomerAddressTaxableType { get; set; }

        [BsonElement("CustomerAddressFax")]
        public string? CustomerAddressFax { get; set; }

        [BsonElement("CustomerIsDeactivate")]
        public bool CustomerAddressIsDeactivate { get; set; } = false;

        [BsonElement("CustomerAddressIsSetAsDefault")]
        public bool CustomerAddressIsSetAsDefault { get; set; } = false;

        [BsonElement("CustomerAddressPostalCode")]
        public string? CustomerAddressPostalCode { get; set; }

        [BsonElement("CustomerAddressLOBWise")]
        public string? CustomerAddressLOBWise { get; set; }

        [BsonElement("CustomerAddressStatus")]
        public string CustomerAddressStatus { get; set; } = "Y";

        [BsonElement("CustomerAddressCreatedBy")]
        public DateTime CustomerAddressCreatedBy { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerAddressEntryDate")]
        public DateTime CustomerAddressEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerAddressUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public required string CustomerAddressUpdatedBy { get; set; }

        [BsonElement("CustomerAddressUpdateDate")]
        public DateTime CustomerAddressUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
