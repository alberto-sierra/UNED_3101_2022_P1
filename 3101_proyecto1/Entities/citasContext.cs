﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace _3101_proyecto1.Entities
{
    public partial class citasContext : DbContext
    {
        public citasContext()
        {
        }

        public citasContext(DbContextOptions<citasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cita> Cita { get; set; } = null!;
        public virtual DbSet<Consultorio> Consultorios { get; set; } = null!;
        public virtual DbSet<ConsultorioEquipo> ConsultorioEquipos { get; set; } = null!;
        public virtual DbSet<Equipo> Equipos { get; set; } = null!;
        public virtual DbSet<Especialidad> Especialidades { get; set; } = null!;
        public virtual DbSet<EspecialistaDisponibilidad> EspecialistaDisponibilidads { get; set; } = null!;
        public virtual DbSet<Especialista> Especialista { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;
        public virtual DbSet<ReservaConsultorio> ReservaConsultorios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cita>(entity =>
            {
                entity.HasOne(d => d.IdPacienteNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdPaciente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cita__IdPaciente__619B8048");

                entity.HasOne(d => d.IdReservaNavigation)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.IdReserva)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cita__IdReserva__628FA481");
            });

            modelBuilder.Entity<ConsultorioEquipo>(entity =>
            {
                entity.HasOne(d => d.IdConsultorioNavigation)
                    .WithMany(p => p.ConsultorioEquipos)
                    .HasForeignKey(d => d.IdConsultorio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Consultor__IdCon__59FA5E80");

                entity.HasOne(d => d.IdEquipoNavigation)
                    .WithMany(p => p.ConsultorioEquipos)
                    .HasForeignKey(d => d.IdEquipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Consultor__IdEqu__5AEE82B9");
            });

            modelBuilder.Entity<Equipo>(entity =>
            {
                entity.HasOne(d => d.IdEspecialidadNavigation)
                    .WithMany(p => p.Equipos)
                    .HasForeignKey(d => d.IdEspecialidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Equipo__IdEspeci__571DF1D5");
            });

            modelBuilder.Entity<EspecialistaDisponibilidad>(entity =>
            {
                entity.HasOne(d => d.IdEspecialistaNavigation)
                    .WithMany(p => p.EspecialistaDisponibilidads)
                    .HasForeignKey(d => d.IdEspecialista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Especiali__IdEsp__5441852A");
            });

            modelBuilder.Entity<Especialista>(entity =>
            {
                entity.HasOne(d => d.IdEspecialidadNavigation)
                    .WithMany(p => p.Especialista)
                    .HasForeignKey(d => d.IdEspecialidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Especiali__IdEsp__5165187F");
            });

            modelBuilder.Entity<ReservaConsultorio>(entity =>
            {
                entity.HasOne(d => d.IdConsultorioNavigation)
                    .WithMany(p => p.ReservaConsultorios)
                    .HasForeignKey(d => d.IdConsultorio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReservaCo__IdCon__5EBF139D");

                entity.HasOne(d => d.IdEspecialistaNavigation)
                    .WithMany(p => p.ReservaConsultorios)
                    .HasForeignKey(d => d.IdEspecialista)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ReservaCo__IdEsp__5DCAEF64");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
