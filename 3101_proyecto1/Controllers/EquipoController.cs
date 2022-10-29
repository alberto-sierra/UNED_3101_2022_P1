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
    public class EquipoController : Controller
    {
        private readonly citasContext _context;

        public EquipoController(citasContext context)
        {
            _context = context;
        }

        // GET: Equipo
        public async Task<IActionResult> Index()
        {
              return _context.Equipos != null ? 
                          View(await _context.Equipos.ToListAsync()) :
                          Problem("Entity set 'citasContext.EquipoViewModel'  is null.");
        }

        // GET: Equipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Equipos == null)
            {
                return NotFound();
            }

            var equipoViewModel = await _context.Equipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipoViewModel == null)
            {
                return NotFound();
            }

            return View(equipoViewModel);
        }

        // GET: Equipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,IdEspecialidad")] EquipoViewModel equipoViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipoViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(equipoViewModel);
        }

        // GET: Equipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Equipos == null)
            {
                return NotFound();
            }

            var equipoViewModel = await _context.Equipos.FindAsync(id);
            if (equipoViewModel == null)
            {
                return NotFound();
            }
            return View(equipoViewModel);
        }

        // POST: Equipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,IdEspecialidad")] EquipoViewModel equipoViewModel)
        {
            if (id != equipoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipoViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipoViewModelExists(equipoViewModel.Id))
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
            return View(equipoViewModel);
        }

        // GET: Equipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Equipos == null)
            {
                return NotFound();
            }

            var equipoViewModel = await _context.Equipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (equipoViewModel == null)
            {
                return NotFound();
            }

            return View(equipoViewModel);
        }

        // POST: Equipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Equipos == null)
            {
                return Problem("Entity set 'citasContext.EquipoViewModel'  is null.");
            }
            var equipoViewModel = await _context.Equipos.FindAsync(id);
            if (equipoViewModel != null)
            {
                _context.Equipos.Remove(equipoViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipoViewModelExists(int id)
        {
          return (_context.Equipos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
