using Microsoft.AspNetCore.Mvc;
using _3101_proyecto1.Api.Data;
using _3101_proyecto1.Api.Entities;
using _3101_proyecto1.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("Cita")]
public class CitaController : ControllerBase
{

    private readonly ApiContext _context;

    public CitaController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet("{identificacion}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<Cita>> GetCitas(string identificacion)
    {
        var citas = _context.Cita
        .Include(x => x.IdPacienteNavigation)
        .Where(x => x.IdPacienteNavigation.Identificacion == identificacion)
        .Select(x => new CitumViewModel
        {
            Id = x.Id,
            IdPaciente = x.IdPaciente,
            IdReserva = x.IdReserva,
            Fecha = x.Fecha,
            HoraInicio = x.HoraInicio
        })
        .ToListAsync();

        if (citas == null)
        {
            return NotFound();
        }

        return Ok(citas);
    }
}

[Route("Paciente")]
public class PacienteController : ControllerBase
{
    private readonly ApiContext _context;

    public PacienteController(ApiContext context)
    {
        _context = context;
    }

    [HttpGet("Throw")]
    public IActionResult Throw() =>
    throw new Exception("Internal error.");

    [HttpGet("{identificacion}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PacienteViewModel))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetByIdNumber(string identificacion)
    {
        var paciente = _context.Pacientes
            .FirstOrDefault(m => m.Identificacion == identificacion);

        if (paciente == null)
        {
            return NotFound();
        }

        var pacienteViewModel = new PacienteViewModel
        {
            Id = paciente.Id,
            Identificacion = paciente.Identificacion,
            NombreCompleto = paciente.NombreCompleto,
            Telefono = paciente.Telefono
        };

        return Ok(pacienteViewModel);
    }
}
