using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ApiNovoLeads
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Contacto> Contactos { get; set; }
        public virtual DbSet<Seguimiento> Seguimientos { get; set; }
        public virtual DbSet<TiposDeSeguimiento> TiposDeSeguimientos { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contacto>(entity =>
            {
                entity.HasKey(e => e.ContactoIdInt);

                entity.Property(e => e.ContactoIdInt).HasColumnName("contactoID_int");

                entity.Property(e => e.EsIntegradorBit).HasColumnName("esIntegrador_bit");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_Date");

                entity.Property(e => e.FechaModificacionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaModificacion_Date");

                entity.Property(e => e.IsActiveBit).HasColumnName("isActive_bit");

                entity.Property(e => e.NombreVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombre_var");

                entity.Property(e => e.PctjedecierreInt).HasColumnName("pctjedecierre_int");

                entity.Property(e => e.SolucionVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("solucion_var");

                entity.Property(e => e.TelefonoVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("telefono_var");

                entity.Property(e => e.UsuarioAltaInt).HasColumnName("usuarioAlta_int");

                entity.Property(e => e.UsuarioModificaInt).HasColumnName("usuarioModifica_int");

                entity.Property(e => e.WhatsappVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("whatsapp_var");

                entity.HasOne(d => d.UsuarioAltaIntNavigation)
                    .WithMany(p => p.ContactoUsuarioAltaIntNavigations)
                    .HasForeignKey(d => d.UsuarioAltaInt)
                    .HasConstraintName("FK_Contactos_UsuarioAlta");

                entity.HasOne(d => d.UsuarioModificaIntNavigation)
                    .WithMany(p => p.ContactoUsuarioModificaIntNavigations)
                    .HasForeignKey(d => d.UsuarioModificaInt)
                    .HasConstraintName("FK_Contactos_UsuarioMod");
            });

            modelBuilder.Entity<Seguimiento>(entity =>
            {
                entity.HasKey(e => e.SeguimientoIdInt);

                entity.ToTable("Seguimiento");

                entity.Property(e => e.SeguimientoIdInt).HasColumnName("seguimientoID_int");

                entity.Property(e => e.ComentariosText)
                    .HasColumnType("text")
                    .HasColumnName("comentarios_text");

                entity.Property(e => e.ContactoIdInt).HasColumnName("contactoID_int");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_Date");

                entity.Property(e => e.FechaModificacionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaModificacion_Date");

                entity.Property(e => e.IsActiveBit).HasColumnName("IsActive_bit");

                entity.Property(e => e.PctjedecierreInt).HasColumnName("pctjedecierre_int");

                entity.Property(e => e.SolucionVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("solucion_var");

                entity.Property(e => e.TelefonoVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("telefono_var");

                entity.Property(e => e.TipoSeguimientoIdInt).HasColumnName("tipoSeguimientoID_int");

                entity.Property(e => e.UsuarioAltaInt).HasColumnName("usuarioAlta_int");

                entity.Property(e => e.UsuarioModificaInt).HasColumnName("usuarioModifica_int");

                entity.Property(e => e.WhatsappVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("whatsapp_var");

                entity.HasOne(d => d.ContactoIdIntNavigation)
                    .WithMany(p => p.Seguimientos)
                    .HasForeignKey(d => d.ContactoIdInt)
                    .HasConstraintName("FK_Seguimiento_Contactos");

                entity.HasOne(d => d.TipoSeguimientoIdIntNavigation)
                    .WithMany(p => p.Seguimientos)
                    .HasForeignKey(d => d.TipoSeguimientoIdInt)
                    .HasConstraintName("FK_Seguimiento_TipoSeguimiento");

                entity.HasOne(d => d.UsuarioAltaIntNavigation)
                    .WithMany(p => p.SeguimientoUsuarioAltaIntNavigations)
                    .HasForeignKey(d => d.UsuarioAltaInt)
                    .HasConstraintName("FK_Seguimiento_UsuarioAlta");

                entity.HasOne(d => d.UsuarioModificaIntNavigation)
                    .WithMany(p => p.SeguimientoUsuarioModificaIntNavigations)
                    .HasForeignKey(d => d.UsuarioModificaInt)
                    .HasConstraintName("FK_Seguimiento_UsuarioMod");
            });

            modelBuilder.Entity<TiposDeSeguimiento>(entity =>
            {
                entity.HasKey(e => e.TipoSeguimientoIdInt);

                entity.ToTable("TiposDeSeguimiento");

                entity.Property(e => e.TipoSeguimientoIdInt).HasColumnName("tipoSeguimientoID_int");

                entity.Property(e => e.DescripcionVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion_var");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_Date");

                entity.Property(e => e.FechaModificacionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaModificacion_Date");

                entity.Property(e => e.IsActiveBit).HasColumnName("isActive_bit");

                entity.Property(e => e.UsuarioAltaInt).HasColumnName("usuarioAlta_int");

                entity.Property(e => e.UsuarioModificaInt).HasColumnName("usuarioModifica_int");

                entity.HasOne(d => d.UsuarioAltaIntNavigation)
                    .WithMany(p => p.TiposDeSeguimientoUsuarioAltaIntNavigations)
                    .HasForeignKey(d => d.UsuarioAltaInt)
                    .HasConstraintName("FK_TiposDeSeguimiento_UsuarioAlta");

                entity.HasOne(d => d.UsuarioModificaIntNavigation)
                    .WithMany(p => p.TiposDeSeguimientoUsuarioModificaIntNavigations)
                    .HasForeignKey(d => d.UsuarioModificaInt)
                    .HasConstraintName("FK_TiposDeSeguimiento_UsuarioMod");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.UsuarioIdInt);

                entity.Property(e => e.UsuarioIdInt).HasColumnName("usuarioID_int");

                entity.Property(e => e.FechaAltaDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaAlta_Date");

                entity.Property(e => e.FechaModificacionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaModificacion_Date");

                entity.Property(e => e.IsActiveBit).HasColumnName("isActive_bit");

                entity.Property(e => e.NombreVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("nombre_var");

                entity.Property(e => e.PasswordVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("password_var");

                entity.Property(e => e.UsuarioModificaVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuarioModifica_var");

                entity.Property(e => e.UsuarioVar)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("usuario_var");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
