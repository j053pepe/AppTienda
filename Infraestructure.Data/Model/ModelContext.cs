using System;
using Core.Models.AppTiendaModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Infraestructure.Data.Model
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<ProductoDetalle> ProductoDetalle { get; set; }
        public virtual DbSet<Tienda> Tienda { get; set; }
        public virtual DbSet<TiendaDetalle> TiendaDetalle { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<UsuarioDireccion> UsuarioDireccion { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<VentaDetalle> VentaDetalle { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseJet("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=G:/MSAccess/DbTiendaWebNP.accdb;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasIndex(e => e.ProductoId)
                    .HasName("ProductoId");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("UsuarioId");

                entity.Property(e => e.ProductoId).HasColumnType("counter");

                entity.Property(e => e.Activo)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("No");

                entity.Property(e => e.Codigo)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Precio).HasColumnType("currency");

                entity.Property(e => e.Stock).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UsuarioId).IsRequired();

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Producto)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioProducto");
            });

            modelBuilder.Entity<ProductoDetalle>(entity =>
            {
                entity.HasKey(e => e.ProductoId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.ProductoId)
                    .HasName("ProductoId");

                entity.Property(e => e.ProductoId).ValueGeneratedNever();

                entity.Property(e => e.Descripcion).IsRequired();

                entity.Property(e => e.UrlImage).IsRequired();

                entity.HasOne(d => d.Producto)
                    .WithOne(p => p.ProductoDetalle)
                    .HasForeignKey<ProductoDetalle>(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductoProductoDetalle");
            });

            modelBuilder.Entity<Tienda>(entity =>
            {
                entity.HasIndex(e => e.TiendaId)
                    .HasName("TiendaId");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("UsuarioId");

                entity.Property(e => e.TiendaId).HasColumnType("counter");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UsuarioId).IsRequired();

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Tienda)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioTienda");
            });

            modelBuilder.Entity<TiendaDetalle>(entity =>
            {
                entity.HasKey(e => e.TiendaId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.TiendaId)
                    .HasName("TiendaDetalleId");

                entity.Property(e => e.TiendaId).ValueGeneratedNever();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UrlImage).IsRequired();

                entity.HasOne(d => d.Tienda)
                    .WithOne(p => p.TiendaDetalle)
                    .HasForeignKey<TiendaDetalle>(d => d.TiendaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TiendaTiendaDetalle");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(e => e.UsuarioId)
                    .HasName("UsuarioId");

                entity.Property(e => e.Activo)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("No");

                entity.Property(e => e.ApellidoMaterno)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ApellidoPaterno)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password).IsRequired();

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<UsuarioDireccion>(entity =>
            {
                entity.HasKey(e => e.UsuarioId)
                    .HasName("PrimaryKey");

                entity.HasIndex(e => e.UsuarioId)
                    .HasName("UsuarioId");

                entity.Property(e => e.Calle)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Colonia)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Cp)
                    .IsRequired()
                    .HasColumnName("CP")
                    .HasMaxLength(255);

                entity.Property(e => e.Numero)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Usuario)
                    .WithOne(p => p.UsuarioDireccion)
                    .HasForeignKey<UsuarioDireccion>(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioUsuarioDireccion");
            });

            modelBuilder.Entity<Venta>(entity =>
            {
                entity.HasIndex(e => e.UsuarioId)
                    .HasName("UsuarioId");

                entity.HasIndex(e => e.VentaId)
                    .HasName("VentaId");

                entity.Property(e => e.VentaId).HasColumnType("counter");

                entity.Property(e => e.Activo)
                    .HasColumnType("bit")
                    .HasDefaultValueSql("No");

                entity.Property(e => e.Total).HasColumnType("currency");

                entity.Property(e => e.UsuarioId).IsRequired();

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Venta)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UsuarioVenta");
            });

            modelBuilder.Entity<VentaDetalle>(entity =>
            {
                entity.HasIndex(e => e.ProductoId)
                    .HasName("ProductoId");

                entity.HasIndex(e => e.VentaDetalleId)
                    .HasName("VentaDetalleId");

                entity.HasIndex(e => e.VentaId)
                    .HasName("VentaId");

                entity.Property(e => e.VentaDetalleId).HasColumnType("counter");

                entity.Property(e => e.Cantidad).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Precio).HasColumnType("currency");

                entity.Property(e => e.Total).HasColumnType("currency");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.VentaDetalle)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ProductoVentaDetalle");

                entity.HasOne(d => d.Venta)
                    .WithMany(p => p.VentaDetalle)
                    .HasForeignKey(d => d.VentaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("VentaVentaDetalle");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
