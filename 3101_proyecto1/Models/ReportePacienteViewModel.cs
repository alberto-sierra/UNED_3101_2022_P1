﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace _3101_proyecto1.Models
{
	public class ReportePacienteViewModel
	{
        [Display(Name = "Nombre del Paciente")]
        public string NombrePaciente { get; set; }

        [Display(Name = "Identificación")]
        public string Identificacion { get; set; }
    }
}

