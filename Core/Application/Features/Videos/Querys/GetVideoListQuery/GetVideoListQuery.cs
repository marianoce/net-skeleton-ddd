using MediatR;

namespace Application.Features.Videos.Querys.GetVideoListQuery
{
    public class GetVideoListQuery : IRequest<List<VideosVm>>
    {
        public string _username { get; set; } = string.Empty;
        public GetVideoListQuery(string username)
        {
            _username = username;
        }
    }
}