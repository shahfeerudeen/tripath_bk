using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerReg
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerRegistrationId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; } // ObjectId from Customer table

        [BsonElement("RegistrationType")]
        public string? RegistrationType { get; set; }

        [BsonElement("Branch")]
        public string? Branch { get; set; }

        [BsonElement("RegistrationNo")]
        public string? RegistrationNo { get; set; }

        [BsonElement("ValidDate")]
        public DateTime ValidDate { get; set; }

        [BsonElement("ValidUpto")]
        public DateTime ValidUpto { get; set; }

        [BsonElement("CustomerRegistrationStatus")]
        public string CustomerRegistrationStatus { get; set; } = "Y"; // Default to Active
        [BsonElement("CustomerRegCreateddBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerRegCreateddBy { get; set; }

        [BsonElement("CustomerRegistrationEntryDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CustomerRegistrationEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; } // ObjectId  from UserManagement

        [BsonElement("CustomerRegistrationUpdateDate")]
        public DateTime CustomerRegistrationUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
