﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNovoLeads.Dto.Seguimineto
{
    public class SeguimientoCreateDto
    {
        public int? ContactoIdInt { get; set; }
        public int? TipoSeguimientoIdInt { get; set; }
        public string ComentariosText { get; set; }
        public string SolucionVar { get; set; }
        public string TelefonoVar { get; set; }
        public string WhatsappVar { get; set; }
        public int? PctjedecierreInt { get; set; }
        public DateTime? FechaAltaDate { get; set; }
        public int? UsuarioAltaInt { get; set; }
    }
}