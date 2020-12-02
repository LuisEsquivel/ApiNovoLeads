using ApiNovoLeads.Dto.Seguimineto;
using ApiPlafonesWeb.Helpers;
using ApiPlafonesWeb.Interface;
using ApiPlafonesWeb.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Controllers
{
    [Route("api/Seguimiento")]
    [ApiController]
    public class SeguimientoController : Controller
    {
        private IGenericRepository<Seguimiento> repository;
        private IMapper mapper;
        private Response response;

        public SeguimientoController(IMapper _mapper, ApplicationDbContext context)
        {
            this.repository = new GenericRepository<Seguimiento>(context);
            this.mapper = _mapper;
            response = new Response();
        }


        [HttpGet]
        public IActionResult GetSeguimientos()
        {
            var list = repository.GetAll();

            var listDto = new List<SeguimientoDto>();

            foreach (var row in list)
            {
                listDto.Add(mapper.Map<SeguimientoDto>(row));
            }

            return Ok(this.response.ResponseValues(this.Response.StatusCode, listDto));
        }


        [HttpGet("{seguimientoId:int}", Name = "GetSeguimiento")]
        public IActionResult GetSeguimiento(int seguimientoId)
        {
            var itemSeguimiento = repository.GetById(seguimientoId);

            if (itemSeguimiento == null)
            {
                return NotFound();
            }

            var itemSeguimientoDto = mapper.Map<SeguimientoDto>(itemSeguimiento);
            return Ok(itemSeguimientoDto);
        }

        public IActionResult CrearSeguimiento([FromForm] SeguimientoDto seguimientoDto)
        {
            if (seguimientoDto == null)
            {
                return BadRequest(ModelState);
            }

            if (repository.Exist(x => x.SolucionVar == seguimientoDto.SolucionVar))
            {
                ModelState.AddModelError("", "La solucion ya existe");
                return StatusCode(404, ModelState);
            }

            var seguimiento = mapper.Map<Seguimiento>(seguimientoDto);

            if (!repository.Add(seguimiento))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro{seguimiento.SolucionVar}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetSeguimiento", new { seguimientoId = seguimiento.SeguimientoIdInt }, seguimiento);

        }
    }
}
