using Microsoft.AspNetCore.Mvc;

namespace catalog_serviceAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CatalogServiceController : ControllerBase
{

    private readonly ILogger<CatalogServiceController> _logger;

    public CatalogServiceController(ILogger<CatalogServiceController> logger)
    {
        _logger = logger;
    }
    [HttpGet("GetAllProducts")]

    [HttpGet("GetProduct/{id}")]

    [HttpPost("PostProduct")]

    [HttpPut("UpdateProduct")]

    [HttpDelete("DeleteProduct/{id}")]

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
}
