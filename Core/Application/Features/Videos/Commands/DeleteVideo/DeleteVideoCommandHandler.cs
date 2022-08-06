using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Videos.Commands.DeleteVideo
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DeleteVideoCommandHandler(IVideoRepository videoRepository, IMapper mapper, ILogger logger)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var videoToDelete = await _videoRepository.GetByIdAsync(request.Id);

            if (videoToDelete == null)
            {
                _logger.LogError("No se encontro el video id: " + request.Id);
                throw new NotFoundException(nameof(Video), request.Id.ToString());
            }

            await _videoRepository.DeleteAsync(videoToDelete);

            _logger.LogInformation($"La informacion fue exitosa actualizando el video: { request.Id.ToString() }");

            return Unit.Value;
        }
    }
}