using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto.Usuario
{
    public class UsuarioUpdateDto
    {
        public int UsuarioIdInt { get; set; }
        public string UsuarioVar { get; set; }
        public string NombreVar { get; set; }
        public string PasswordVar { get; set; }
        public DateTime? FechaModificacionDate { get; set; }
        public string UsuarioModificaVar { get; set; }
    }
}
