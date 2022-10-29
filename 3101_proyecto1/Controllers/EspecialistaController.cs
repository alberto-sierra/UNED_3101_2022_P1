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
    public class EspecialistaController : Controller
    {
        private readonly citasContext _context;

        public EspecialistaController(citasContext context)
        {
            _context = context;
        }

        // GET: Especialista
        public async Task<IActionResult> Index()
        {
              return _context.Especialistas != null ? 
                          View(await _context.Especialistas.ToListAsync()) :
                          Problem("Entity set 'citasContext.EspecialistumViewModel'  is null.");
        }

        // GET: Especialista/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Especialistas == null)
            {
                return NotFound();
            }

            var especialistumViewModel = await _context.Especialistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especialistumViewModel == null)
            {
                return NotFound();
            }

            return View(especialistumViewModel);
        }

        // GET: Especialista/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Especialista/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,PrecioConsulta,IdEspecialidad")] EspecialistumViewModel especialistumViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(especialistumViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(especialistumViewModel);
        }

        // GET: Especialista/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Especialistas == null)
            {
                return NotFound();
            }

            var especialistumViewModel = await _context.Especialistas.FindAsync(id);
            if (especialistumViewModel == null)
            {
                return NotFound();
            }
            return View(especialistumViewModel);
        }

        // POST: Especialista/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,PrecioConsulta,IdEspecialidad")] EspecialistumViewModel especialistumViewModel)
        {
            if (id != especialistumViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(especialistumViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EspecialistumViewModelExists(especialistumViewModel.Id))
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
            return View(especialistumViewModel);
        }

        // GET: Especialista/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Especialistas == null)
            {
                return NotFound();
            }

            var especialistumViewModel = await _context.Especialistas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (especialistumViewModel == null)
            {
                return NotFound();
            }

            return View(especialistumViewModel);
        }

        // POST: Especialista/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Especialistas == null)
            {
                return Problem("Entity set 'citasContext.EspecialistumViewModel'  is null.");
            }
            var especialistumViewModel = await _context.Especialistas.FindAsync(id);
            if (especialistumViewModel != null)
            {
                _context.Especialistas.Remove(especialistumViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EspecialistumViewModelExists(int id)
        {
          return (_context.Especialistas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
