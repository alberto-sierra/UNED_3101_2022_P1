using Microsoft.AspNetCore.Mvc;
using _3101_proyecto1.Data;
using _3101_proyecto1.Entities;

namespace API.Controllers;

[ApiController]
[Route("Cita")]
public class CitaController : ControllerBase
{

    private readonly ILogger<CitaController> _logger;
    private readonly APIContext _context;

    public CitaController(ILogger<CitaController> logger, APIContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet(Name = "GetCita")]
    public IEnumerable<Cita> Get()
    {
        return _context.Cita.Select(x => new Cita
        {
            Id = x.Id,
            IdPaciente = x.IdPaciente,
            IdReserva = x.IdReserva,
            Fecha = x.Fecha,
            HoraInicio = x.HoraInicio
        })
        .ToArray();
    }
}
