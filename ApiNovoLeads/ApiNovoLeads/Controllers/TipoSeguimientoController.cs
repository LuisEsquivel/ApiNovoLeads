using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiNovoLeads.Dto.TipoSeguimiento;
using ApiPlafonesWeb.Helpers;
using ApiPlafonesWeb.Interface;
using ApiPlafonesWeb.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiNovoLeads.Controllers
{
    /// <summary>
    /// Tipo De Seguimiento Controller
    /// </summary>
    [Route("api/tiposdeseguimiento/")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiTiposDeSeguimiento")]
    public class TipoSeguimientoController : ControllerBase
    {

        private IGenericRepository<TiposDeSeguimiento> repository;
        private IMapper mapper;
        private Response response;

        public TipoSeguimientoController(ApplicationDbContext context, IMapper _mapper)
        {
            this.mapper = _mapper;
            this.repository = new GenericRepository<TiposDeSeguimiento>(context);
            this.response = new Response();
        }


        /// <summary>
        /// Tipos de seguimiento Get
        /// </summary>
        /// <returns>lista de tipos de seguimiento</returns>
        [HttpGet("get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {

            var list = repository.GetAll();

            var listDto = new List<TipoSeguimientoDto>();

            foreach (var row in list)
            {
                listDto.Add(mapper.Map<TipoSeguimientoDto>(row));
            }

            return Ok(response.ResponseValues(this.Response.StatusCode, listDto));

        }



        /// <summary>
        /// Obtener tipo de seguimiento por el Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>StatusCode 200</returns>
        [HttpGet("GetById/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById(int Id)
        {
            return Ok(this.response.ResponseValues(this.Response.StatusCode, mapper.Map<TipoSeguimientoDto>(repository.GetById(Id))));
        }





        /// <summary>
        /// Agregar una nuevo tipo de seguimiento
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] TipoSeguimientoAddDto dto)
        {
            if (dto == null)
            {
                return BadRequest(StatusCodes.Status406NotAcceptable);
            }


            if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar ))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Tipo De Seguimiento Ya Existe!!"));
            }

            var tiposeguimiento = mapper.Map<TiposDeSeguimiento>(dto);

            if (!repository.Add(tiposeguimiento))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {dto.DescripcionVar}"));
            }

            return Ok(
                         response.ResponseValues(this.Response.StatusCode,
                                                 mapper.Map<TipoSeguimientoAddDto >(repository.GetById(tiposeguimiento.TipoSeguimientoIdInt))
                                               )
                      );
        }



        /// <summary>
        /// Actualizar tipo de seguimiento
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] TipoSeguimientoUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(StatusCodes.Status406NotAcceptable);
            }

            if (repository.Exist(x => x.DescripcionVar == dto.DescripcionVar && x.TipoSeguimientoIdInt != dto.TipoSeguimientoIdInt))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Tipo De Seguimiento Ya Existe!!"));
            }

            var tiposeguimiento = mapper.Map<TiposDeSeguimiento>(dto);

            if (!repository.Update(tiposeguimiento, tiposeguimiento.TipoSeguimientoIdInt))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.DescripcionVar}"));
            }


            return Ok(
                       response.ResponseValues(this.Response.StatusCode,
                                               mapper.Map<TipoSeguimientoUpdateDto>(repository.GetById(tiposeguimiento.TipoSeguimientoIdInt))
                                             )
                    );

        }



        /// <summary>
        /// Eliminar un tipo de seguimiento por Id
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


            if (repository.Exist(x => x.TipoSeguimientoIdInt == Id))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El tipo de seguimiento con Id: {Id} No existe"));
            }

            var row = repository.GetById(Id);

            var tiposeguimiento = mapper.Map<TipoSeguimientoDto>(row);

            if (!repository.Delete(tiposeguimiento))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {tiposeguimiento.DescripcionVar}"));

            }


            return Ok(response.ResponseValues(this.Response.StatusCode));
        }

    }
}
