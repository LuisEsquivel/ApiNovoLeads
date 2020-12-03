using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto.TipoSeguimiento
{
    public class TipoSeguimientoAddDto
    {

  
        public int TipoSeguimientoIdInt { get; set; }

        [Required(ErrorMessage = "La DescripcionVar es Requerida")]
        public string DescripcionVar { get; set; }
        public DateTime? FechaAltaDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El UsuarioAltaInt es Requerido")]
        public int? UsuarioAltaInt { get; set; }
 

    }
}
