using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentUploader.Core.Interfaces;
using DocumentUploader.Core.Models;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DocumentUploader.Controllers
{
    [Route("v1" + "/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IValidator<IFormFile> _validator;
        private readonly IDocumentService _documentService;

        public DocumentController(IValidator<IFormFile> validator, IDocumentService documentService)
        {
            _validator = validator;
            _documentService = documentService;
        }

        [ProducesResponseType(typeof(IEnumerable<DocumentResponseModel>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("All")]
        public async Task<IActionResult> GetAllAsync()
        {
           var response =  await _documentService.GetAllAsync();
           return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("upload-document")]
        public async Task<IActionResult> UploadDocumentAsync(IFormFile formFile)
        {
            var eValidationResult = _validator.Validate(formFile);
            if (!eValidationResult.IsValid)
            {
                return BadRequest(eValidationResult.Errors.Select(x=>x.ErrorMessage));
            }

            byte[] fileContent;
            using (var fileStream = formFile.OpenReadStream())
            {
                fileContent = ReadAllBytes(fileStream);
            }

            await _documentService.UploadDocumentAsync(new CreateDocumentRequestModel
            {
                Location = formFile.Name,
                Name = formFile.FileName,
                File = fileContent
            });

            return Accepted();
        }

        #region private memebers

        private byte[] ReadAllBytes(Stream inStream)
        {
            if (inStream is MemoryStream)
                return ((MemoryStream)inStream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                inStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        #endregion

    }
}