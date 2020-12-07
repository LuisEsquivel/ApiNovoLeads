using System;
using System.Collections.Generic;

#nullable disable

namespace ApiNovoLeads
{
    public partial class Seguimiento
    {
        public int SeguimientoIdInt { get; set; }
        public int? ContactoIdInt { get; set; }
        public int? TipoSeguimientoIdInt { get; set; }
        public string ComentariosText { get; set; }
        public string SolucionVar { get; set; }
        public string TelefonoVar { get; set; }
        public string WhatsappVar { get; set; }
        public int? PctjedecierreInt { get; set; }
        public DateTime? FechaAltaDate { get; set; }
        public DateTime? FechaModificacionDate { get; set; }
        public int? UsuarioAltaInt { get; set; }
        public int? UsuarioModificaInt { get; set; }
        public bool? IsActiveBit { get; set; }

        public virtual Contacto ContactoIdIntNavigation { get; set; }
        public virtual TiposDeSeguimiento TipoSeguimientoIdIntNavigation { get; set; }
        public virtual Usuario UsuarioAltaIntNavigation { get; set; }
        public virtual Usuario UsuarioModificaIntNavigation { get; set; }
    }
}
