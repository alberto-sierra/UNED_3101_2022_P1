using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _3101_proyecto1.Entities;
using _3101_proyecto1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _3101_proyecto1.Controllers
{
    public class ReporteController : Controller
    {
        private readonly citasContext _context;

        public ReporteController(citasContext context)
        {
            _context = context;
        }

        // GET: /Reporte/
        public IActionResult Index()
        {
            var pctEspecialista = _context.ReservaConsultorios
                .Join(_context.Cita, r => r.Id, c => c.Id,
                    (r, c) => new { Reserva = r, Cita = c })
                .Join(_context.Especialista, r => r.Reserva.IdEspecialista, e => e.Id,
                    (r, e) => new { Reserva = r, Especialista = e })
                .GroupBy(x => x.Especialista.Nombre)
                .Select(x => new EstadisticaModel { Nombre = x.Key, Total = x.Count() })
                .ToList();

            decimal totalCitas = _context.Cita
                .Count();

            foreach (var item in pctEspecialista)
            {
                item.Porcentaje = item.Total / totalCitas;
            }

            var pctEspecialidad = _context.ReservaConsultorios
                .Join(_context.Cita, r => r.Id, c => c.Id,
                    (r, c) => new { Reserva = r, Cita = c })
                .Join(_context.Especialista, r => r.Reserva.IdEspecialista, e => e.Id,
                    (r, e) => new { Reserva = r, Especialista = e })
                .Join(_context.Especialidades, e => e.Especialista.IdEspecialidad, s => s.Id,
                    (e, s) => new { Especialista = e, Especialidad = s })
                .GroupBy(s => s.Especialidad.Nombre)
                .Select(x => new EstadisticaModel { Nombre = x.Key, Total = x.Count() })
                .ToList();

            foreach (var item in pctEspecialidad)
            {
                item.Porcentaje = item.Total / totalCitas;
            }

            var reporteEstadisticasViewModel = new ReporteEstadisticaViewModel
            {
                EstadisticaEspecialidad = pctEspecialidad,
                EstadisticaEspecialista = pctEspecialista
            };

            return View(reporteEstadisticasViewModel);
        }

        // GET: Reporte/Pacientes
        public async Task<IActionResult> Pacientes()
        {
            var filtroFecha = DateTime.Today + new TimeSpan(0, 0, 0);
            return View(await _context.Cita
                .Include(x => x.IdPacienteNavigation)
                .Where(x => x.Fecha < filtroFecha )
                .Select(x => new ReportePacienteViewModel
                {
                    Identificacion = x.IdPacienteNavigation.Identificacion,
                    NombrePaciente = x.IdPacienteNavigation.NombreCompleto
                })
                .Distinct()
            .ToListAsync());
        }

        // GET: Cita/ReporteEspecialistas
        public async Task<IActionResult> Especialistas()
        {
            var filtroFecha = DateTime.Today + new TimeSpan(0, 0, 0);
            return View(await _context.Cita
                .Join(_context.ReservaConsultorios, c => c.IdReserva, r => r.Id,
                (c, r) => new { Cita = c, Reserva = r })
                .Join(_context.Especialista, r => r.Reserva.IdEspecialista, e => e.Id,
                (r, e) => new { Reserva = r, Especialista = e })
                .Where(x => x.Reserva.Cita.Fecha < filtroFecha)
                .Select(x => new EspecialistumViewModel
                {
                    Identificacion = x.Especialista.Identificacion,
                    Nombre = x.Especialista.Nombre
                })
                .Distinct()
            .ToListAsync());
        }

        // GET: Reporte/Citas
        public async Task<IActionResult> Citas()
        {
            var filtroFecha = DateTime.Today + new TimeSpan(0, 0, 0);
            return View(await _context.Cita
                .Include(x => x.IdPacienteNavigation)
                .Include(x => x.IdReservaNavigation)
                .Where(x => x.Fecha < filtroFecha)
                .Select(x => new ReporteCitaViewModel
                {
                    NombrePaciente = x.IdPacienteNavigation.NombreCompleto,
                    NombreEspecialista = x.IdReservaNavigation.IdEspecialistaNavigation.Nombre,
                    HoraCita = x.Fecha,
                    Costo = x.PrecioConsulta
                })
            .ToListAsync());
        }

        // GET: Reporte/Consultorios
        public async Task<IActionResult> Consultorios()
        {
            return View(await _context.ReservaConsultorios
                .Include(x => x.IdConsultorioNavigation)
                .Join(_context.Especialista, r => r.IdEspecialista, e => e.Id,
                (r, e) => new { Reserva = r, Especialista = e })
                .Join(_context.Especialidades, e => e.Especialista.IdEspecialidad, s => s.Id,
                (e, s) => new { Especialista = e, Especialidad = s })
                .Select(x => new ReporteConsultorioViewModel
                {
                    NumeroConsultorio = x.Especialista.Reserva.IdConsultorioNavigation.Numero.ToString(),
                    NombreEspecialidad = x.Especialidad.Nombre
                })
            .ToListAsync());
        }

        // GET: Reporte/Ingresos
        public IActionResult Ingresos()
        {
            var filtroFecha = DateTime.Today + new TimeSpan(0, 0, 0);
            var ingresoTotal = _context.Cita
                .Where(x => x.Fecha < filtroFecha)
                .Sum(x => x.PrecioConsulta);

            var citasCantidad = _context.Cita
                .Where(x => x.Fecha < filtroFecha)
                .Count();

            var resultados = new ReporteIngresoViewModel
            {
                IngresoTotal = ingresoTotal,
                CitasCantidad = citasCantidad
            };

            return View(resultados);
        }

    }
}

