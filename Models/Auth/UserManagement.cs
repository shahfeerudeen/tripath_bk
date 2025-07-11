using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class UserManagement
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public string? UserId { get; set; } //ObjectId from DB

        [BsonRepresentation(BsonType.ObjectId)]
        public string? UserMasterId { get; set; } // ObjectId from UserMaster table

        [BsonRepresentation(BsonType.ObjectId)]
        public string? ParentId { get; set; } // ObjectId from UserManagementTable(UserId)

        [BsonElement("OrganizationId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? OrganizationId { get; set; } // objectId from OrganizationMaster table

        [BsonElement("LoginName")]
        public string? UserName { get; set; } //UserName

        [BsonElement("UserPassword")]
        public required string UserPassword { get; set; } //UserPassword

        [BsonElement("UserEmail")]
        public string? UserEmail { get; set; } //UserEmail

        [BsonElement("UserMobileNo")]
        public string? UserMobileNo { get; set; } //UserMobileNo

        [BsonElement("UserStatus")]
        public string UserStatus { get; set; } = "Y";

        [BsonElement("UserEntryDate")]
        public DateTime UserEntryDate { get; set; } = DateTime.UtcNow;

        [BsonElement("UserUpdateDate")]
        public DateTime UserUpdateDate { get; set; } = DateTime.UtcNow;
    }
}
