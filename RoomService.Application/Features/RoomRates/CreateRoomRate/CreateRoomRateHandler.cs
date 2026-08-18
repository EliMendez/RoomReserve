using AutoMapper;
using MediatR;
using RoomService.Application.Interface;
using RoomService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.RoomRates.CreateRoomRate
{
    public class CreateRoomRateHandler: IRequestHandler<CreateRoomRateCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateRoomRateHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateRoomRateCommand request, CancellationToken cancellationToken)
        {
            var roomExists = await _unitOfWork.Rooms.ExistsById(request.RoomId);
            if (!roomExists)
                throw new KeyNotFoundException("La sala no existe en los registros.");
            
            var hasOverlap = await _unitOfWork.RoomRates.HasOverlapAsync(request.RoomId, request.StartTime, request.EndTime);
            if (hasOverlap)
                throw new InvalidOperationException("El horario de la tarifa esta superpuesto con otro de esta habitación.");

            var roomRate = _mapper.Map<RoomRate>(request);
            await _unitOfWork.RoomRates.AddAsync(roomRate);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return roomRate.RoomRateId;
        }
    }
}
