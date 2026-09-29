using PaymentService.Application.Interfaces;
using PaymentService.Domain.Entities;
using PaymentService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentService.Application.Services
{
    public class PaymentProcessor: IPaymentProcessor
    {
        public async Task<Payment> ProcessAsync(Payment payment)
        {
            // Simulamos el procesamiento del pago
            await Task.Delay(1000);

            // Por ahora siempre aprobamos el pago
            payment.Status = PaymentStatus.APPROVED.ToString();

            return payment;
        }
    }
}
