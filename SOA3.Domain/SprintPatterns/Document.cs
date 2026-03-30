namespace SOA3.Domain.SprintPatterns
{
    public class Document
    {
        private Guid _id = Guid.NewGuid();
        private string _fileName;
        private string _fileType;
        private string _content;
        private DateTime _uploadDateTime;
    }
}
