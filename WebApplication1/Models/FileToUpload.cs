using Sale.Business;
using System.Collections.Generic;
using Sale.Data;
using System;

namespace Sale.Models
{
    public class FileToUpload
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileType { get; set; }
        public long LastModifiedTime { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public string FileAsBase64 { get; set; }
        public byte[] FileAsByteArray { get; set; }

    }
}
