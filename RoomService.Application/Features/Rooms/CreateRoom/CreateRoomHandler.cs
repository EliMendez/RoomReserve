using AutoMapper;
using MediatR;
using RoomService.Application.Interface;
using RoomService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.Rooms.CreateRoom
{
    public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreateRoomHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomExists = await _unitOfWork.Rooms.ExistsByName(request.Name);
            if (roomExists) 
            {
                throw new Exception("El nombre de la sala ya existe en los registros.");
            }

            var room = _mapper.Map<Room>(request);
            await _unitOfWork.Rooms.AddAsync(room);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return room.RoomId;
        }
    }
}
