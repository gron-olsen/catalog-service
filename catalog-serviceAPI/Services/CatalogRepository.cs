using System.Collections.Generic;
using System.Threading.Tasks;
using catalogServiceAPI.Models;
using MongoDB.Driver;
using catalogServiceAPI.Services;

namespace catalogServiceAPI.Services {

public class CatalogRepository : IProductRepository
{
    public readonly IConfiguration _configuration;
    private readonly IMongoCollection<Product> _catalogCollection;

    public CatalogRepository(ILogger<CatalogRepository> logger, IConfiguration configuration)
    {
        
        _configuration = configuration;
    
        logger.LogInformation($"INFO: connection string is: {configuration["connectionString"]}");
    
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