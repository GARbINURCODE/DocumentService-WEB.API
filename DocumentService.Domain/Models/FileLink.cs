namespace DocumentService.Domain
{
    public class FileLink
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }

        public Document Document { get; set; } = null!;

    }
}
