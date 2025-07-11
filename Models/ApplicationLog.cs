using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

public class ApplicationLog
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ApplicationLogId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? UserId { get; set; }

    public string ApplicationAction { get; set; } = "";

    public string ApplicationLogStatus { get; set; } = "Y";

    public DateTime ApplicationLogEntryDate { get; set; } = DateTime.Now;
}
