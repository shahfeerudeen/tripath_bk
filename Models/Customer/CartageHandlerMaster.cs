using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CartageHandlerMaster
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CartageHandlerId { get; set; }

        [BsonElement("CustomerId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CustomerId { get; set; } = string.Empty;

        [BsonElement("CartageHandlerName")]
        public string CartageHandlerName { get; set; } = string.Empty;

        [BsonElement("CartagehandlerAlias")]
        public string CartagehandlerAlias { get; set; } = string.Empty;

        [BsonElement("CartageHandlerType")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CartageHandlerType { get; set; } = string.Empty;

        [BsonElement("CartageHandlerEntityType")]
        public string CartageHandlerEntityType { get; set; } = string.Empty;

        [BsonElement("CartageHandlerStatus")]
        public string CartageHandlerStatus { get; set; } = "Y";

        [BsonElement("CartageHandlerEntryDate")]
        public DateTime CartageHandlerEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("CustomerUpdatedBy")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CustomerUpdatedBy { get; set; }

        [BsonElement("CartageHandlerUpdateDate")]
        public DateTime CartageHandlerUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
