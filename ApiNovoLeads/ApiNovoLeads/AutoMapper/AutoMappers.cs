using ApiNovoLeads.Dto;
using ApiNovoLeads.Dto.Seguimineto;
using ApiNovoLeads.Dto.TipoSeguimiento;
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
            CreateMap<Contacto, ContactoAddDto>().ReverseMap();
            CreateMap<Contacto, ContactoUpdateDto>().ReverseMap();

            CreateMap<Seguimiento, SeguimientoDto>().ReverseMap();
            CreateMap<Seguimiento, SeguimientoCreateDto>().ReverseMap();
            CreateMap<Seguimiento, SeguimientoUpdateDto>().ReverseMap();
                

            CreateMap<TiposDeSeguimiento, TipoSeguimientoDto>().ReverseMap();
            CreateMap<TiposDeSeguimiento, TipoSeguimientoAddDto >().ReverseMap();
            CreateMap<TiposDeSeguimiento, TipoSeguimientoUpdateDto >().ReverseMap();

        }
    }
}
