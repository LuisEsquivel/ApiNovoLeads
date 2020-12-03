﻿using ApiNovoLeads.Dto;
using ApiNovoLeads.Dto.Seguimineto;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.AutoMapper
{
    public class AutoMappers : Profile
    {

        public AutoMappers()
        {

            CreateMap<Contacto, ContactoDto>().ReverseMap();
            CreateMap<Seguimiento, SeguimientoDto>().ReverseMap();
            CreateMap<Seguimiento, SeguimientoCreateDto>().ReverseMap();
            CreateMap<Seguimiento, SeguimientoUpdateDto>().ReverseMap();
                
        }
    }
}
