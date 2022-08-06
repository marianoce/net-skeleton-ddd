using FluentValidation;

namespace Application.Features.Videos.Commands.UpdateVideo
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateVideoCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(p => p.Nombre).NotNull().WithMessage("Nombre invalido");
        }
    }
}