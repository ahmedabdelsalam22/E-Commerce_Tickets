using AutoMapper;
using eTickets.Models;
using eTickets.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.Utility
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Actor,ActorDto>();
            CreateMap<ActorCreateDto, Actor>();
        }
    }
}
