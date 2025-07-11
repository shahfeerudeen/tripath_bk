using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CountryMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CountryId { get; set; } //ObjectId

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CurrencyId { get; set; } // ObjectId from CurrencyMaster

        [BsonElement("CountryName")]
        public string? CountryName { get; set; } //CountryName

        [BsonElement("CountryCode")]
        public string? CountryCode { get; set; } //CountryCode eg:AF

        [BsonElement("ISDCode")]
        public string? ISDCode { get; set; } //ISD code

        [BsonElement("TimeZone")]
        public DateTimeOffset TimeZone { get; set; } = DateTimeOffset.Now; //+1.00,-12.00

        [BsonElement("Language")]
        public string? Language { get; set; }

        [BsonElement("CodeForBorE")]
        public string? CodeForBorE { get; set; } //buy or export

        [BsonElement("CountryStatus")]
        public string CountryStatus { get; set; } = "Y";

        [BsonElement("CountryEntryDate")]
        public DateTime CountryEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CountryUpdateDate")]
        public DateTime CountryUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
