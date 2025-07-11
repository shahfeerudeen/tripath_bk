using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerIntegration
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerIntegrationId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }  // FK from Customer table

        [BsonElement("FrieightJobSharingEnabled")]
        public bool FrieightJobSharingEnabled { get; set; }

        [BsonElement("FrieightJobSharingIntegrationType")]
        public string? FrieightJobSharingIntegrationType { get; set; }

        [BsonElement("FrieightJobSharingPartnerId")]
        public string? FrieightJobSharingPartnerId { get; set; }

        [BsonElement("CustomImportJobSharing")]
        public string? CustomImportJobSharing { get; set; }

        [BsonElement("CustomExportJobSharing")]
        public string? CustomExportJobSharing { get; set; }

        [BsonElement("InvoiceSharing")]
        public string? InvoiceSharing { get; set; }

        [BsonElement("CustomerIntegrationStatus")]
        public string CustomerIntegrationStatus { get; set; } = "Y";  // Default Active
        
        [BsonElement("CustomerIntegrationCreatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerIntegrationCreatedBy { get; set; }

        [BsonElement("CustomIntegrationEntryDate")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CustomIntegrationEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }  // ObjectId  from UserManagement

        [BsonElement("CustomIntegrationUpdateDate")]
        public DateTime CustomIntegrationUpdateDate { get; set; } = DateTime.UtcNow;


    }
}