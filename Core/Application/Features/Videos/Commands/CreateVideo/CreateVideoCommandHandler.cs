using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Models;
using AutoMapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Features.Videos.Commands
{
    public class CreateVideoCommandHandler : IRequestHandler<CreateVideoCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateVideoCommandHandler(IMapper mapper, IEmailService emailService, ILogger logger, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateVideoCommand request, CancellationToken cancellationToken)
        {
            var videoEntity = _mapper.Map<Video>(request);
            _unitOfWork.Repository<Video>().AddEntity(videoEntity);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
                throw new Exception("Error al grabar video");

            _logger.LogInformation("Video: creado exitosamente");
            await SendEmail(videoEntity);

            return videoEntity.Id;
        }

        private async Task SendEmail(Video video)
        {
            var email = new Email();
            await _emailService.SendEmail(email);
        }
    }
}