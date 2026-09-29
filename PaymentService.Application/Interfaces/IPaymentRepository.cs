using PaymentService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Interfaces
{
    public interface IPaymentRepository
    {
        Task AddAsync(Payment payment);

        Task UpdateAsync(Payment payment);
    }
}
