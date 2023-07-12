namespace DocumentService.Domain.DTOs
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Description { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public List<FileLinkDto> FileLinkDtos { get; set; } = new List<FileLinkDto>();
    }
}
