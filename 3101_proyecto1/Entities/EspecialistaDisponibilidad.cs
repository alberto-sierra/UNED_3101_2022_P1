using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace _3101_proyecto1.Entities
{
    [Table("EspecialistaDisponibilidad")]
    public partial class EspecialistaDisponibilidad
    {
        [Key]
        public int Id { get; set; }
        public byte DiaSemana { get; set; }
        public byte HoraInicio { get; set; }
        public byte HoraFinal { get; set; }
        public int IdEspecialista { get; set; }

        [ForeignKey("IdEspecialista")]
        [InverseProperty("EspecialistaDisponibilidads")]
        public virtual Especialistum IdEspecialistaNavigation { get; set; } = null!;
    }
}
