using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace NutrientAuto.Community.Domain.CommandValidators.SeedWork
{
    public class FormFileValidator : AbstractValidator<IFormFile>
    {
        public FormFileValidator()
        {
            RuleFor(command => command.FileName)
                .Length(1, 100);

            RuleFor(command => command.ContentType)
                .Must(contentType =>
                {
                    return contentType.ToLower() != "image/jpg" ||
                           contentType.ToLower() != "image/jpeg" ||
                           contentType.ToLower() != "image/x-png" ||
                           contentType.ToLower() != "image/png";
                })
                .WithMessage("O formato da imagem deve ser .jpg, .jpeg ou .png");

            RuleFor(command => command.Length)
                .GreaterThan(0)
                .LessThan(1024 * 1024 * 3)
                .WithMessage("O tamanho do arquivo máximo do arquivo deve ser 3mb");
        }
    }
}