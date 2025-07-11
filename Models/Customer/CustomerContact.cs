using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerContact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerContactId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        [BsonElement("CustomerContactName")]
        public string? CustomerContactName { get; set; }

        [BsonElement("CustomerContactDesignation")]
        public string? CustomerContactDesignation { get; set; }

        [BsonElement("CustomerContactTelephone")]
        public string? CustomerContactTelephone { get; set; }

        [BsonElement("CustomerContactEmailAddress")]
        public string? CustomerContactEmailAddress { get; set; }

        [BsonElement("CustomerContactIsEmailPrimary")]
        public bool CustomerContactIsEmailPrimary { get; set; } = false;

        [BsonElement("CustomerContactDepartment")]
        public string? CustomerContactDepartment { get; set; }

        [BsonElement("CustomerContactMobile")]
        public string? CustomerContactMobile { get; set; }

        [BsonElement("CustomerContactIsMobilePrimary")]
        public bool CustomerContactIsMobilePrimary { get; set; } = false;

        [BsonElement("CustomerContactIsContactPrimary")]
        public bool CustomerContactIsContactPrimary { get; set; } = false;

        [BsonElement("CustomerContactStatus")]
        public string CustomerContactStatus { get; set; } = "Y";

        [BsonElement("CustomerContactEntryDate")]
        public DateTime CustomerContactEntryDate { get; set; } = DateTime.UtcNow;
        
        [BsonElement("CustomerContactCreatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerContactCreatedBy { get; set; }
        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("CustomerContactUpdateDate")]
        public DateTime CustomerContactUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
