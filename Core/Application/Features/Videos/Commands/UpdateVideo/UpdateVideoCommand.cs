using MediatR;

namespace Application.Features.Videos.Commands.UpdateVideo
{
    public class UpdateVideoCommand : IRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
    }
}