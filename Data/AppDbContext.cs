using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Models;

namespace ProyectoFinalDraft.Data
    {
    public class AppDbContext : DbContext
        {
        public AppDbContext(DbContextOptions<AppDbContext> options)
  : base(options)
            {
            }

        public DbSet<ProyectoFinalDraft.Models.Usuario> Usuario { get; set; } = default!;
        public DbSet<ProyectoFinalDraft.Models.Rol> Rol { get; set; } = default!;
        public DbSet<ProyectoFinalDraft.Models.Cita> Cita { get; set; } = default!;
        public DbSet<ProyectoFinalDraft.Models.EstadoCita> EstadoCita { get; set; } = default!;
        public DbSet<ProyectoFinalDraft.Models.Factura> Factura { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);

            // Aplica precisión decimal (10,2) a todas las propiedades tipo decimal o decimal?
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                foreach (var property in entityType.GetProperties())
                    {
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                        {
                        property.SetPrecision(10);
                        property.SetScale(2);
                        }
                    }
                }

            // Relación Cita <-> Factura (1:1)
            modelBuilder.Entity<Cita>()
                .HasOne(c => c.Factura)
                .WithOne(f => f.Cita)
                .HasForeignKey<Factura>(f => f.CitaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relación Factura <-> Usuario (N:1) sin cascada
            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Usuario)
                .WithMany()
                .HasForeignKey(f => f.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            //Tabla Relacional CitaServicio con ID Compuesto

            modelBuilder.Entity<CitaServicio>()
                .HasKey(cs => new { cs.CitaId, cs.ServicioId });

            modelBuilder.Entity<CitaServicio>()
                .HasOne(cs => cs.Cita)
                .WithMany(c => c.CitaServicios)
                .HasForeignKey(cs => cs.CitaId);

            modelBuilder.Entity<CitaServicio>()
                .HasOne(cs => cs.Servicio)
                .WithMany(s => s.CitaServicios)
                .HasForeignKey(cs => cs.ServicioId);

            modelBuilder.Entity<Factura>()
                .HasOne(f => f.Usuario)
                .WithMany(u => u.Facturas)
                .HasForeignKey (u => u.CitaId);

            }
        public DbSet<ProyectoFinalDraft.Models.Servicio> Servicio { get; set; } = default!;


        }
    }