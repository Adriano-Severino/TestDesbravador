using FluentValidation;
using Modelo.Domain.Entities;

namespace Modelo.Service.Validators
{
    public class EmployeesValidator : AbstractValidator<Employees>
    {
        public EmployeesValidator()
        {
            RuleFor(c => c.Nome)
                .NotEmpty().WithMessage("Por favor digite um nome.")
                .NotNull().WithMessage("Por favor digite um nome.");

            RuleFor(c => c.Sobrenome)
                .NotEmpty().WithMessage("Por favor digite um sobrenome.")
                .NotNull().WithMessage("Por favor digite um sobrenome.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Por favor digite um email.")
                .EmailAddress().WithMessage("Por favor digite um email válido.")
                .NotNull().WithMessage("Por favor digite um email.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Por favor digite uma senha.")
                .NotNull().WithMessage("Por favor digite um senha");
        }
    }
}
