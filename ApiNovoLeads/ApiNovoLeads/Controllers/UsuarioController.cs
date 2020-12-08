using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ApiNovoLeads.Dto.Usuario;
using ApiPlafonesWeb.Helpers;
using ApiPlafonesWeb.Interface;
using ApiPlafonesWeb.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ApiNovoLeads.Controllers
{
    [Route("api/usuario")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "ApiUsuario")]
    public class UsuarioController : ControllerBase
    {

        private IGenericRepository<Usuario> repository;
        private IMapper mapper;
        private IConfiguration config;
        private Response response;

        public UsuarioController(IMapper _mapper, ApplicationDbContext context, IConfiguration _congig)
        {
            this.mapper = _mapper;
            this.repository = new GenericRepository<Usuario>(context);
            this.config = _congig;
            this.response = new Response();
        }



        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        /// <returns>StatusCode 200</returns>
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var dto = repository.GetAll();
            var user = new List<UsuarioDto>();

            foreach (var row in dto)
            {
                user.Add(mapper.Map<UsuarioDto>(row));
            }


            return Ok(this.response.ResponseValues(this.Response.StatusCode, user));
        }




        /// <summary>
        /// Obtener usuario por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("GetById/{Id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById(int Id)
        {
            return Ok(this.response.ResponseValues(this.Response.StatusCode, mapper.Map<UsuarioDto>(repository.GetById(Id))));
        }




        /// <summary>
        /// Registro de usuario
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPost("Add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Add([FromBody] UsuarioAddDto dto)
        {

            if (dto == null)
            {
                return BadRequest(StatusCodes.Status406NotAcceptable);
            }

            var u = mapper.Map<Usuario>(dto);

            if (!repository.Add(u))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {u.UsuarioVar}"));
            }

            return Ok(this.response.ResponseValues(this.Response.StatusCode, mapper.Map<UsuarioDto>(this.repository.GetById(u.UsuarioIdInt))));
        }




        /// <summary>
        /// Login con UsuarioVar y PasswordVar
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UsuarioLoginDto dto)
        {
            if (dto == null)
            {
                return Unauthorized();
            }


            if (!repository.Exist(x=> x.UsuarioVar == dto.UsuarioVar && x.PasswordVar == dto.PasswordVar && x.IsActiveBit == true))
            {
                return Unauthorized();
            }

            var user = repository.GetByValues(x => x.UsuarioVar == dto.UsuarioVar && x.PasswordVar == dto.PasswordVar).FirstOrDefault();

            var claims = new[]
            {

                new Claim(ClaimTypes.NameIdentifier, user.UsuarioIdInt.ToString()),
                new Claim(ClaimTypes.Name, user.UsuarioVar.ToString())

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("AppSettings:Token").Value));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            }); ;
        }


        private bool ValidatePassword(string password, byte[] passwordHash)
        {

                var hmac = new System.Security.Cryptography.HMACSHA512();
                var hashComputado = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));


                for (int i = 0; i < hashComputado.Length; i++)
                {
                    if (hashComputado[i] != passwordHash[i]) { return false; }
                }

        
            return true;
        }




        private void CrearPassword(string password, out byte[] passwordHash)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }



        /// <summary>
        /// Actualizar usuario
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPut("Update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update([FromBody] UsuarioUpdateDto dto)
        {
            if (dto == null)
            {
                return BadRequest(StatusCodes.Status406NotAcceptable);
            }

            if (repository.Exist(x => x.UsuarioVar == dto.UsuarioVar && x.UsuarioIdInt != dto.UsuarioIdInt))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, "El Usuario Ya Existe!!"));
            }

            var usuario = mapper.Map<Usuario>(dto);
            var update = repository.GetByValues(x => x.UsuarioIdInt == dto.UsuarioIdInt).FirstOrDefault();
            usuario.FechaAltaDate = update.FechaAltaDate;

            if (!repository.Update(usuario, usuario.UsuarioIdInt))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al actualizar el registro: {dto.NombreVar}"));
            }


            return Ok(
                       response.ResponseValues(this.Response.StatusCode,
                                               mapper.Map<Usuario>(repository.GetById(usuario.UsuarioIdInt))
                                             )
                    );

        }


        /// <summary>
        /// Eliminar un usuario por Id
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


            if (repository.Exist(x => x.UsuarioIdInt == Id))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status406NotAcceptable, null, $"El usuario con Id: {Id} No existe"));
            }

            var row = repository.GetById(Id);

            var usuario = mapper.Map<Usuario>(row);

            if (!repository.Delete(usuario))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal al eliminar el registro: {usuario.NombreVar}"));

            }


            return Ok(response.ResponseValues(this.Response.StatusCode));
        }


    }
}
