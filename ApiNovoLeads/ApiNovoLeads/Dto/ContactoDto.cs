using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto
{
    public class ContactoDto
    {

        public ContactoDto()
        {
            Seguimientos = new HashSet<Seguimiento>();
        }

        public int ContactoIdInt { get; set; }
        public string NombreVar { get; set; }
        public string SolucionVar { get; set; }
        public string TelefonoVar { get; set; }
        public string WhatsappVar { get; set; }
        public int? PctjedecierreInt { get; set; }
        public bool? EsIntegradorBit { get; set; }
        public DateTime? FechaAltaDate { get; set; }
        public DateTime? FechaModificacionDate { get; set; }
        public int? UsuarioAltaInt { get; set; }
        public int? UsuarioModificaInt { get; set; }

        public virtual Usuario UsuarioAltaIntNavigation { get; set; }
        public virtual Usuario UsuarioModificaIntNavigation { get; set; }
        public virtual ICollection<Seguimiento> Seguimientos { get; set; }

    }
}
