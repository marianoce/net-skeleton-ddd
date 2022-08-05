using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.Videos.Querys.GetVideoListQuery
{
    public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, List<VideosVm>>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public GetVideoListQueryHandler(IVideoRepository videoRepository, IMapper mapper)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
        }

        public async Task<List<VideosVm>> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
        {
            var videoList = await _videoRepository.GetVideoByNombre(request._username);

            return _mapper.Map<List<VideosVm>>(videoList);
        }
    }
}