using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto.Usuario
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "El UsuarioVar es Requerido")]
        public string UsuarioVar { get; set; }

        [Required(ErrorMessage = "La PasswordVar es Requerido")]
        public string PasswordVar { get; set; }

        public bool? IsActiveBit { get; set; }
    }
}
