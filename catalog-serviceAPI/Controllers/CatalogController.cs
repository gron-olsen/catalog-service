using Microsoft.AspNetCore.Mvc;
using catalogServiceAPI.Models;
using catalogServiceAPI.Services;
using MongoDB.Driver;
using System.Linq;

namespace catalogServiceAPI.Controllers
{
    // Kontrollerklasse til håndtering af HTTP-anmodninger relateret til katalogtjenester
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        public readonly IConfiguration _config;
        private readonly ICatalogRepository _catalogRepo;

        // Konstruktør til at initialisere controlleren med logger, konfiguration og katalogrepository
        public CatalogController(ILogger<CatalogController> logger, IConfiguration configuration, ICatalogRepository catalogRepository)
        {
            _logger = logger;
            _catalogRepo = catalogRepository;
            _config = configuration;
        }

        // Endpunkt til at hente versionsoplysninger om programmet
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

        // Endpunkt til at tilføje et nyt produkt til kataloget
        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product product)
        {
            _logger.LogInformation("Metode AddProduct kaldt kl. {DT}", DateTime.UtcNow.ToLongTimeString());
            _catalogRepo.AddProduct(product);
            _logger.LogInformation("Et nyt produkt med ProduktID {ProductID} er blevet tilføjet.", product.ProductID);
            return Ok();
        }

        // Endpunkt til at hente et produkt baseret på dets id
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

        // Endpunkt til at hente alle produkter fra kataloget
        [HttpGet("getAllProducts")]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            _logger.LogInformation("Metode GetAllProducts kaldt kl. {DT}", DateTime.UtcNow.ToLongTimeString());
            var list = await _catalogRepo.GetAllProducts();
            return Ok(list);
        }

        // Endpunkt til at opdatere et eksisterende produkt i kataloget
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int ProductID, Product product)
        {
            _logger.LogInformation("Metode UpdateProduct kaldt kl. {DT}", DateTime.UtcNow.ToLongTimeString());
            var exists = await _catalogRepo.GetProduct(ProductID);
            if (exists == null)
            {    
                return NotFound();
            }
            await _catalogRepo.UpdateProduct(ProductID, product);
            return Ok();
        }

        // Endpunkt til at slette et produkt fra kataloget baseret på dets id
        [HttpDelete("deleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _logger.LogInformation("Metode DeleteProduct kaldt kl. {DT}", DateTime.UtcNow.ToLongTimeString());
            _catalogRepo.DeleteProduct(id);
            return Ok();
        }
    }
}
