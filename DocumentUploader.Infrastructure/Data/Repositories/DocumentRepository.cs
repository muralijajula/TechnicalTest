using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DocumentUploader.Infrastructure.Data.Contexts;
using DocumentUploader.Infrastructure.Data.Repositories.Interfaces.Infrastructure;
using DocumentUploader.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace DocumentUploader.Infrastructure.Data.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly FileUploaderDbContext _context;

        public DocumentRepository(FileUploaderDbContext context)
        {
            _context = context;
        }

        public async Task InsertNewDocument(Document requestModel)
        {
            var response = await _context.Documents.AddAsync(requestModel);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Document>> GetAllAsync()
        {

            return await _context.Documents.ToListAsync();
        }
    }


}
