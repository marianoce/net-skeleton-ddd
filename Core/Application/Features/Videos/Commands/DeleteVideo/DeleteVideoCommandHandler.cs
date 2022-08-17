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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public DeleteVideoCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteVideoCommand request, CancellationToken cancellationToken)
        {
            var videoToDelete = await _unitOfWork.Repository<Video>().GetByIdAsync(request.Id);

            if (videoToDelete == null)
            {
                _logger.LogError("No se encontro el video id: " + request.Id);
                throw new NotFoundException(nameof(Video), request.Id.ToString());
            }

            _unitOfWork.Repository<Video>().DeleteEntity(videoToDelete);
            var result = await _unitOfWork.Complete();

            if (result <= 0)
                throw new Exception("Error al borrar la entidad");

            _logger.LogInformation($"La informacion fue exitosa actualizando el video: { request.Id.ToString() }");

            return Unit.Value;
        }
    }
}