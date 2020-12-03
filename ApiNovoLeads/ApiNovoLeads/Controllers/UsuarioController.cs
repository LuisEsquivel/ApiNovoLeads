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

            byte[] passwordHash;
            CrearPassword(dto.PasswordVar, out passwordHash);


            var user = new UsuarioDto();
            user.PasswordVar = passwordHash.ToString();
            user.UsuarioVar = dto.UsuarioVar;

            var u = mapper.Map<Usuario>(user);

            if (!repository.Add(u))
            {
                return BadRequest(this.response.ResponseValues(StatusCodes.Status500InternalServerError, null, $"Algo salió mal guardar el registro: {user.UsuarioVar}"));
            }

            return Ok(this.response.ResponseValues(this.Response.StatusCode, mapper.Map<UsuarioDto>(this.repository.GetById(u.UsuarioIdInt))));
        }




        /// <summary>
        /// Login con Email y Password
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>StatusCode 200</returns>
        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UsuarioDto dto)
        {
            if (dto == null)
            {
                return Unauthorized();
            }

            var user = repository.GetByValues(x => x.UsuarioVar == dto.UsuarioVar).FirstOrDefault();
            Byte[] passwordHash = Encoding.ASCII.GetBytes(user.PasswordVar);

            if (!ValidatePassword(dto.PasswordVar, passwordHash))
            {
                return Unauthorized();
            }

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



    }
}
