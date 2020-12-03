using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto.TipoSeguimiento
{
    public class TipoSeguimientoUpdateDto
    {

   
        public int TipoSeguimientoIdInt { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime? FechaModificacionDate { get; set; }
        public int? UsuarioModificaInt { get; set; }

    }
}
