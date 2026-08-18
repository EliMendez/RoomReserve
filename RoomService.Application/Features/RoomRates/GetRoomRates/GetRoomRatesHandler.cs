using AutoMapper;
using MediatR;
using RoomService.Application.Dto.RoomRates;
using RoomService.Application.Interfaces.RoomRates;
using RoomService.Application.Interfaces.Rooms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomService.Application.Features.RoomRates.GetRoomRates
{
    public class GetRoomRatesHandler : IRequestHandler<GetRoomRatesQuery, IEnumerable<RoomRateDto>>
    {
        private readonly IRoomRateQueryRepository _rateQueryRepository;
        private readonly IRoomQueryRepository _roomQueryRepository;
        public GetRoomRatesHandler(IRoomRateQueryRepository rateQueryRepository, IRoomQueryRepository roomQueryRepository)
        {
            _rateQueryRepository = rateQueryRepository;
            _roomQueryRepository = roomQueryRepository;
        }

        public async Task<IEnumerable<RoomRateDto>> Handle(GetRoomRatesQuery request, CancellationToken cancellationToken)
        {
            var roomExists = await _roomQueryRepository.ExistsById(request.RoomId);
            if (!roomExists)
                throw new KeyNotFoundException("La sala no existe en los registros.");

            return await _rateQueryRepository.GetAllAsync(request.RoomId);
        }
    }
}
