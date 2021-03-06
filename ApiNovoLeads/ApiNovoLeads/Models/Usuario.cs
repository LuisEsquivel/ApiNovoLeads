﻿using System;
using System.Collections.Generic;

#nullable disable

namespace ApiNovoLeads
{
    public partial class Usuario
    {
        public Usuario()
        {
            ContactoUsuarioAltaIntNavigations = new HashSet<Contacto>();
            ContactoUsuarioModificaIntNavigations = new HashSet<Contacto>();
            SeguimientoUsuarioAltaIntNavigations = new HashSet<Seguimiento>();
            SeguimientoUsuarioModificaIntNavigations = new HashSet<Seguimiento>();
            TiposDeSeguimientoUsuarioAltaIntNavigations = new HashSet<TiposDeSeguimiento>();
            TiposDeSeguimientoUsuarioModificaIntNavigations = new HashSet<TiposDeSeguimiento>();
        }

        public int UsuarioIdInt { get; set; }
        public string UsuarioVar { get; set; }
        public string NombreVar { get; set; }
        public string PasswordVar { get; set; }
        public DateTime? FechaAltaDate { get; set; }
        public DateTime? FechaModificacionDate { get; set; }
        public string UsuarioModificaVar { get; set; }
        public bool? IsActiveBit { get; set; }

        public virtual ICollection<Contacto> ContactoUsuarioAltaIntNavigations { get; set; }
        public virtual ICollection<Contacto> ContactoUsuarioModificaIntNavigations { get; set; }
        public virtual ICollection<Seguimiento> SeguimientoUsuarioAltaIntNavigations { get; set; }
        public virtual ICollection<Seguimiento> SeguimientoUsuarioModificaIntNavigations { get; set; }
        public virtual ICollection<TiposDeSeguimiento> TiposDeSeguimientoUsuarioAltaIntNavigations { get; set; }
        public virtual ICollection<TiposDeSeguimiento> TiposDeSeguimientoUsuarioModificaIntNavigations { get; set; }
    }
}
