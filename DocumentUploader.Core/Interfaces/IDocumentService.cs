using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentUploader.Core.Models;

namespace DocumentUploader.Core.Interfaces
{
   public interface IDocumentService
    {
        Task UploadDocumentAsync(CreateDocumentRequestModel createDocumentRequestModel);
        Task<IEnumerable<DocumentResponseModel>> GetAllAsync();
    }
}
