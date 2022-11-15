using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace _3101_proyecto1.FrontEnd.Models
{
    public partial class CitumViewModel
    {
        public int Id { get; set; }

        [Required]
        public long IdPaciente { get; set; }

        [Required]
        public int IdReserva { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public string NombreEspecialista { get; set; }
        public int IdEspecialista { get; set; }
        public string NombrePaciente { get; set; }
        public int IdEspecialidad { get; set; }
        public string Especialidad { get; set; }
        public decimal PrecioConsulta { get; set; }
        public List<SelectListItem> ListaItems { get; set; }
    }
}
