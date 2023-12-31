﻿using AutoMapper;
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
            CreateMap<ActorUpdateDto, Actor>().ReverseMap();

            CreateMap<Producer, ProducerDto>();
            CreateMap<ProducerCreateDto, Producer>();
            CreateMap<ProducerUpdateDto, Producer>().ReverseMap();

            CreateMap<Cinema, CinemaDto>();
            CreateMap<CinemaCreateDto, Cinema>();
            CreateMap<CinemaUpdateDto, Cinema>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>().ReverseMap();

            CreateMap<Movie, MovieDto>();
            CreateMap<MovieCreateDto, Movie>();
            CreateMap<MovieUpdateDto, Movie>().ReverseMap();

        }
    }
}
