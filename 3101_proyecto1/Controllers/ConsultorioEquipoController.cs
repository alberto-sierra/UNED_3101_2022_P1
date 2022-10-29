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
    public class ConsultorioEquipoController : Controller
    {
        private readonly citasContext _context;

        public ConsultorioEquipoController(citasContext context)
        {
            _context = context;
        }

        // GET: ConsultorioEquipo
        public async Task<IActionResult> Index()
        {
              return _context.ConsultorioEquipos != null ? 
                          View(await _context.ConsultorioEquipos.ToListAsync()) :
                          Problem("Entity set 'citasContext.ConsultorioEquipoViewModel'  is null.");
        }

        // GET: ConsultorioEquipo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ConsultorioEquipos == null)
            {
                return NotFound();
            }

            var consultorioEquipoViewModel = await _context.ConsultorioEquipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultorioEquipoViewModel == null)
            {
                return NotFound();
            }

            return View(consultorioEquipoViewModel);
        }

        // GET: ConsultorioEquipo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConsultorioEquipo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdConsultorio,IdEquipo")] ConsultorioEquipoViewModel consultorioEquipoViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultorioEquipoViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultorioEquipoViewModel);
        }

        // GET: ConsultorioEquipo/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ConsultorioEquipos == null)
            {
                return NotFound();
            }

            var consultorioEquipoViewModel = await _context.ConsultorioEquipos.FindAsync(id);
            if (consultorioEquipoViewModel == null)
            {
                return NotFound();
            }
            return View(consultorioEquipoViewModel);
        }

        // POST: ConsultorioEquipo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdConsultorio,IdEquipo")] ConsultorioEquipoViewModel consultorioEquipoViewModel)
        {
            if (id != consultorioEquipoViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultorioEquipoViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultorioEquipoViewModelExists(consultorioEquipoViewModel.Id))
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
            return View(consultorioEquipoViewModel);
        }

        // GET: ConsultorioEquipo/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ConsultorioEquipos == null)
            {
                return NotFound();
            }

            var consultorioEquipoViewModel = await _context.ConsultorioEquipos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultorioEquipoViewModel == null)
            {
                return NotFound();
            }

            return View(consultorioEquipoViewModel);
        }

        // POST: ConsultorioEquipo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ConsultorioEquipos == null)
            {
                return Problem("Entity set 'citasContext.ConsultorioEquipoViewModel'  is null.");
            }
            var consultorioEquipoViewModel = await _context.ConsultorioEquipos.FindAsync(id);
            if (consultorioEquipoViewModel != null)
            {
                _context.ConsultorioEquipos.Remove(consultorioEquipoViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultorioEquipoViewModelExists(int id)
        {
          return (_context.ConsultorioEquipos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
