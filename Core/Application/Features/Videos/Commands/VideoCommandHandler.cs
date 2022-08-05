using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Videos.Commands
{
    public class VideoCommandHandler : IRequestHandler<VideoCommand, int>
    {
        private readonly IVideoRepository _videoRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public VideoCommandHandler(IVideoRepository videoRepository, IMapper mapper, IEmailService emailService, ILogger logger)
        {
            _videoRepository = videoRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(VideoCommand request, CancellationToken cancellationToken)
        {
            var videoEntity = _mapper.Map<Video>(request);
            var newVideo = await _videoRepository.AddAsync(videoEntity);
            _logger.LogInformation("Video: creado exitosamente");

            await SendEmail(newVideo);

            return newVideo.Id;
        }

        private async Task SendEmail(Video video)
        {
            var email = new Email();
            await _emailService.SendEmail(email);
        }
    }
}