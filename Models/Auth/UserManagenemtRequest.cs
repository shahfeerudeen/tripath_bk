using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class UserManagementRequest
    {
        [BsonElement("UserName")]
        [Required(ErrorMessage = "Username is required")]
        [RegularExpression(
            @"^(?!.*\..*\.).*[a-zA-Z0-9 .]+$",
            ErrorMessage = "Username can contain only letters, numbers, spaces, and one dot (.) only."
        )]
        public string? UserName { get; set; }

        [BsonElement("UserPassword")]
        [Required(ErrorMessage = "Password is required")]
        public string? UserPassword { get; set; }

        public required string OrganizationId { get; set; }

    }
}
