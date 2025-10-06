using System;

namespace AngularClient.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public Int64 Length { get; set; }
        public DateTime? RecordCreated { get; set; }

        public Attachment()
        {
        }

        public Attachment(int id, string filename, string filetype,Int64 length,DateTime? createdate)
        {
            this.Id = id;
            this.FileName = filename;
            this.FileType = filetype;
            this.Length = length;
            this.RecordCreated = createdate;
        }
    }
}

