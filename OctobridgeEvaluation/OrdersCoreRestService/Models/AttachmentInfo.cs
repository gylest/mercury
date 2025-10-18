namespace OctobridgeCoreRestService.Models;

public partial class AttachmentInfo
{
    public int Id { get; set; }
    public string Filename { get; set; }
    public string Filetype { get; set; }
    public long Length { get; set; }
    public DateTime RecordCreated { get; set; }
}
