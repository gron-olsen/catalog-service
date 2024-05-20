using System.Collections.Generic;
using System.Threading.Tasks;
using catalogServiceAPI.Models;
using MongoDB.Driver;
using catalogServiceAPI.Services;

namespace catalogServiceAPI.Services
{

    public class CatalogRepository : ICatalogRepository
    {
        // Readonly-felt til lagring af IConfiguration-instantiering og IMongoCollection for produkter
        private readonly IConfiguration _configuration;
        public readonly ILogger<CatalogRepository> _logger;
        private readonly IMongoCollection<Product> _catalogCollection;

        // Konstruktør, der initialiserer repository med logger og konfiguration
        public CatalogRepository(ILogger<CatalogRepository> logger, IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;

            // Log connection string for debug purposes
            _logger.LogInformation($"INFO: connection string is: {configuration["connectionString"]}");

            // Opret en forbindelse til MongoDB og vælg den ønskede database og samling
            var mongoClient = new MongoClient(_configuration["connectionString"]);
            var database = mongoClient.GetDatabase(_configuration["database"]);
            _catalogCollection = database.GetCollection<Product>(_configuration["collection"]);
        }


        public async Task AddProduct(Product product) =>
            await _catalogCollection.InsertOneAsync(product);


        public async Task<Product> GetProduct(int id) =>
            await _catalogCollection.Find(u => u.ProductID == id).FirstOrDefaultAsync();


        public async Task<List<Product>> GetAllProducts() =>
            await _catalogCollection.Find(_ => true).ToListAsync();


        public async Task UpdateProduct(int productId, Product product) =>
            await _catalogCollection.ReplaceOneAsync(u => u.ProductID == productId, product);


        public async Task DeleteProduct(int productId) =>
            await _catalogCollection.DeleteOneAsync(u => u.ProductID == productId);
    }
}
