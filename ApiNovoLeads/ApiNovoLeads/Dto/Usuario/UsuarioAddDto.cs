using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto.Usuario
{
    public class UsuarioAddDto
    {   
        public int UsuarioIdInt { get; set; }

        [Required(ErrorMessage = "El UsuarioVar es Requerido")]
        public string UsuarioVar { get; set; }

        [Required(ErrorMessage = "El NombreVar es Requerido")]
        public string NombreVar { get; set; }

        [Required(ErrorMessage = "El PasswordVar es Requerido")]
        public string PasswordVar { get; set; }
        public DateTime? FechaAltaDate { get; set; }

        public bool? IsActiveBit { get; set; }
    }
}
