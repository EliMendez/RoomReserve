using AutoMapper;
using MediatR;
using RoomService.Application.Interface;
using RoomService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.UpdateRoom
{
    public class UpdateRoomHandler : IRequestHandler<UpdateRoomCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateRoomHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(request.RoomId);
            if (room == null)
                throw new KeyNotFoundException("La sala no existe en los registros.");

            var roomExists = await _unitOfWork.Rooms.ExistsByName(request.Name, request.RoomId);
            if (roomExists) 
                throw new Exception("El nombre de la sala ya existe en los registros.");

            room.Name = request.Name;
            room.Description = request.Description;
            room.Capacity = request.Capacity;
            room.PricePerHour = request.PricePerHour;

            await _unitOfWork.Rooms.Update(room);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}
