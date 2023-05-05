using System;
using System.Collections.Generic;
using Domain.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Domain.DataAccess.Context;

public partial class MysqlPonchesContext : DbContext
{
    public MysqlPonchesContext()
    {
    }

    public MysqlPonchesContext(DbContextOptions<MysqlPonchesContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Credenciale> Credenciales { get; set; }

    public virtual DbSet<Negocio> Negocios { get; set; }

    public virtual DbSet<Registro> Registros { get; set; }

    public virtual DbSet<Reporte> Reportes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuariosRole> UsuariosRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("server=localhost;database=ponches_api_rest;user=root;password=Mosquea42510");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Credenciale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("credenciales");

            entity.HasIndex(e => e.Correo, "credenciales_correo_unique").IsUnique();

            entity.HasIndex(e => e.UsuarioId, "credenciales_usuario_id_foreign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .HasColumnName("correo");
            entity.Property(e => e.Pwd)
                .HasMaxLength(16)
                .IsFixedLength()
                .HasColumnName("pwd");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Credenciales)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("credenciales_usuario_id_foreign");
        });

        modelBuilder.Entity<Negocio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("negocios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Verificado)
                .HasDefaultValueSql("'0'")
                .HasColumnName("verificado");
        });

        modelBuilder.Entity<Registro>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("registros");

            entity.HasIndex(e => e.Cedula, "registros_cedula_indx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cedula)
                .HasMaxLength(16)
                .HasColumnName("cedula");
            entity.Property(e => e.FechaRegistro)
                .HasColumnType("datetime")
                .HasColumnName("fecha_registro");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reportes");

            entity.HasIndex(e => e.UsuarioId, "reportes_usuario_id_foreign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .HasColumnName("descripcion");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Usuario).WithMany(p => p.Reportes)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("reportes_usuario_id_foreign");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.Cedula, "usuarios_cedula_unique").IsUnique();

            entity.HasIndex(e => e.NegocioId, "usuarios_negocio_id_foreign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cedula)
                .HasMaxLength(16)
                .HasColumnName("cedula");
            entity.Property(e => e.NegocioId).HasColumnName("negocio_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(35)
                .HasColumnName("nombre");
            entity.Property(e => e.PrimerApellido)
                .HasMaxLength(40)
                .HasColumnName("primer_apellido");
            entity.Property(e => e.SegundoApellido)
                .HasMaxLength(40)
                .HasColumnName("segundo_apellido");

            entity.HasOne(d => d.Negocio).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.NegocioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_negocio_id_foreign");
        });

        modelBuilder.Entity<UsuariosRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios_roles");

            entity.HasIndex(e => e.RoleId, "usuarios_roles_role_id_foreign");

            entity.HasIndex(e => e.UsuarioId, "usuarios_roles_usuario_id_foreign");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_roles_role_id_foreign");

            entity.HasOne(d => d.Usuario).WithMany(p => p.UsuariosRoles)
                .HasForeignKey(d => d.UsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_roles_usuario_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
