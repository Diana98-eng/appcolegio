using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appcolegio.Models;

namespace appcolegio.Controllers
{
    public class NotumsController : Controller
    {
        private readonly ColegioContext _context;

        public NotumsController(ColegioContext context)
        {
            _context = context;
        }

        // GET: Notums
        public async Task<IActionResult> Index()
        {
            var colegioContext = _context.Nota.Include(n => n.oEstudiante).Include(n => n.oMateria);
            return View(await colegioContext.ToListAsync());
        }

        // GET: Notums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notum = await _context.Nota
                .Include(n => n.oEstudiante)
                .Include(n => n.oMateria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notum == null)
            {
                return NotFound();
            }

            return View(notum);
        }

        // GET: Notums/Create
        public IActionResult Create()
        {
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "Id", "Nombre");
            ViewData["IdMateria"] = new SelectList(_context.Materia, "Id", "NomMateria");
            return View();
        }

        // POST: Notums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nota,IdEstudiante,IdMateria")] Notum notum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "Id", "Nombre", notum.IdEstudiante);
            ViewData["IdMateria"] = new SelectList(_context.Materia, "Id", "NomMateria", notum.IdMateria);
            return View(notum);
        }

        // GET: Notums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notum = await _context.Nota.FindAsync(id);
            if (notum == null)
            {
                return NotFound();
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "Id", "Nombre", notum.IdEstudiante);
            ViewData["IdMateria"] = new SelectList(_context.Materia, "Id", "NomMateria", notum.IdMateria);
            return View(notum);
        }

        // POST: Notums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nota,IdEstudiante,IdMateria")] Notum notum)
        {
            if (id != notum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotumExists(notum.Id))
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
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "Id", "Nombre", notum.IdEstudiante);
            ViewData["IdMateria"] = new SelectList(_context.Materia, "Id", "NomMateria", notum.IdMateria);
            return View(notum);
        }

        // GET: Notums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notum = await _context.Nota
                .Include(n => n.oEstudiante)
                .Include(n => n.oMateria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (notum == null)
            {
                return NotFound();
            }

            return View(notum);
        }

        // POST: Notums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var notum = await _context.Nota.FindAsync(id);
            if (notum != null)
            {
                _context.Nota.Remove(notum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotumExists(int id)
        {
            return _context.Nota.Any(e => e.Id == id);
        }
    }
}
