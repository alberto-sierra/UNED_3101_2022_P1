using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace _3101_proyecto1.Models
{
    public partial class ReservaConsultorioViewModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int IdEspecialista { get; set; }

        [Required]
        public int IdConsultorio { get; set; }

        [Required]
        [Display(Name = "Hora de Inicio")]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [Display(Name = "Día de la Semana")]
        [RegularExpression(@"[0-7]{1}$")]
        public byte DiaSemana { get; set; }

        [Required]
        [Display(Name = "Disponible")]
        public bool Disponible { get; set; }
    }
}
