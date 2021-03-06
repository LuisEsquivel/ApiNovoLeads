﻿using ApiNovoLeads.Dto.Seguimineto;
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
    [Route("api/seguimientos")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiSeguimientos")]
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


        /// <summary>
        /// Obtener Seguimientos
        /// </summary>
        /// <returns>lista de seguimientos</returns>
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var list = repository.GetAll();

            var listDto = new List<SeguimientoDto>();

            foreach (var item in list)
            {
                listDto.Add(mapper.Map<SeguimientoDto>(item));
            }

            return Ok(response.ResponseValues(this.Response.StatusCode, listDto));
        }


        /// <summary>
        /// Obtener seguimiento por el Id
        /// </summary>
        /// <param name="seguimientoId"></param>
        /// <returns>StatusCode 200</returns>
        [HttpGet("{seguimientoId:int}", Name = "GetById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById(int seguimientoId)
        {
            var itemSeguimiento = repository.GetById(seguimientoId);

            if (itemSeguimiento == null)
            {
                return NotFound();
            }

            var itemSeguimientoDto = mapper.Map<SeguimientoDto>(itemSeguimiento);
            return Ok(itemSeguimientoDto);
        }


        /// <summary>
        /// Agregar un nuevo seguimiento
        /// </summary>
        /// <param name="DTO"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] SeguimientoCreateDto DTO)
        {
            if (DTO == null)
            {
                return BadRequest(ModelState);
            }

            if (repository.Exist(x => x.SolucionVar == DTO.SolucionVar))
            {
                ModelState.AddModelError("", "La solucion ya existe");
                return StatusCode(404, ModelState);
            }

            var seguimiento = mapper.Map<Seguimiento>(DTO);

            if (!repository.Add(seguimiento))
            {
                ModelState.AddModelError("", $"Algo salio mal guardando el registro{seguimiento.SolucionVar}");
                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetById", new { seguimientoId = seguimiento.SeguimientoIdInt }, seguimiento);

        }


        /// <summary>
        /// Actualizar seguimiento
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPost("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] Seguimiento dto)
        {
            if (dto == null)
            {
                return BadRequest(StatusCodes.Status406NotAcceptable);
            }

            if (repository.Exist(x => x.SolucionVar == dto.SolucionVar && x.SeguimientoIdInt != dto.SeguimientoIdInt))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "La solucion ya existe!!"));
            }

            var seguimiento = mapper.Map<Seguimiento>(dto);
            var update = repository.GetByValues(x => x.SeguimientoIdInt == dto.SeguimientoIdInt).FirstOrDefault();
            seguimiento.FechaAltaDate = update.FechaAltaDate;
            seguimiento.UsuarioAltaInt = update.UsuarioAltaInt;

            if (!repository.Update(seguimiento, seguimiento.SeguimientoIdInt))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.SolucionVar}"));
            }


            return Ok(
                       response.ResponseValues(this.Response.StatusCode,
                                               mapper.Map<SeguimientoUpdateDto>(repository.GetById(seguimiento.SeguimientoIdInt))
                                             )
                    );

        }

        /// <summary>
        /// Eliminar un seguimiento por id
        /// </summary>
        /// <param name="seguimientoId"></param>
        /// <returns>StatusCode 200</returns>
        [HttpDelete("{seguimientoId:int}", Name = "Delete")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int seguimientoId)
        {
            if (!repository.ExistById(seguimientoId))
            {
                return NotFound();
            }

            var seguimiento = repository.GetById(seguimientoId);

            if (!repository.Delete(seguimiento))
            {
                ModelState.AddModelError("", $"Algo salio mal al eliminar el registro{seguimiento.SolucionVar}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
