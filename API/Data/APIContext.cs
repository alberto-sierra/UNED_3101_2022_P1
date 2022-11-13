using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using _3101_proyecto1.Entities;

namespace _3101_proyecto1.Data
{
    public partial class APIContext : DbContext
    {

        public APIContext(DbContextOptions<APIContext> options)
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

    }
}
