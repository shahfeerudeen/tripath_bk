using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerMasterUpdateModel
    {
        public string? CustomerName { get; set; }
        public string? AliseName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public List<string>? UserTypesId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CountryId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? StateId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CityId { get; set; }

        public string? CustomerAddressBranchName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerAddressTelephone { get; set; }
        public string? CustomerAddressWebsite { get; set; }
        public string? CustomerAddressEmailAddress { get; set; }
        public string? CustomerAddressSalesPerson { get; set; }
        public string? CustomerAddressCollectionExec { get; set; }
        public string? CustomerAddressTaxableType { get; set; }
        public string? CustomerAddressFax { get; set; }
        public bool? CustomerAddressIsDeactivate { get; set; }
        public bool? CustomerAddressIsSetAsDefault { get; set; }
        public string? CustomerAddressPostalCode { get; set; }
        public bool? CustomerAddressLOBWise { get; set; }

        public string? CustomerApprovalStatus { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        public DateTime? CustomerUpdateDate { get; set; }
    }
}
