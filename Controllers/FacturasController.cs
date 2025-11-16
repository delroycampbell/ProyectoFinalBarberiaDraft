using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Data;
using ProyectoFinalDraft.Models;

namespace ProyectoFinalDraft.Controllers
    {
    public class FacturasController : Controller
        {
        private readonly AppDbContext _context;

        public FacturasController(AppDbContext context)
            {
            _context = context;
            }

        // GET: Facturas
        public async Task<IActionResult> Index()
            {
            var appDbContext = _context.Factura.Include(f => f.Cita).Include(f => f.Usuario);
            return View(await appDbContext.ToListAsync());
            }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var factura = await _context.Factura
                .Include(f => f.Cita)
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
                {
                return NotFound();
                }

            return View(factura);
            }

        // GET: Facturas/Create
        public IActionResult Create()
            {
            return View();
            }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FacturaId,Fecha,Detalle,Total,CitaId,UsuarioId")] Factura factura)
            {

            try
                {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            catch { }
            ViewData["CitaId"] = new SelectList(_context.Cita, "CitaId", "CitaId", factura.CitaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "UsuarioId", "Nombre", factura.UsuarioId);
            return View(factura);
            }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var factura = await _context.Factura.FindAsync(id);
            if (factura == null)
                {
                return NotFound();
                }
            ViewData["CitaId"] = new SelectList(_context.Cita, "CitaId", "CitaId", factura.CitaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "UsuarioId", "Nombre", factura.UsuarioId);
            return View(factura);
            }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FacturaId,Fecha,Detalle,Total,CitaId,UsuarioId")] Factura factura)
            {
            if (id != factura.FacturaId)
                {
                return NotFound();
                }

            if (ModelState.IsValid)
                {
                try
                    {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                    }
                catch (DbUpdateConcurrencyException)
                    {
                    if (!FacturaExists(factura.FacturaId))
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
            ViewData["CitaId"] = new SelectList(_context.Cita, "CitaId", "CitaId", factura.CitaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "UsuarioId", "Correo", factura.UsuarioId);
            return View(factura);
            }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var factura = await _context.Factura
                .Include(f => f.Cita)
                .Include(f => f.Usuario)
                .FirstOrDefaultAsync(m => m.FacturaId == id);
            if (factura == null)
                {
                return NotFound();
                }

            return View(factura);
            }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var factura = await _context.Factura.FindAsync(id);
            if (factura != null)
                {
                _context.Factura.Remove(factura);
                }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }

        private bool FacturaExists(int id)
            {
            return _context.Factura.Any(e => e.FacturaId == id);
            }
        }
    }
