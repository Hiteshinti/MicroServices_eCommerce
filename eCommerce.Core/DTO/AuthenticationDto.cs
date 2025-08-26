using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.DTO
{
    public record AuthenticationDto(Guid UserId,
    string? Email,
    string? UserName,
    string? Gender,
    string? Token,
    bool? Success
    )
    {
        public AuthenticationDto() : this(default, default, default, default, default, default)
        {

        }
       
    }
}
