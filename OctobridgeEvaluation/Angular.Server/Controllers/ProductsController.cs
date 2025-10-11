namespace AngularServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    readonly ProductsService productsService;
    private readonly ILogger<ProductsController> _logger;

    public ProductsController(IConfiguration configuration, ILogger<ProductsController> logger)
    {
        productsService = new ProductsService(configuration.GetConnectionString("OctobridgeDatabase"));
        _logger = logger;
    }

    // GET: api/Products
    // GET: api/Products?name=Shadow&productNumber=B
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProduct([FromQuery] string? name, [FromQuery] string? productNumber)
    {
        Product[] products;

        try
        {
            if ((name is null) && (productNumber is null))
            {
                products = productsService.GetAllProducts().ToArray();
            }
            else
            {
                products = productsService.GetProductsByName(name, productNumber).ToArray();
            }

            return Ok(products);
        }
        catch (Exception exc)
        {
            return BadRequest($"Error in retrieving products. Source: {exc.Source}. Message: {exc.Message}.");
        }
    }

    // GET: api/Products/4
    [HttpGet("{productId}")]
    public ActionResult<Product> Get(int productId)
    {
        Product product;

        try
        {
            product = productsService.GetProductById(productId);

            // log
            string productJsonString = JsonSerializer.Serialize(product);
            _logger.LogInformation($"Products (Get by Id): {productJsonString}");

            return Ok(product);
        }
        catch (Exception exc)
        {
            return BadRequest($"Error in retrieving product. Source: {exc.Source}. Message: {exc.Message}.");
        }
    }

    // POST: api/Products
    [HttpPost]
    public ActionResult<Product> Post([FromBody] Product product)
    {
        try
        {
            if (product == null)
            {
                return BadRequest();
            }

            productsService.AddProduct(product);

            // log
            string productJson = JsonSerializer.Serialize(product);
            _logger.LogInformation($"Products (Add): {productJson}");

            return Ok(product);
        }
        catch (Exception exc)
        {
            return BadRequest($"Error in adding product. Source: {exc.Source}. Message: {exc.Message}.");
        }
    }

    // PUT: api/Products/5
    [HttpPut("{id}")]
    public ActionResult<Product> Put(int id, [FromBody] Product product)
    {
        if (product == null)
        {
            return BadRequest();
        }

        try
        {
            product.ProductId = id;

            productsService.UpdateProduct(product);

            return Ok(product);
        }
        catch (Exception exc)
        {
            return BadRequest($"Error in updating product. Source: {exc.Source}. Message: {exc.Message}.");
        }
    }

    // DELETE: api/Products/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            productsService.DeleteProduct(id);
            _logger.LogInformation($"Products (Delete): ID: {id}");

            return Ok();
        }
        catch (Exception exc)
        {
            return BadRequest($"Error in deleting product. Source: {exc.Source}. Message: {exc.Message}.");
        }
    }

}
