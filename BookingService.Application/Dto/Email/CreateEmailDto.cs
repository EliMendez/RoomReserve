using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Dto.Email
{
    public record CreateEmailDto
    (
        string To,
        string Subject,
        string Body
    );
}
