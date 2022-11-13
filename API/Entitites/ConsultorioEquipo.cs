using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace _3101_proyecto1.Entities
{
    [Table("ConsultorioEquipo")]
    public partial class ConsultorioEquipo
    {
        public ConsultorioEquipo()
        {
            ReservaConsultorios = new HashSet<ReservaConsultorio>();
        }

        [Key]
        public int Id { get; set; }
        public int IdConsultorio { get; set; }
        public int IdEquipo { get; set; }

        [ForeignKey("IdConsultorio")]
        [InverseProperty("ConsultorioEquipos")]
        public virtual Consultorio IdConsultorioNavigation { get; set; } = null!;
        [ForeignKey("IdEquipo")]
        [InverseProperty("ConsultorioEquipos")]
        public virtual Equipo IdEquipoNavigation { get; set; } = null!;
        [InverseProperty("IdConsultorioNavigation")]
        public virtual ICollection<ReservaConsultorio> ReservaConsultorios { get; set; }
    }
}
