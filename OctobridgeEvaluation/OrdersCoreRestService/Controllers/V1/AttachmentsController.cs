namespace OctobridgeCoreRestService.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:ApiVersion}/attachments")]
public class AttachmentsController : ControllerBase
{
    private readonly OctobridgeContext _context;
    private readonly ILogger _logger;

    public AttachmentsController(OctobridgeContext context, ILogger<AttachmentsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    //
    // GET: https://localhost:44366/api/v1/attachments
    // GET: https://localhost:44366/api/v1/attachments?filename=helfer.jpg
    //
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AttachmentInfo>>> GetAttachmentInfo([FromQuery] string? fileName)
    {
        List<AttachmentInfo> attachmentInfos;
        List<Attachment> attachments;

        if (fileName is null)
        {
            attachments = await _context.Attachments.ToListAsync().ConfigureAwait(true);

            attachmentInfos = attachments
                .Select(x => new AttachmentInfo()
                {
                    Id = x.Id,
                    Filename = x.Filename,
                    Filetype = x.Filetype,
                    Length = x.Length,
                    RecordCreated = x.RecordCreated
                }).ToList();
        }
        else
        {
            attachments = await _context.Attachments.Where(a => a.Filename == fileName).ToListAsync().ConfigureAwait(true);

            attachmentInfos = attachments
                .Select(x => new AttachmentInfo()
                {
                    Id = x.Id,
                    Filename = x.Filename,
                    Filetype = x.Filetype,
                    Length = x.Length,
                    RecordCreated = x.RecordCreated
                }).ToList();
        }

        if (attachmentInfos.Count == 0)
        {
            return NotFound();
        }

        return attachmentInfos;
    }

    //
    // GET: https://localhost:44366/api/v1/attachments/2
    //
    [HttpGet("{id}")]
    public async Task<ActionResult<AttachmentInfo>> GetAttachment(int id)
    {
        try
        {
            Attachment attachment = await _context.Attachments.FindAsync(id);

            if (attachment == null)
            {
                return NotFound();
            }
            else
            {
                // Convert fileData to memory stream
                var memory = new MemoryStream(attachment.Filedata)
                {
                    // Return file as blob
                     Position = 0
                };
                return File(memory, attachment.Filetype, attachment.Filename);
            }
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

    //
    // POST: https://localhost:44366/api/v1/attachments
    //
    [HttpPost]
    [RequestSizeLimit(100_000_000)]
    public async Task<ActionResult<AttachmentInfo>> PostAttachment()
    {
        Attachment attachment = new Attachment();
        AttachmentInfo attachmentInfo = new AttachmentInfo();

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

                    // Create attachment object and save
                    attachment.RecordCreated = DateTime.Now;
                    attachment.Filename = fileName;
                    attachment.Length = file.Length;
                    attachment.Filedata = buffer;
                    attachment.Filetype = fileType;

                    _context.Attachments.Add(attachment);
                    await _context.SaveChangesAsync().ConfigureAwait(true);

                    // Create attachmentinfo object for return
                    attachmentInfo.Id = attachment.Id;
                    attachmentInfo.RecordCreated = attachment.RecordCreated;
                    attachmentInfo.Filename = attachment.Filename;
                    attachmentInfo.Length = attachment.Length;
                    attachmentInfo.Filetype = attachment.Filetype;

                    // Log 
                    string attachmentInfoJson = JsonSerializer.Serialize(attachmentInfo);
                    _logger.LogInformation($"Attachment (Post): {attachmentInfoJson}");

                    // return
                    return CreatedAtAction("PostAttachment", new { id = attachmentInfo.Id }, attachmentInfo);
                }
            }

            return Ok();
        }
        catch (DbUpdateException exc)
        {
            return BadRequest($"Error in creating attachment. Source: {exc.Source}. Message: {exc.Message}.");
        }
        catch (System.Exception ex)
        {
            return StatusCode(401, ex.Message);
        }
    }

    //
    // DELETE: https://localhost:44366/api/v1/attachments/2
    //
    [HttpDelete("{id}")]
    public async Task<ActionResult<AttachmentInfo>> DeleteAttachment(int id)
    {
        AttachmentInfo attachmentInfo = new AttachmentInfo();
        Attachment attachment = await _context.Attachments.FindAsync(id);

        if (attachment == null)
        {
            return NotFound();
        }
        else
        {
            _context.Attachments.Remove(attachment);
            await _context.SaveChangesAsync().ConfigureAwait(true);

            // Create attachmentinfo object for return
            attachmentInfo.Id = attachment.Id;
            attachmentInfo.RecordCreated = attachment.RecordCreated;
            attachmentInfo.Filename = attachment.Filename;
            attachmentInfo.Length = attachment.Length;
            attachmentInfo.Filetype = attachment.Filetype;

            return attachmentInfo;
        }


    }

}
