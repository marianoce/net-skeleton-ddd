using MediatR;

namespace Application.Features.Videos.Commands
{
    public class CreateVideoCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; }
    }
}