using FluentValidation;

namespace Application.Features.Videos.Commands
{
    public class CreateVideoCommandValidator : AbstractValidator<CreateVideoCommand>
    {
        public CreateVideoCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("El nombre no puede ser vacio")
                                .NotNull().MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres");
        }
    }
}