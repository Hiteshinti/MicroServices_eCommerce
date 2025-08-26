using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Mappers
{
    public class ApplicationUserMapping: AutoMapper.Profile
    {
       public ApplicationUserMapping() 
       {
            CreateMap<ApplicationUser, AuthenticationDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest=> dest.UserName, opt=>opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
                .ForMember(dest => dest.Token, opt => opt.Ignore())
                .ForMember(dest => dest.Success, opt => opt.Ignore());

            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender));
                


        }  
    }
}
