using FluentValidation;
using Modelo.Domain.Entities;

namespace Modelo.Service.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Por favor difite um nome.")
                .NotNull().WithMessage("Por favor difite um nome.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Por favor difite um email.")
                .NotNull().WithMessage("Por favor difite um email.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Por favor difite uma senha.")
                .NotNull().WithMessage("Por favor difite um senha");
        }
    }
}
