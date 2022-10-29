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
    public class CitaController : Controller
    {
        private readonly citasContext _context;

        public CitaController(citasContext context)
        {
            _context = context;
        }

        // GET: Cita
        public async Task<IActionResult> Index()
        {
              return _context.Citas != null ? 
                          View(await _context.Citas.ToListAsync()) :
                          Problem("Entity set 'citasContext.CitumViewModel'  is null.");
        }

        // GET: Cita/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var citumViewModel = await _context.Citas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (citumViewModel == null)
            {
                return NotFound();
            }

            return View(citumViewModel);
        }

        // GET: Cita/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cita/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdPaciente,IdReserva,Fecha,HoraInicio")] CitumViewModel citumViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citumViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(citumViewModel);
        }

        // GET: Cita/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var citumViewModel = await _context.Citas.FindAsync(id);
            if (citumViewModel == null)
            {
                return NotFound();
            }
            return View(citumViewModel);
        }

        // POST: Cita/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdPaciente,IdReserva,Fecha,HoraInicio")] CitumViewModel citumViewModel)
        {
            if (id != citumViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citumViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitumViewModelExists(citumViewModel.Id))
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
            return View(citumViewModel);
        }

        // GET: Cita/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Citas == null)
            {
                return NotFound();
            }

            var citumViewModel = await _context.Citas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (citumViewModel == null)
            {
                return NotFound();
            }

            return View(citumViewModel);
        }

        // POST: Cita/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Citas == null)
            {
                return Problem("Entity set 'citasContext.CitumViewModel'  is null.");
            }
            var citumViewModel = await _context.Citas.FindAsync(id);
            if (citumViewModel != null)
            {
                _context.Citas.Remove(citumViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitumViewModelExists(int id)
        {
          return (_context.Citas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
