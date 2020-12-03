using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto.TipoSeguimiento
{
    public class TipoSeguimientoUpdateDto
    {


        [Required(ErrorMessage = "El TipoSeguimientoIdInt es Requerido")]
        public int TipoSeguimientoIdInt { get; set; }

        [Required(ErrorMessage = "La DescripcionVar es Requerida")]
        public string DescripcionVar { get; set; }
        public DateTime? FechaModificacionDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El UsuarioModificaInt es Requerido")]
        public int? UsuarioModificaInt { get; set; }

    }
}
