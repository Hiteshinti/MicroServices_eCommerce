using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.IRepository;
using eCommerce.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{

    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;

        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
             _userRepository = userRepository; 
             _mapper = mapper;   
        }
        public async Task<AuthenticationDto?>Login(LoginDto loginDto)
        {
            ApplicationUser? user = await _userRepository.GetUserByEmailAndPassword(loginDto.Email, loginDto.Password);

            if (user == null)
                return null;

            return _mapper.Map<AuthenticationDto>(user) with { Success = true, Token = "token" };
            
        }

        public async Task<AuthenticationDto?>Register(RegisterDto registerDto)
        {
           ApplicationUser? user= _mapper.Map<ApplicationUser>(registerDto); 
           ApplicationUser? registeredUser = await _userRepository.AddUser(user);

            if (registeredUser == null)
                return null;

            return _mapper.Map<AuthenticationDto>(registeredUser) with { Success = true, Token = "token" };
        }
    }
}

  
