using DocumentService.Domain;

namespace DocumentService.Infrastructure.DTOs
{
    public static class DocumentMapper
    {
        public static DocumentDto ToDto(Document document)
        {
            var documentDto = new DocumentDto
            {
                Id = document.Id,
                Title = document.Title,
                Content = document.Content,
                Description = document.Description,
                CreatedDate = document.CreatedDate,
                UpdatedDate = document.UpdatedDate,
            };

            foreach(var FileLink in document.FileLinks)
            {
                documentDto.FileLinkDtos.Add(FileLinkMapper.ToDto(FileLink));
            }

            return documentDto;
        }

        public static Document ToEntity(DocumentDto documentDto) 
        {
            var document = new Document
            {
                Id = documentDto.Id,
                Title = documentDto.Title,
                Content = documentDto.Content,
                Description = documentDto.Description,
                CreatedDate = documentDto.CreatedDate,
                UpdatedDate = documentDto.UpdatedDate
            };

            foreach(var filelinkDto in documentDto.FileLinkDtos)
            {
                var filelink = FileLinkMapper.ToEntity(filelinkDto);
                filelink.Document = document;
                document.FileLinks.Add(filelink);
            }

            return document;
        }

        public static List<DocumentDto> ToDto(List<Document> documents)
        {
            var documentDtos = new List<DocumentDto>();

            foreach (var document in documents)
            {
                documentDtos.Add(ToDto(document));
            }

            return documentDtos;
        }
    }
}
