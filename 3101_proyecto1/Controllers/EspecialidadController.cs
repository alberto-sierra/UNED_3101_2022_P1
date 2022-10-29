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
    public class EspecialidadController : Controller
    {
        private readonly citasContext _context;

        public EspecialidadController(citasContext context)
        {
            _context = context;
        }

        // GET: Especialidad
        public async Task<IActionResult> Index()
        {
              return _context.Especialidades != null ? 
                          View(await _context.Especialidades.ToListAsync()) :
                          Problem("Entity set 'citasContext.EspecialidadViewModel'  is null.");
        }

        // GET: Especialidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Especialidades == null)
            {
                return NotFound();
            }

            var especialidadViewModel = await _context.Especialidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especialidadViewModel == null)
            {
                return NotFound();
            }

            return View(especialidadViewModel);
        }

        // GET: Especialidad/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Especialidad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] EspecialidadViewModel especialidadViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialidadViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialidadViewModel);
        }

        // GET: Especialidad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Especialidades == null)
            {
                return NotFound();
            }

            var especialidadViewModel = await _context.Especialidades.FindAsync(id);
            if (especialidadViewModel == null)
            {
                return NotFound();
            }
            return View(especialidadViewModel);
        }

        // POST: Especialidad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre")] EspecialidadViewModel especialidadViewModel)
        {
            if (id != especialidadViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialidadViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialidadViewModelExists(especialidadViewModel.Id))
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
            return View(especialidadViewModel);
        }

        // GET: Especialidad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Especialidades == null)
            {
                return NotFound();
            }

            var especialidadViewModel = await _context.Especialidades
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especialidadViewModel == null)
            {
                return NotFound();
            }

            return View(especialidadViewModel);
        }

        // POST: Especialidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Especialidades == null)
            {
                return Problem("Entity set 'citasContext.EspecialidadViewModel'  is null.");
            }
            var especialidadViewModel = await _context.Especialidades.FindAsync(id);
            if (especialidadViewModel != null)
            {
                _context.Especialidades.Remove(especialidadViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialidadViewModelExists(int id)
        {
          return (_context.Especialidades?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
