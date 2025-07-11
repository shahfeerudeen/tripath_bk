using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class ExporterMaster
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ExporterMasterId { get; set; }

    public required string ExporterJobNumber { get; set; }

    public DateTime? ExportJobDate { get; set; }

    public DateTime? ExportJobReceivedOn { get; set; }

    public string? ExporterJobFillingMode { get; set; }

    public string? ExporterJobFillingType { get; set; }

    public string? ExportLoadingPort { get; set; }

    public string? ExportTransportMode { get; set; }

    public string? ExportSBType { get; set; }

    public string? ExportCustomHouse { get; set; }

    public string? ExportJOBOwner { get; set; }

    public string? ExportStandardDocuments { get; set; }

    
    public string? ExportStatus { get; set; } = "Y"; 

    public DateTime? ExportEntryDate { get; set; }

    public DateTime? ExportUpdateDate { get; set; }
}
