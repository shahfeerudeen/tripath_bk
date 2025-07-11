using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerDetailAddress
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerDetailAddressId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        [BsonElement("StateId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? StateId { get; set; }

        [BsonElement("CityId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CityId { get; set; }

        [BsonElement("CustomerDetailAddressType")]
        public string? CustomerDetailAddressType { get; set; }

        [BsonElement("CustomerDetailAddressName")]
        public string? CustomerDetailAddressName { get; set; }

        [BsonElement("CustomerDetailAddress")]
        public string? CustomerDetailAddressLine { get; set; }

        [BsonElement("CustomerDetailAddressPostalCode")]
        public string? CustomerDetailAddressPostalCode { get; set; }

        [BsonElement("CustomerDetailAddressTelephone")]
        public string? CustomerDetailAddressTelephone { get; set; }

        [BsonElement("CustomerDetailAddressFax")]
        public string? CustomerDetailAddressFax { get; set; }

        [BsonElement("CustomerDetailEmailAddress")]
        public string? CustomerDetailEmailAddress { get; set; }

        [BsonElement("CustomerDetailAddressStatus")]
        public string CustomerDetailAddressStatus { get; set; } = "Y";
        [BsonElement("CustomerDetailAddressCreatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerDetailAddressCreatedBy { get; set; }

        [BsonElement("CustomerDetailAddressEntryDate")]
        public DateTime CustomerDetailAddressEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("CustomerDetailAddressUpdateDate")]
        public DateTime CustomerDetailAddressUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
