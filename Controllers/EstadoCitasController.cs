using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Data;
using ProyectoFinalDraft.Models;

namespace ProyectoFinalDraft.Controllers
{
    public class EstadoCitasController : Controller
    {
        private readonly AppDbContext _context;

        public EstadoCitasController(AppDbContext context)
        {
            _context = context;
        }

        // GET: EstadoCitas
        public async Task<IActionResult> Index()
        {
            return View(await _context.EstadoCita.ToListAsync());
        }

        // GET: EstadoCitas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoCita = await _context.EstadoCita
                .FirstOrDefaultAsync(m => m.EstadoCitaId == id);
            if (estadoCita == null)
            {
                return NotFound();
            }

            return View(estadoCita);
        }

        // GET: EstadoCitas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EstadoCitas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EstadoCitaId,Nombre")] EstadoCita estadoCita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estadoCita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estadoCita);
        }

        // GET: EstadoCitas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoCita = await _context.EstadoCita.FindAsync(id);
            if (estadoCita == null)
            {
                return NotFound();
            }
            return View(estadoCita);
        }

        // POST: EstadoCitas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EstadoCitaId,Nombre")] EstadoCita estadoCita)
        {
            if (id != estadoCita.EstadoCitaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estadoCita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstadoCitaExists(estadoCita.EstadoCitaId))
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
            return View(estadoCita);
        }

        // GET: EstadoCitas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estadoCita = await _context.EstadoCita
                .FirstOrDefaultAsync(m => m.EstadoCitaId == id);
            if (estadoCita == null)
            {
                return NotFound();
            }

            return View(estadoCita);
        }

        // POST: EstadoCitas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estadoCita = await _context.EstadoCita.FindAsync(id);
            if (estadoCita != null)
            {
                _context.EstadoCita.Remove(estadoCita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstadoCitaExists(int id)
        {
            return _context.EstadoCita.Any(e => e.EstadoCitaId == id);
        }
    }
}
