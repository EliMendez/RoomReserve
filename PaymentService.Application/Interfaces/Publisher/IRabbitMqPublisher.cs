using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Interfaces.Publisher
{
    public interface IRabbitMqPublisher
    {
        Task PublishAsync<T>(T message);
    }
}
