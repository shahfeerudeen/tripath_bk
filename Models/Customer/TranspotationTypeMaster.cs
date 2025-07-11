using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class TranspotationTypeMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? TranspotationTypeId { get; set; }

        [BsonElement("TranspotationTypeName")]
        public string? TranspotationTypeName { get; set; }

        [BsonElement("TranspotationTypeStatus")]
        public string TranspotationTypeStatus { get; set; } = "Y"; // Default Active

        [BsonElement("TranspotationTypeEntryDate")]
        public DateTime TranspotationTypeEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("TranspotationTypeUpdateDate")]
        public DateTime TranspotationTypeUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
