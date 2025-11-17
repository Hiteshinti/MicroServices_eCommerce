using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.IServices
{
    public interface IUserService
    {
       Task<AuthenticationDto?> Login(LoginDto loginDto);   

       Task<AuthenticationDto?> Register(RegisterDto registerDto);   


    }
}
