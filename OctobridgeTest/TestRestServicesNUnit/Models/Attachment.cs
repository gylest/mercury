using System;

namespace Tests.Models
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Filetype { get; set; }
        public long Length { get; set; }
        public byte[] Filedata { get; set; }
        public DateTime Createdate { get; set; }
    }
}
