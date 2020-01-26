using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentUploader.Infrastructure.Entities;

namespace DocumentUploader.Infrastructure.Data.Repositories.Interfaces.Infrastructure
{
    public interface IDocumentRepository
    {
        Task InsertNewDocument(Document requestModel);
        Task<IEnumerable<Document>> GetAllAsync();
    }
}