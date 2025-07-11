using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CurrencyMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CurrencyId { get; set; } //CurrencyId from DB ObjectId  //CurrencyMaster

        [BsonElement("CurrencyName")]
        public string? CurrencyName { get; set; } //curreny name

        [BsonElement("CurrencyCode")]
        public string? CurrencyCode { get; set; } //currrecy code Dollar

        [BsonElement("CurrencySymbol")]
        public string? CurrencySymbol { get; set; } //Eg:USD

        [BsonElement("MajorCurrency")]
        public string? MajorCurrency { get; set; } //Global Trade (USD,EUR,GBP)

        [BsonElement("MinorCurrency")]
        public string? MinorCurrency { get; set; } //Specific Contries(INR,Cent)

        [BsonElement("Scale")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Scale { get; set; } // 1 Rupee=1.00 Paise

        [BsonElement("RoundTo")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal RoundTo { get; set; } //0.01 = 123.45

        [BsonElement("Minimum")]
        [BsonRepresentation(BsonType.Decimal128)]
        public string? Minimum { get; set; } //Minimum value

        [BsonElement("CurrencyStatus")] //Active
        public string CurrencyStatus { get; set; } = "Y";

        [BsonElement("CurrencyEntryDate")]
        public DateTime CurrencyEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CurrencyUpdateDate")]
        public DateTime CurrencyUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
