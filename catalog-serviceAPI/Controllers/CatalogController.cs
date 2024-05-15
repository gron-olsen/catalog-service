using Microsoft.AspNetCore.Mvc;
using catalogServiceAPI.Models;
using catalogServiceAPI.Services;
using MongoDB.Driver;
using System.Linq;

namespace catalogServiceAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogController : ControllerBase
{
    private readonly ILogger<CatalogController> _logger;
    public readonly IConfiguration _config;
    private readonly ICatalogRepository _catalogRepo;

    public CatalogController(ILogger<CatalogController> logger, IConfiguration configuration, ICatalogRepository catalogRepository)
    {
        _logger = logger;
        _catalogRepo = catalogRepository;
        _config = configuration;
    }

    [HttpGet("version")]
    public IEnumerable<string> Get()
    {
        var properties = new List<string>();
        var assembly = typeof(Program).Assembly;
        foreach (var attribute in assembly.GetCustomAttributesData())
        {
            properties.Add($"{attribute.AttributeType.Name} - {attribute.ToString()}");
        }
        return properties;
    }
    [HttpPost("AddProduct")]
    public IActionResult AddProduct(Product product)
    {
        _logger.LogInformation("Metode PostProduct called at {DT}", DateTime.UtcNow.ToLongTimeString());
        _catalogRepo.AddProduct(product);
        _logger.LogInformation("a new product with ProductID {ProductID} has been added.", product.ProductID);
        return Ok();
    }
    [HttpGet("GetProduct/{id}")]
     public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _catalogRepo.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    [HttpGet("getAllProducts")]
    public async Task<ActionResult<List<Product>>> GetAllProducts()
    {

        _logger.LogInformation("Metode GetAllProduct called at {DT}", DateTime.UtcNow.ToLongTimeString());

        var list = await _catalogRepo.GetAllProducts();
        return Ok(list);
    }
    [HttpPut("UpdateProduct")]
public async Task<IActionResult> UpdateProduct(int ProductID,Product product)
    {
        _logger.LogInformation("Metode UpdateProduct called at {DT}", DateTime.UtcNow.ToLongTimeString());
     //finds the product
     var exists = await _catalogRepo.GetProduct(ProductID);
         if (exists == null)
     {    
        return NotFound();
    }
    //updates the product
    await _catalogRepo.UpdateProduct(ProductID, product);
        return Ok();
    }

    [HttpDelete("deleteProduct/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        _logger.LogInformation("Metode DeleteProduct called {DT}", DateTime.UtcNow.ToLongTimeString());

        _catalogRepo.DeleteProduct(id);
        return Ok();
    }
}
