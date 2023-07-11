using DocumentService.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentService.Infrastructure
{
    public class DocumentRepository
    {
        private readonly Context _context;
        public Context UnitOfWork
        {
            get { return _context; }
        }

        public DocumentRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Document>> GetAllAsync()
        {
            return await _context.Documents
                .Include(p => p.FileLinks)
                .OrderBy(doc => doc.Title).ToListAsync();
        }

        public async Task<Document> GetByIdAsync(Guid id)
        {
            return await _context.Documents
                .Where(doc => doc.Id == id)
                .Include(p => p.FileLinks)
                .FirstOrDefaultAsync() ?? throw new Exception("Object not found.");
        }

        public async Task<Document> GetByIdAsNoTrackingAsync(Guid id)
        {
            return await _context.Documents
                .AsNoTracking()
                .Where(doc => doc.Id == id)
                .Include(p => p.FileLinks)
                .FirstOrDefaultAsync() ?? throw new Exception("Object not found.");
        }

        public async Task<Document> AddAsync(Document document)
        {
            var doc = await _context.Documents
                .AsNoTracking()
                .Where(doc => doc.Id == document.Id)
                .FirstOrDefaultAsync();

            if (doc != null)
                throw new Exception("This object already exists.");

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            return document;
        }

        public async Task UpdateAsync(Document document)
        {
            var existDocument = await GetByIdAsync(document.Id);

            if (existDocument == null)
                throw new Exception("Object not found.");
            _context.Entry(existDocument).CurrentValues.SetValues(document);

            foreach(var filelink in document.FileLinks)
            {
                var existFileLink = existDocument.FileLinks.FirstOrDefault(fl => fl.Id == filelink.Id);
                if (existFileLink == null)
                {
                    existDocument.FileLinks.Add(filelink);
                    continue;
                }    
            }

            foreach(var existFileLink in existDocument.FileLinks)
            {
                if (!document.FileLinks.Any(fl => fl.Id == existFileLink.Id)) 
                {
                    _context.Remove(existFileLink);
                } 
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var document = await GetByIdAsync(id);

            if (document == null)
                throw new Exception("Object not found.");

            _context.Remove(document);
            await _context.SaveChangesAsync();
        }

        public void ChangeTrackerClear()
        {
            _context.ChangeTracker.Clear();
        }
    }
}
