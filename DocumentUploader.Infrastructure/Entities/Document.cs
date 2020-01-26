using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentUploader.Infrastructure.Entities
{
    public class Document 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public byte[] File { get; set; }
    }
}
