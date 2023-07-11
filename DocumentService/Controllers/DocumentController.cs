using Microsoft.AspNetCore.Mvc;

namespace DocumentService.Infrastructure.DTOs
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly Context _context;
        private readonly DocumentRepository _documentRepository;

        public DocumentController(Context context)
        {
            _context = context;
            _documentRepository = new DocumentRepository(_context);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentDto>>> GetDocuments()
        {
            var documents = await _documentRepository.GetAllAsync();
            return DocumentMapper.ToDto(documents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentDto>> GetDocument(Guid id)
        {
            var document = await _documentRepository.GetByIdAsNoTrackingAsync(id);
            var documentDto = DocumentMapper.ToDto(document);
            return documentDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(Guid id, DocumentDto documentDto)
        {
            if (id != documentDto.Id) throw new Exception("Bad request");

            var document = DocumentMapper.ToEntity(documentDto);
            await _documentRepository.UpdateAsync(document);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> PostDocument(DocumentDto documentDto)
        {
            documentDto.Id = Guid.NewGuid();
            foreach(var filelinkDto in documentDto.FileLinkDtos)
            {
                filelinkDto.Id = Guid.NewGuid();
            }

            var document = DocumentMapper.ToEntity(documentDto);
            await _documentRepository.AddAsync(document);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(Guid id)
        {
            await _documentRepository.DeleteAsync(id);

            return Ok();
        }
    }
}
