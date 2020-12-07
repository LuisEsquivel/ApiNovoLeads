using System;
using System.Collections.Generic;

#nullable disable

namespace ApiNovoLeads
{
    public partial class TiposDeSeguimiento
    {
        public TiposDeSeguimiento()
        {
            Seguimientos = new HashSet<Seguimiento>();
        }

        public int TipoSeguimientoIdInt { get; set; }
        public string DescripcionVar { get; set; }
        public DateTime? FechaAltaDate { get; set; }
        public DateTime? FechaModificacionDate { get; set; }
        public int? UsuarioAltaInt { get; set; }
        public int? UsuarioModificaInt { get; set; }
        public bool? IsActiveBit { get; set; }

        public virtual Usuario UsuarioAltaIntNavigation { get; set; }
        public virtual Usuario UsuarioModificaIntNavigation { get; set; }
        public virtual ICollection<Seguimiento> Seguimientos { get; set; }
    }
}
