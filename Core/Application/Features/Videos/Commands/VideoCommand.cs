using MediatR;

namespace Application.Features.Videos.Commands
{
    public class VideoCommand : IRequest<int>
    {
        public string Name { get; set; }
        public bool Active { get; set; }
    }
}