using Microsoft.AspNetCore.Mvc;
using catalogServiceAPI.Models;
using catalogServiceAPI.Services;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace catalogServiceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CatalogController : ControllerBase
    {
        private readonly ILogger<CatalogController> _logger;
        public readonly IConfiguration _config;
        private readonly ICatalogRepository _catalogRepo;
        private readonly HttpClient _httpClient;

        public CatalogController(ILogger<CatalogController> logger, IConfiguration configuration, ICatalogRepository catalogRepository, HttpClient httpClient)
        {
            _logger = logger;
            _catalogRepo = catalogRepository;
            _config = configuration;
            _httpClient = httpClient;
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product product)
        {
            _logger.LogInformation("Method PostProduct called at {DT}", DateTime.UtcNow.ToLongTimeString());
            _catalogRepo.AddProduct(product);
            _logger.LogInformation("A new product with ProductID {ProductID} has been added.", product.ProductID);
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
            _logger.LogInformation("Method GetAllProduct called at {DT}", DateTime.UtcNow.ToLongTimeString());
            var list = await _catalogRepo.GetAllProducts();
            return Ok(list);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int ProductID, Product product)
        {
            _logger.LogInformation("Method UpdateProduct called at {DT}", DateTime.UtcNow.ToLongTimeString());
            var exists = await _catalogRepo.GetProduct(ProductID);
            if (exists == null)
            {
                return NotFound();
            }
            await _catalogRepo.UpdateProduct(ProductID, product);
            return Ok();
        }

        [HttpDelete("deleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            _logger.LogInformation("Method DeleteProduct called at {DT}", DateTime.UtcNow.ToLongTimeString());
            _catalogRepo.DeleteProduct(id);
            return Ok();
        }

        [HttpGet("Version")]
        public Dictionary<string, string> GetVersion()
        {
            var properties = new Dictionary<string, string>();
            var assembly = typeof(Program).Assembly;
            properties.Add("service", "Catalog");
            var ver = System.Diagnostics.FileVersionInfo.GetVersionInfo(typeof(Program).Assembly.Location).FileVersion ?? "Undefined";
            Console.WriteLine($"Version before: {ver}");
            properties.Add("version", ver);
            var feature = HttpContext.Features.Get<IHttpConnectionFeature>();
            var localIPAddr = feature?.LocalIpAddress?.ToString() ?? "N/A";
            properties.Add("local-host-address", localIPAddr);
            return properties;
        }

        [HttpPost("PostProductToAuction")]
        public async Task<IActionResult> PostToAuction([FromBody] AuctionProduct[] ProductList)
        {
            var json = JsonConvert.SerializeObject(ProductList);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            _logger.LogInformation($"Attempting to Post to auction-service: {_config["AuctionConnection"]}/auction/AuctionPost");
            var response = await _httpClient.PostAsync($"{_config["AuctionConnection"]}/auction/AuctionPost", content);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Products successfully sent to the auction service.");
                return Ok();
            }
            else
            {
                _logger.LogError("Failed to send to auction-service");
                return NotFound();
            }
        }
    }
}
