using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto.TipoSeguimiento
{
    public class TipoSeguimientoDto
    {

  

        public int TipoSeguimientoIdInt { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime? FechaAltaDate { get; set; }
        public DateTime? FechaModificacionDate { get; set; }
        public int? UsuarioAltaInt { get; set; }
        public int? UsuarioModificaInt { get; set; }

        public bool? IsActiveBit { get; set; }
    }
}
