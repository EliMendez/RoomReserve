using BookingService.Application.Dto.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Interfaces.ServiceClient
{
    public interface IEmailServiceClient
    {
        Task<int> CreateEmail(CreateEmailDto createEmailDto);
    }
}
