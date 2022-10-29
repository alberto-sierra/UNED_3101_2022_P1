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
    public class ConsultorioController : Controller
    {
        private readonly citasContext _context;

        public ConsultorioController(citasContext context)
        {
            _context = context;
        }

        // GET: Consultorio
        public async Task<IActionResult> Index()
        {
              return _context.Consultorios != null ? 
                          View(await _context.Consultorios.ToListAsync()) :
                          Problem("Entity set 'citasContext.Consultorios'  is null.");
        }

        // GET: Consultorio/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Consultorios == null)
            {
                ViewBag.mensaje = "Consultorio no encontrado.";
                return RedirectToAction("Index", "Consultorio");
            }

            var consultorioViewModel = await _context.Consultorios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultorioViewModel == null)
            {
                ViewBag.mensaje = "Consultorio no encontrado.";
                return RedirectToAction("Index", "Consultorio");
            }

            return View(consultorioViewModel);
        }

        // GET: Consultorio/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Consultorio/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Numero")] ConsultorioViewModel consultorioViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consultorioViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(consultorioViewModel);
        }

        // GET: Consultorio/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Consultorios == null)
            {
                return NotFound();
            }

            var consultorioViewModel = await _context.Consultorios.FindAsync(id);
            if (consultorioViewModel == null)
            {
                return NotFound();
            }
            return View(consultorioViewModel);
        }

        // POST: Consultorio/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Numero")] ConsultorioViewModel consultorioViewModel)
        {
            if (id != consultorioViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(consultorioViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConsultorioViewModelExists(consultorioViewModel.Id))
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
            return View(consultorioViewModel);
        }

        // GET: Consultorio/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Consultorios == null)
            {
                return NotFound();
            }

            var consultorioViewModel = await _context.Consultorios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (consultorioViewModel == null)
            {
                return NotFound();
            }

            return View(consultorioViewModel);
        }

        // POST: Consultorio/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Consultorios == null)
            {
                return Problem("Entity set 'citasContext.ConsultorioViewModel'  is null.");
            }
            var consultorioViewModel = await _context.Consultorios.FindAsync(id);
            if (consultorioViewModel != null)
            {
                _context.Consultorios.Remove(consultorioViewModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConsultorioViewModelExists(int id)
        {
          return (_context.Consultorios?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
