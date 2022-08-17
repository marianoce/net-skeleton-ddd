using Application.Contracts.Persistence;
using AutoMapper;
using Core.Application.Features.Videos.Querys.GetVideoListQuery;
using MediatR;

namespace Application.Features.Videos.Querys.GetVideoListQuery
{
    public class GetVideoListQueryHandler : IRequestHandler<GetVideoListQuery, List<VideosVm>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetVideoListQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<VideosVm>> Handle(GetVideoListQuery request, CancellationToken cancellationToken)
        {
            var videoList = await _unitOfWork.VideoRepository.GetVideoByNombre(request._username);

            return _mapper.Map<List<VideosVm>>(videoList);
        }
    }
}