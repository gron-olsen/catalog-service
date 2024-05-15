using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace catalogServiceAPI.Models
{

    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public int ProductID { get; set; }
        public required string ProductName { get; set; }
        public required string ProductDescription { get; set; }
        public required int ProductStartPrice { get; set; }
        public int ProductPrice { get; set; }
        public DateTime ProductStartDate { get; set; }
        public DateTime ProductEndDate { get; set; }
    }
}