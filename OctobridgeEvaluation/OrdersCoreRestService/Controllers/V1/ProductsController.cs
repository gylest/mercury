namespace OctobridgeCoreRestService.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:ApiVersion}/products")]
public class ProductsController : ControllerBase
{
    private readonly OctobridgeContext _context;
    private readonly ILogger _logger;

    public ProductsController(OctobridgeContext context, ILogger<ProductsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    //
    // GET: https://localhost:44366/api/v1/products/5
    //
    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        return product;
    }

    //
    // GET: https://localhost:44366/api/v1/products
    //
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProduct()
    {
        return await _context.Products.ToListAsync().ConfigureAwait(true);
    }

    //
    // PUT: https://localhost:44366/api/v1/products/5
    //
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, Product product)
    {
        if ((product == null) || (id != product.ProductId))
        {
            return BadRequest();
        }

        // Update record modified
        if (product.RecordModified == DateTime.MinValue)
        {
            product.RecordModified = DateTime.UtcNow;
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync().ConfigureAwait(true);
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    //
    // POST: https://localhost:44366/api/v1/products
    //
    [HttpPost]
    public async Task<ActionResult<Product>> PostProduct(Product product)
    {
        if (product == null)
        {
            return BadRequest();
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync().ConfigureAwait(true);

        // log
        string productJson = JsonSerializer.Serialize(product);
        _logger.LogInformation($"Product (Post): {productJson}");

        // return
        return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
    }

    //
    // DELETE: https://localhost:44366/api/v1/products/5
    //
    [HttpDelete("{id}")]
    public async Task<ActionResult<Product>> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync().ConfigureAwait(true);

        return product;
    }

    private bool ProductExists(int id)
    {
        return _context.Products.Any(e => e.ProductId == id);
    }
}
