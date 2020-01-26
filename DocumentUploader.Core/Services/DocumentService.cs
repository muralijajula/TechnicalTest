using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentUploader.Core.Interfaces;
using DocumentUploader.Core.Models;
using DocumentUploader.Infrastructure.Data.Repositories.Interfaces.Infrastructure;
using DocumentUploader.Infrastructure.Entities;

namespace DocumentUploader.Core.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;
        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task UploadDocumentAsync(CreateDocumentRequestModel requestModel)
        {
            await _documentRepository.InsertNewDocument(new Document
            {
                Name = requestModel.Name,
                File = requestModel.File,
                Location = requestModel.Location
            });
        }

        public async Task<IEnumerable<DocumentResponseModel>> GetAllAsync()
        {
            List<DocumentResponseModel> responseModel = new List<DocumentResponseModel>();

            var response =await _documentRepository.GetAllAsync();

            foreach (var value in response)
            {
                responseModel.Add(new DocumentResponseModel
                {
                    Name = value.Name,
                    Length = value.File.Length,
                    Location = value.Location
                });
            }

            return responseModel;
        }
    }
}
