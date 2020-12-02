using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPlafonesWeb.Helpers;
using ApiPlafonesWeb.Interface;
using ApiPlafonesWeb.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNovoLeads.Controllers
{
    [Route("api/contactos/")]
    [ApiController]
    public class ContactoController : ControllerBase
    {
        private IGenericRepository<Contacto> repository;
        private IMapper mapper;
        private Response response; 

        public ContactoController(ApplicationDbContext context, IMapper _mapper)
        {
            this.mapper = _mapper;
            this.repository = new GenericRepository<Contacto>(context);
            this.response = new Response();
        }


    }
}
