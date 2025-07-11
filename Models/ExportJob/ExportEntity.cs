using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Tripath_Logistics_BE.Models.ExportJob
{
    public class ExportEntity
    {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? ExportEntityId { get; set; }

    [BsonRepresentation(BsonType.ObjectId)]
    public string? ExportMasterId { get; set; }

    public string? ExportEntityType { get; set; }
    public string? ExportEntityName { get; set; }
    public string? ExportEntityCountry { get; set; }
    public string? ExportState { get; set; }
    public string? ExportCity { get; set; }
    public string? ExportEntityPostalCode { get; set; }
    public string? ExportEntityAddress { get; set; }
    public string? ExportEntityTelephone { get; set; }
    public string? ExportEntityFax { get; set; }
    public string? ExportEntityContactPerson { get; set; }
    public string? ExportEntityEmail { get; set; }
    public string? ExportEntityMobile { get; set; }

    public string? ExportStatus { get; set; }
    public DateTime ExportEntryDate { get; set; }
    public DateTime ExportUpdateDate { get; set; }
    }
}