namespace AngularServer.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttachmentsController : ControllerBase
{
    readonly AttachmentsService attachmentsService;
    private readonly ILogger<AttachmentsController> _logger;

    public AttachmentsController(IConfiguration configuration, ILogger<AttachmentsController> logger)
    {
        attachmentsService = new AttachmentsService(configuration.GetConnectionString("OctobridgeDatabase"));
        _logger = logger;
    }

    // GET: api/Attachments?fileName=amber.jpg
    [HttpGet]
    public ActionResult<IEnumerable<Attachment>> Get([FromQuery] string? fileName)
    {
        try
        {
            Attachment[] attachments = attachmentsService.GetAttachments(fileName);

            // Log 
            _logger.LogInformation($"Attachments (Get): Filename: {fileName ?? string.Empty} Count: {attachments.Length}");

            return Ok(attachments);
        }
        catch (Exception exc)
        {
            return BadRequest($"Error in retrieving attachments. Source: {exc.Source}. Message: {exc.Message}.");
        }
    }

    // GET: api/Attachments/id
    [HttpGet("{id}", Name = "Get")]
    public IActionResult Get(int id)
    {
        byte[] fileData;

        try
        {
            // Memory stream for file stored in SQL Server 
            fileData = attachmentsService.GetFile(id, out var fileName, out var fileType);

            // Convert fileData to memory stream
            var memory = new MemoryStream(fileData)
            {
                // Return file as blob
                Position = 0
            };

            return File(memory, fileType, fileName);
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    // POST: api/Attachments
    [HttpPost]
    [RequestSizeLimit(100_000_000)]
    public ActionResult<Attachment> UploadFile()
    {
        Attachment attachment = new Attachment();

        try
        {
            var file = Request.Form.Files[0];

            using (MemoryStream stream = new MemoryStream())
            {
                file.CopyTo(stream);

                byte[] buffer = stream.ToArray();

                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fileType = file.ContentType;

                    // Copy to SQL
                    attachmentsService.SaveFile(fileName, fileType, file.Length, buffer, out int id, out DateTime dtm);

                    // Create return object
                    attachment.Id = id;
                    attachment.FileName = fileName;
                    attachment.FileType = fileType;
                    attachment.Length = file.Length;
                    attachment.RecordCreated = dtm;

                    // Log 
                    string attachmentJson = JsonSerializer.Serialize(attachment);
                    _logger.LogInformation($"Attachments (Add): {attachmentJson}");
                }
            }

            return Ok(attachment);
        }
        catch (Exception ex)
        {
            return StatusCode(401, ex.Message);
        }
    }

    // DELETE: api/Attachments/id
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            attachmentsService.DeleteFile(id);

            // Log 
            _logger.LogInformation($"Attachments (Delete): ID: {id}");

            return Ok();
        }
        catch (Exception exc)
        {
            return BadRequest($"Error in deleting attachment. Source: {exc.Source}. Message: {exc.Message}.");
        }
    }

}
