using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ApiLog
{ 
   [BsonId]
   [BsonRepresentation(BsonType.ObjectId)]
   public string? Id { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }
    public string? RequestId { get; set; }

    public string? RequestMethod { get; set; }
    public string? RequestPath { get; set; }
    public string? RequestQueryParams { get; set; }
    public string? RequestBody { get; set; }
    public DateTime RequestEntryDate { get; set; } = DateTime.UtcNow;

    public DateTime ResponseSentDate { get; set; } = DateTime.UtcNow;
    public string? ResponseMessage { get; set; }

    public string? ResponseStatus { get; set; }
}
