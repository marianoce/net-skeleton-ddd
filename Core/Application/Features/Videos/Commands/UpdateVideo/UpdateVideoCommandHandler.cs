using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Videos.Commands.UpdateVideo
{
    public class UpdateVideoCommandHandler : IRequestHandler<UpdateVideoCommand>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateVideoCommandHandler(IVideoRepository videoRepository, IMapper mapper, ILogger logger)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            Video videoToUpdate = await _videoRepository.GetByIdAsync(request.Id);
            if (videoToUpdate == null)
            {
                _logger.LogError("No se encontro el video id: " + request.Id);
                throw new NotFoundException(nameof(Video), request.Id.ToString());
            }

            _mapper.Map(request, videoToUpdate, typeof(UpdateVideoCommand), typeof(Video));

            await _videoRepository.UpdateAsync(videoToUpdate);

            _logger.LogInformation($"La informacion fue exitosa actualizando el video: { request.Id.ToString() }");

            return Unit.Value;
        }
    }
}