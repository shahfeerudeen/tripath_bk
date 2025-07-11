using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace tripath.Models
{
    public class CustomerAllDetails
    {
        public CustomerMaster? CustomerMaster { get; set; }
        public CustomerAddress? CustomerAddress { get; set; }
        public CustomerGeneral? CustomerGeneral { get; set; }
    }
}
