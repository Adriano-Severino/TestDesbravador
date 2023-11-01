using FluentValidation;
using Modelo.Domain.Entities;

namespace Modelo.Service.Validators
{
    public class ProjectValidator : AbstractValidator<Project>
    {
        public ProjectValidator()
        {
            RuleFor(c => c.ProjectName)
                .NotEmpty().WithMessage("Por favor digite o nome do projeto.")
                .NotNull().WithMessage("Por favor difite o nome do projeto.");

            RuleFor(c => c.ProjectDescription)
                .NotEmpty().WithMessage("Por favor digite a descrição do projeto.")
                .NotNull().WithMessage("Por favor digite a descrição do projeto.");
        }
    }
}
