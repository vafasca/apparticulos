﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ArticulosWeb.Models
{
    public partial class InventarioDBWContext : DbContext
    {
        public InventarioDBWContext()
        {
        }

        public InventarioDBWContext(DbContextOptions<InventarioDBWContext> options)
            : base(options)
        {
        }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Anuncio> Anuncio { get; set; }
        public virtual DbSet<AppInventario> AppInventario { get; set; }
        public virtual DbSet<Auto> Auto { get; set; }
        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Galeria> Galeria { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Repuesto> Repuesto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Host=localhost;Database=InventarioDBW;Username=postgres;Password=postgres");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique();

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.LockoutEnd).HasColumnType("timestamp with time zone");

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Anuncio>(entity =>
            {
                entity.Property(e => e.AnuncioId)
                    .HasColumnName("AnuncioID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.Gestion).HasColumnType("date");

                entity.Property(e => e.Titulo).HasMaxLength(255);
            });

            modelBuilder.Entity<AppInventario>(entity =>
            {
                entity.HasKey(e => e.InventarioId)
                    .HasName("AppInventario_pk");

                entity.Property(e => e.InventarioId)
                    .HasColumnName("InventarioID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AnuncioIdAnuncio).HasColumnName("AnuncioID_Anuncio");

                entity.Property(e => e.Gestion).HasColumnType("date");

                entity.HasOne(d => d.AnuncioIdAnuncioNavigation)
                    .WithMany(p => p.AppInventario)
                    .HasForeignKey(d => d.AnuncioIdAnuncio)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Anuncio_fk");
            });

            modelBuilder.Entity<Auto>(entity =>
            {
                entity.Property(e => e.AutoId)
                    .HasColumnName("AutoID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Marca).HasMaxLength(255);

                entity.Property(e => e.Modelo).HasMaxLength(255);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.Property(e => e.CategoriaId)
                    .HasColumnName("CategoriaID")
                    .ValueGeneratedNever();

                entity.Property(e => e.NombreCategoria).HasMaxLength(255);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.ClienteId)
                    .HasColumnName("ClienteID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Apellido).HasMaxLength(255);

                entity.Property(e => e.Ci).HasColumnName("CI");

                entity.Property(e => e.FechaRegistro).HasColumnType("date");

                entity.Property(e => e.Nombre).HasMaxLength(255);
            });

            modelBuilder.Entity<Galeria>(entity =>
            {
                entity.Property(e => e.GaleriaId)
                    .HasColumnName("GaleriaID")
                    .ValueGeneratedNever();

                entity.Property(e => e.DescripcionFoto).HasMaxLength(255);

                entity.Property(e => e.NombreFoto).HasMaxLength(255);

                entity.Property(e => e.RepuestoIdRepuesto).HasColumnName("RepuestoID_Repuesto");

                entity.HasOne(d => d.RepuestoIdRepuestoNavigation)
                    .WithMany(p => p.Galeria)
                    .HasForeignKey(d => d.RepuestoIdRepuesto)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Repuesto_fk");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.Property(e => e.PedidoId)
                    .HasColumnName("PedidoID")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClienteIdCliente).HasColumnName("ClienteID_Cliente");

                entity.Property(e => e.FechaReserva).HasColumnType("date");

                entity.Property(e => e.InventarioIdAppInventario).HasColumnName("InventarioID_AppInventario");

                entity.Property(e => e.NombrePedido).HasMaxLength(255);

                entity.Property(e => e.RepuestoIdRepuesto).HasColumnName("RepuestoID_Repuesto");

                entity.HasOne(d => d.ClienteIdClienteNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.ClienteIdCliente)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Cliente_fk");

                entity.HasOne(d => d.InventarioIdAppInventarioNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.InventarioIdAppInventario)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("AppInventario_fk");

                entity.HasOne(d => d.RepuestoIdRepuestoNavigation)
                    .WithMany(p => p.Pedido)
                    .HasForeignKey(d => d.RepuestoIdRepuesto)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Repuesto_fk");
            });

            modelBuilder.Entity<Repuesto>(entity =>
            {
                entity.Property(e => e.RepuestoId)
                    .HasColumnName("RepuestoID")
                    .ValueGeneratedNever();

                entity.Property(e => e.AutoIdAuto).HasColumnName("AutoID_Auto");

                entity.Property(e => e.CategoriaIdCategoria).HasColumnName("CategoriaID_Categoria");

                entity.Property(e => e.Descripcion).HasMaxLength(255);

                entity.Property(e => e.InventarioIdAppInventario).HasColumnName("InventarioID_AppInventario");

                entity.Property(e => e.Nombre).HasMaxLength(255);

                entity.HasOne(d => d.AutoIdAutoNavigation)
                    .WithMany(p => p.Repuesto)
                    .HasForeignKey(d => d.AutoIdAuto)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Auto_fk");

                entity.HasOne(d => d.CategoriaIdCategoriaNavigation)
                    .WithMany(p => p.Repuesto)
                    .HasForeignKey(d => d.CategoriaIdCategoria)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Categoria_fk");

                entity.HasOne(d => d.InventarioIdAppInventarioNavigation)
                    .WithMany(p => p.Repuesto)
                    .HasForeignKey(d => d.InventarioIdAppInventario)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("AppInventario_fk");
            });
        }
    }
}
