using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _3101_proyecto1.Entities;
using _3101_proyecto1.Models;

namespace Backend.Controllers
{
    public class PacienteController : Controller
    {
        private readonly citasContext _context;

        public PacienteController(citasContext context)
        {
            _context = context;
        }

        // GET: Paciente
        public async Task<IActionResult> Index()
        {
              return _context.Pacientes != null ? 
                          View(await _context.Pacientes.ToListAsync()) :
                          Problem("Entity set 'citasContext.PacienteViewModel'  is null.");
        }

        // GET: Paciente/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var pacienteViewModel = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

        // GET: Paciente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Paciente/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Identificacion,NombreCompleto")] PacienteViewModel pacienteViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacienteViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacienteViewModel);
        }

        // GET: Paciente/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var pacienteViewModel = await _context.Pacientes.FindAsync(id);
            if (pacienteViewModel == null)
            {
                return NotFound();
            }
            return View(pacienteViewModel);
        }

        // POST: Paciente/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Identificacion,NombreCompleto")] PacienteViewModel pacienteViewModel)
        {
            if (id != pacienteViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacienteViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteViewModelExists(pacienteViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pacienteViewModel);
        }

        // GET: Paciente/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Pacientes == null)
            {
                return NotFound();
            }

            var pacienteViewModel = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

        // POST: Paciente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Pacientes == null)
            {
                return Problem("Entity set 'citasContext.PacienteViewModel'  is null.");
            }
            var pacienteViewModel = await _context.Pacientes.FindAsync(id);
            if (pacienteViewModel != null)
            {
                _context.Pacientes.Remove(pacienteViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteViewModelExists(long id)
        {
          return (_context.Pacientes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
