using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.IRepository;
using eCommerce.Core.IServices;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


namespace eCommerce.Core.Services
{

    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
             _userRepository = userRepository; 
            _configuration = configuration;
             _mapper = mapper;   
        }
        public async Task<AuthenticationDto?>Login(LoginDto loginDto)
        {
            ApplicationUser? user = await _userRepository.GetUserByEmailAndPassword(loginDto.Email??"", loginDto.Password??"");

            if (user == null)
                return null;

            var token = GenerateToken(user.UserId.ToString(),user.UserName??"");
            return _mapper.Map<AuthenticationDto>(user) with { Success = true, Token = token };
            
        }

        public async Task<AuthenticationDto?>Register(RegisterDto registerDto)
        {
           ApplicationUser? user= _mapper.Map<ApplicationUser>(registerDto); 
           ApplicationUser? registeredUser = await _userRepository.AddUser(user);

            if (registeredUser == null)
                return null;

            return _mapper.Map<AuthenticationDto>(registeredUser) with { Success = true, Token = "token" };
        }

        public string GenerateToken(string userId, string userName)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var audiences = jwtSettings.GetSection("Audience")
                          .GetChildren()
                          .Select(x => x.Value)
                          .ToArray();
            // 1️⃣ Create claims (payload data)
            var claims = new List<Claim>
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            //new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            //foreach (var aud in audiences)
            //{
            //    claims.Add(new Claim(JwtRegisteredClaimNames.Aud, aud));
            //}

            // 2️⃣ Get key and signing credentials
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            

            // 3️⃣ Create the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience:audiences.First(),
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
                signingCredentials: creds
            );
            if (audiences.Length > 1)
            {
                token.Payload["aud"] = audiences; // ensures "aud": [ "a", "b" ] form
            }

            // 4️⃣ Write token as string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}

  
