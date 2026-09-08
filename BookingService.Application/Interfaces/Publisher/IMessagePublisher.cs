using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces.Publisher
{
    public interface IMessagePublisher
    {
        Task PublishAsync<T>(T message);
    }
}
