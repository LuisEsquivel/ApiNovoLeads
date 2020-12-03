using ApiNovoLeads.Dto;
<<<<<<< HEAD
using ApiNovoLeads.Dto.Seguimineto;
=======
using ApiNovoLeads.Dto.TipoSeguimiento;
>>>>>>> aa5f06cdeaf1b5889b30bff6415f74cb30805667
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
<<<<<<< HEAD
            CreateMap<Seguimiento, SeguimientoDto>().ReverseMap();
            CreateMap<Seguimiento, SeguimientoCreateDto>().ReverseMap();
            CreateMap<Seguimiento, SeguimientoUpdateDto>().ReverseMap();
                
=======
            CreateMap<Contacto, ContactoAddDto>().ReverseMap();
            CreateMap<Contacto, ContactoUpdateDto>().ReverseMap();

            CreateMap<TiposDeSeguimiento, TipoSeguimientoDto>().ReverseMap();
            CreateMap<TiposDeSeguimiento, TipoSeguimientoAddDto >().ReverseMap();
            CreateMap<TiposDeSeguimiento, TipoSeguimientoUpdateDto >().ReverseMap();

>>>>>>> aa5f06cdeaf1b5889b30bff6415f74cb30805667
        }
    }
}
