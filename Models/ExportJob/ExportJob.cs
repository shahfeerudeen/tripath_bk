using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models.ExportJob
{
    public class ExportJob
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ExportJobId { get; set; }

        public string?UserId{ get; set; }

        [BsonElement]
        public string? EDocPath { get; set; }
        public string? EDoc { get; set; }

        public string? JobNo { get; set; }

        public DateTime ExporterDate { get; set; } = DateTime.UtcNow;

        public string? ExporterOrganizationName { get; set; }

        public string? Mode { get; set; }

        public string? SBType { get; set; }
        public string? SBNo { get; set; }

        public string? SBStatus { get; set; } = "L.E.O";

        public DateTime ExporterEntryDate { get; set; } = DateTime.UtcNow;

        public DateTime ExporterUpdateDate { get; set; } = DateTime.UtcNow;

        public string? ExportStatus { get; set; } = "Processed";
        
        }
}