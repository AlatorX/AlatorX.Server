using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlatorX.Server.Management.Domain.Entities;
using AlatorX.Server.Management.Service.DTOs.Users;
using AutoMapper;

namespace AlatorX.Server.Management.Service.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserForCreationDto>().ReverseMap();
            CreateMap<User, UserForResultDto>().ReverseMap(); 
        }
    }
}