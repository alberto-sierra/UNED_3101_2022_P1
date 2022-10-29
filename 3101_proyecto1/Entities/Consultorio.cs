using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace _3101_proyecto1.Entities
{
    [Table("Consultorio")]
    public partial class Consultorio
    {
        public Consultorio()
        {
            ConsultorioEquipos = new HashSet<ConsultorioEquipo>();
        }

        [Key]
        public int Id { get; set; }
        public int Numero { get; set; }

        [InverseProperty("IdConsultorioNavigation")]
        public virtual ICollection<ConsultorioEquipo> ConsultorioEquipos { get; set; }
    }
}
