using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{

    [BsonIgnoreExtraElements]
    public class OtpVerification
    {
        [BsonId]

        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserId { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string Otp { get; set; }

        public DateTime OtpEntryDate { get; set; }

        public DateTime OtpUpdateDate { get; set; }

        public int AttemptCount { get; set; } = 1;

        public string? OtpType { get; set; } // "email" or "phone"

        public bool IsOtpVerified { get; set; } = false;
        public string? SessionId { get; set; }
        public DateTime? SessionExpiry { get; set; }  


   
    }
}
