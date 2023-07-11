using DocumentService.Domain;

namespace DocumentService.Infrastructure.DTOs
{
    public class FileLinkMapper
    {
        public static FileLinkDto ToDto(FileLink filelink)
        {
            var filelinkDto = new FileLinkDto
            {
                Id = filelink.Id,
                Name = filelink.Name,
                Type = filelink.Type
            };

            return filelinkDto;
        }

        public static FileLink ToEntity(FileLinkDto filelinkDto) 
        {
            var filelink = new FileLink
            {
                Id = filelinkDto.Id,
                Name = filelinkDto.Name,
                Type = filelinkDto.Type
            };

            return filelink;
        }
    }
}
