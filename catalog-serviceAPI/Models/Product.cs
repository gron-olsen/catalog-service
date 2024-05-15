using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace catalogServiceAPI.Models
{
    // Produktklasse til at repræsentere produkter i kataloget
    public class Product
    {
        // MongoDBs id-felt, der bruges som primærnøgle i databasen
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }

    
        public int ProductID { get; set; }

  
        [BsonRequired]
        public string ProductName { get; set; }

       
        [BsonRequired]
        public string ProductDescription { get; set; }

      
        public int ProductStartPrice { get; set; }

  
        public int ProductPrice { get; set; }

       
        public DateTime ProductStartDate { get; set; }

       
        public DateTime ProductEndDate { get; set; }
    }
}