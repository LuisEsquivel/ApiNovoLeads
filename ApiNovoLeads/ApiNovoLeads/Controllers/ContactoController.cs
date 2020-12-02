using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiNovoLeads.Dto;
using ApiPlafonesWeb.Helpers;
using ApiPlafonesWeb.Interface;
using ApiPlafonesWeb.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNovoLeads.Controllers
{
    /// <summary>
    /// Contactos Controller
    /// </summary>
    [Route("api/contactos/")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiContactos")]
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


        /// <summary>
        /// Contactos Get
        /// </summary>
        /// <returns>lista de contactos</returns>
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {

            var list = repository.GetAll();

            var listDto = new List<ContactoDto>();

            foreach(var row in list)
            {
                listDto.Add(mapper.Map<ContactoDto>(row));
            }

            return Ok(response.ResponseValues(this.Response.StatusCode, listDto));

        }



        /// <summary>
        /// Obtener la categoría por el Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>StatusCode 200</returns>
        [HttpGet("GetById/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById(int Id)
        {
            return Ok(this.response.ResponseValues(this.Response.StatusCode, mapper.Map<ContactoDto>(repository.GetById(Id))));
        }





        /// <summary>
        /// Agregar una nuevo contacto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] ContactoAddDto dto)
        {
            if (dto == null)
            {
                return BadRequest(StatusCodes.Status406NotAcceptable);
            }


            if (repository.Exist(x => x.NombreVar == dto.NombreVar))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Contacto Ya Existe!!"));
            }

            var contacto = mapper.Map<Contacto>(dto);

            if (!repository.Add(contacto))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {dto.NombreVar}"));
            }

            return Ok(
                         response.ResponseValues(this.Response.StatusCode,
                                                 mapper.Map<ContactoAddDto>(repository.GetById(contacto.ContactoIdInt))
                                               )
                      );
        }



        /// <summary>
        /// Actualizar contacto
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] ContactoUpdateDto  dto)
        {
            if (dto == null)
            {
                return BadRequest(StatusCodes.Status406NotAcceptable);
            }

            if (repository.Exist(x => x.NombreVar == dto.NombreVar && x.ContactoIdInt != dto.ContactoIdInt))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Contacto Ya Existe!!"));
            }

            var contacto = mapper.Map<Contacto>(dto);

            if (!repository.Update(contacto, contacto.ContactoIdInt))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.NombreVar}"));
            }


            return Ok(
                       response.ResponseValues(this.Response.StatusCode,
                                               mapper.Map<ContactoUpdateDto>(repository.GetById(contacto.ContactoIdInt))
                                             )
                    );

        }



        /// <summary>
        /// Eliminar un contacto por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>StatusCode 200</returns>
        [HttpDelete("Delete/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El parámetro (Id) es obligatorio"));
            }


            if (repository.Exist(x => x.ContactoIdInt == Id))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El contacto con Id: {Id} No existe"));
            }

            var row = repository.GetById(Id);

            var contacto = mapper.Map<Contacto>(row);

            if (!repository.Delete(contacto))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {contacto.NombreVar}"));

            }


            return Ok(response.ResponseValues(this.Response.StatusCode));
        }

    }
}
