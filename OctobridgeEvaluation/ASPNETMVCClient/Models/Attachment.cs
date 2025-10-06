using System;
using System.Collections.Generic;

#nullable disable

namespace MVCClient.Models
{
    public partial class Attachment
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public string Filetype { get; set; }
        public long Length { get; set; }
        public byte[] Filedata { get; set; }
        public DateTime RecordCreated { get; set; }
    }
}
