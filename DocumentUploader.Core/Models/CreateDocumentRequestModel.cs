namespace DocumentUploader.Core.Models
{
    public class CreateDocumentRequestModel
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public byte[] File { get; set; }
    }
}
