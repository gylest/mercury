namespace AngularServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CodedValuesController : ControllerBase
{    
    readonly CodedValuesService codedValuesService;
    private readonly Serilog.ILogger _logger;

    public CodedValuesController(IConfiguration configuration, Serilog.ILogger logger)
    {
        codedValuesService = new CodedValuesService(configuration.GetConnectionString("OctobridgeDatabase"));
        _logger = logger;
    }

    // GET: api/codedvalues?groupName=OrderStatus
    [HttpGet]
    public ActionResult<IEnumerable<CodedValue>> GetCodedValue([FromQuery] string groupName)
    {
        List<CodedValue> codedValues;

        try
        {
            codedValues = codedValuesService.GetCodedValuesByName(groupName);

            // Log 
            string cvJson = JsonSerializer.Serialize(codedValues);
            _logger.Information($"CodedValues (Get): {cvJson}");

            return Ok(codedValues);
        }
        catch (Exception exc)
        {
            return BadRequest($"Error in retrieving customers. Source: {exc.Source}. Message: {exc.Message}.");
        }
    }

}
