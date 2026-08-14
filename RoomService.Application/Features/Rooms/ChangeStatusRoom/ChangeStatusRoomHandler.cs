using AutoMapper;
using MediatR;
using RoomService.Application.Interface;
using RoomService.Domain.Entities;
using RoomService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.ChangeStatusRoom
{
    public class ChangeStatusRoomHandler : IRequestHandler<ChangeStatusRoomCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ChangeStatusRoomHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(ChangeStatusRoomCommand request, CancellationToken cancellationToken)
        {
            var room = await _unitOfWork.Rooms.GetByIdAsync(request.RoomId);
            if (room == null)
                throw new KeyNotFoundException("La sala no existe en los registros.");

            if (room.Status == RoomStatus.ACTIVE.ToString())
                room.Status = RoomStatus.INACTIVE.ToString();
            else
                room.Status = RoomStatus.ACTIVE.ToString();

            _unitOfWork.Rooms.Update(room);
            var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

            return result > 0;
        }
    }
}
