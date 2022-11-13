using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace _3101_proyecto1.Entities
{
    [Table("ReservaConsultorio")]
    public partial class ReservaConsultorio
    {
        public ReservaConsultorio()
        {
            Cita = new HashSet<Cita>();
        }

        [Key]
        public int Id { get; set; }
        public int IdEspecialista { get; set; }
        public int IdConsultorio { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFinal { get; set; }
        public byte DiaSemana { get; set; }
        public bool Disponible { get; set; }

        [ForeignKey("IdConsultorio")]
        [InverseProperty("ReservaConsultorios")]
        public virtual ConsultorioEquipo IdConsultorioNavigation { get; set; } = null!;
        [ForeignKey("IdEspecialista")]
        [InverseProperty("ReservaConsultorios")]
        public virtual Especialista IdEspecialistaNavigation { get; set; } = null!;
        [InverseProperty("IdReservaNavigation")]
        public virtual ICollection<Cita> Cita { get; set; }
    }
}
