using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class OrganizationMaster
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("OrganizationName")]
    public string? OrganizationName { get; set; }

    [BsonElement("OrganizationAddress")]
    public string? OrganizationAddress { get; set; }

    [BsonElement("OrganizationEmail")]
    public string? OrganizationEmail { get; set; }

    [BsonElement("OrganizationPhoneNumber")]
    public string? OrganizationPhoneNumber { get; set; }

    [BsonIgnore] // Prevents this from being mapped to MongoDB directly
    public string OrganizationStatus => InternalStatus == "Y" ? "Active" : "Inactive";

    // Backing field for DB

    [BsonElement("OrganizationStatus")]
    [JsonIgnore]
    public string InternalStatus { get; set; } = "Y";

    [BsonElement("OrganizationEntryDate")]
    public DateTime OrganizationEntryDate { get; set; } = DateTime.UtcNow;

    [BsonElement("OrganizationUpdatedDate")]
    public DateTime OrganizationUpdatedDate { get; set; } = DateTime.UtcNow;
}
