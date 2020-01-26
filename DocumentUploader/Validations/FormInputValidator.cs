using System.Security.Cryptography.X509Certificates;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace DocumentUploader.Validations
{
    public class FormInputValidator:AbstractValidator<IFormFile>
    {
        public FormInputValidator()
        {
            RuleFor(x => x).NotEmpty().WithMessage("form is null or empty");
            RuleFor(x => x.ContentType).NotEmpty().Equal("application/pdf").WithMessage("file type is not pdf");
            RuleFor(x => x.Length).LessThan(5242880).WithMessage("file content is greater than 5 MB");
        }
    }
}
