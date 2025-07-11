using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    [BsonIgnoreExtraElements]
    public class CustomerMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerId { get; set; }

        [Required]
        public required string CustomerName { get; set; }
        public string? AliseName { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public required string CountryId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public required string StateId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [Required]
        public required string CityId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserTypesId { get; set; }

        public string CustomerStatus { get; set; } = "Y";

        public DateTime CustomerEntryDate { get; set; } = DateTime.UtcNow;

        public DateTime CustomerUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
