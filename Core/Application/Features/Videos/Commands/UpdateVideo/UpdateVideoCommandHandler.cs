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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public UpdateVideoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateVideoCommand request, CancellationToken cancellationToken)
        {
            Video videoToUpdate = await _unitOfWork.Repository<Video>().GetByIdAsync(request.Id);
            if (videoToUpdate == null)
            {
                _logger.LogError("No se encontro el video id: " + request.Id);
                throw new NotFoundException(nameof(Video), request.Id.ToString());
            }

            _mapper.Map(request, videoToUpdate, typeof(UpdateVideoCommand), typeof(Video));

            _unitOfWork.Repository<Video>().UpdateEntity(videoToUpdate);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
                throw new Exception("No se pudo actualizar el video");

            _logger.LogInformation($"La informacion fue exitosa actualizando el video: { request.Id.ToString() }");
            return Unit.Value;
        }
    }
}