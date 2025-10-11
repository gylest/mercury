namespace AngularServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderDetailsController : ControllerBase
{
    readonly OrderDetailsService orderDetailsService;
    private readonly ILogger<OrderDetailsController> _logger;

    public OrderDetailsController(IConfiguration configuration, ILogger<OrderDetailsController> logger)
    {
        orderDetailsService = new OrderDetailsService(configuration.GetConnectionString("OctobridgeDatabase"));
        _logger = logger;
    }

    //GET: api/OrderDetails/2
    [HttpGet("{orderId}")]
    public ActionResult<IEnumerable<OrderDetail>> Get(int orderId)
    {
        List<OrderDetail> orderDetails;

        try
        {
            orderDetails = orderDetailsService.GetOrderDetailsByOrderId(orderId);

            // Log 
            string cvJson = JsonSerializer.Serialize(orderDetails);
            _logger.LogInformation($"OrderDetails (Get): {cvJson}");

            return Ok(orderDetails);
        }
        catch (Exception exc)
        {
            return BadRequest($"Error in retrieving order details. Source: {exc.Source}. Message: {exc.Message}.");
        }
    }

}
