using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto
{
    public class ContactoUpdateDto
    {

        [Required(ErrorMessage = "El ContactoIdInt es Requerido")]
        public int ContactoIdInt { get; set; }

        [Required(ErrorMessage = "El NombreVar es Requerido")]
        public string NombreVar { get; set; }

        [Required(ErrorMessage = "La SolucionVar es Requerida")]
        public string SolucionVar { get; set; }

        [Required(ErrorMessage = "El TelefonoVar es Requerido")]
        public string TelefonoVar { get; set; }

        [Required(ErrorMessage = "El WhatsappVar es Requerido")]
        public string WhatsappVar { get; set; }

        [Required(ErrorMessage = "El PctjedecierreInt es Requerido")]
        public int? PctjedecierreInt { get; set; }

        [Required(ErrorMessage = "El EsIntegradorBit es Requerido")]
        public bool? EsIntegradorBit { get; set; }
        public DateTime? FechaModificacionDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El UsuarioModificaInt es Requerido")]
        public int? UsuarioModificaInt { get; set; }

    }
}
