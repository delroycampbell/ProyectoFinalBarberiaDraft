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
            // Solo citas sin factura
            var citasSinFactura = _context.Cita
                .Where(c => c.Factura == null)
                .Select(c => new
                    {
                    c.CitaId,
                    Texto = "Cita #" + c.CitaId + " - " + c.Fecha.ToString("dd/MM/yyyy")
                    })
                .ToList();

            ViewBag.CitaId = new SelectList(citasSinFactura, "CitaId", "Texto");

            return View();
            }


        [HttpGet]
        public async Task<IActionResult> GetFacturaDatos(int citaId)
            {
            var cita = await _context.Cita
                .Include(c => c.Usuario)
                .Include(c => c.CitaServicios)
                .ThenInclude(cs => cs.Servicio)
                .FirstOrDefaultAsync(c => c.CitaId == citaId);

            if (cita == null)
                {
                return Json(new { error = "Cita no encontrada" });
                }

            var subtotal = cita.CitaServicios.Sum(s => s.Servicio.Precio);
            var iva = subtotal * 0.13m;
            var total = subtotal + iva;

            return Json(new
                {
                usuarioId = cita.UsuarioId,
                total = total,
                fecha = DateTime.Now.ToString("yyyy-MM-dd")
                });
            }
        //El endpoint recibe un citaId.
        //Busca la cita real con su usuario y servicios.
        //Calcula los valores.
        //Devuelve un JSON.
        //El script de tu vista llena los campos.


        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Factura factura)
            {
            // Buscar cita con todo lo necesario
            var cita = await _context.Cita
                .Include(c => c.Usuario)
                .Include(c => c.CitaServicios)
                .ThenInclude(cs => cs.Servicio)
                .FirstOrDefaultAsync(c => c.CitaId == factura.CitaId);

            if (cita == null)
                {
                ModelState.AddModelError("", "La cita no existe.");
                return View(factura);
                }

            // Calcular montos derivados
            var subtotal = cita.CitaServicios.Sum(s => s.Servicio.Precio);
            var iva = subtotal * 0.13m;
            var total = subtotal + iva;

            // Cargar datos automáticos
            factura.UsuarioId = cita.UsuarioId;
            factura.Total = total;
            factura.Fecha = DateTime.Now;

            // Guardar factura
            _context.Add(factura);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
