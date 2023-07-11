namespace DocumentService.Domain
{
    public class Document
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
        public List<FileLink> FileLinks { get; set; } = new List<FileLink>();
    }
}
