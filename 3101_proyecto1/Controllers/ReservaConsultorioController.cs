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
    public class ReservaConsultorioController : Controller
    {
        private readonly citasContext _context;

        public ReservaConsultorioController(citasContext context)
        {
            _context = context;
        }

        // GET: ReservaConsultorio
        public async Task<IActionResult> Index()
        {
              return _context.ReservaConsultorios != null ? 
                          View(await _context.ReservaConsultorios.ToListAsync()) :
                          Problem("Entity set 'citasContext.ReservaConsultorioViewModel'  is null.");
        }

        // GET: ReservaConsultorio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ReservaConsultorios == null)
            {
                return NotFound();
            }

            var reservaConsultorioViewModel = await _context.ReservaConsultorios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservaConsultorioViewModel == null)
            {
                return NotFound();
            }

            return View(reservaConsultorioViewModel);
        }

        // GET: ReservaConsultorio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReservaConsultorio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdEspecialista,IdConsultorio,HoraInicio,DiaSemana,Disponible")] ReservaConsultorioViewModel reservaConsultorioViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservaConsultorioViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservaConsultorioViewModel);
        }

        // GET: ReservaConsultorio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ReservaConsultorios == null)
            {
                return NotFound();
            }

            var reservaConsultorioViewModel = await _context.ReservaConsultorios.FindAsync(id);
            if (reservaConsultorioViewModel == null)
            {
                return NotFound();
            }
            return View(reservaConsultorioViewModel);
        }

        // POST: ReservaConsultorio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdEspecialista,IdConsultorio,HoraInicio,DiaSemana,Disponible")] ReservaConsultorioViewModel reservaConsultorioViewModel)
        {
            if (id != reservaConsultorioViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservaConsultorioViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservaConsultorioViewModelExists(reservaConsultorioViewModel.Id))
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
            return View(reservaConsultorioViewModel);
        }

        // GET: ReservaConsultorio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ReservaConsultorios == null)
            {
                return NotFound();
            }

            var reservaConsultorioViewModel = await _context.ReservaConsultorios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservaConsultorioViewModel == null)
            {
                return NotFound();
            }

            return View(reservaConsultorioViewModel);
        }

        // POST: ReservaConsultorio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ReservaConsultorios == null)
            {
                return Problem("Entity set 'citasContext.ReservaConsultorioViewModel'  is null.");
            }
            var reservaConsultorioViewModel = await _context.ReservaConsultorios.FindAsync(id);
            if (reservaConsultorioViewModel != null)
            {
                _context.ReservaConsultorios.Remove(reservaConsultorioViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservaConsultorioViewModelExists(int id)
        {
          return (_context.ReservaConsultorios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
