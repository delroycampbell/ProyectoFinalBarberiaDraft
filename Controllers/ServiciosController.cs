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
    public class ServiciosController : Controller
        {
        private readonly AppDbContext _context;

        public ServiciosController(AppDbContext context)
            {
            _context = context;
            }

        // GET: Servicios
        public async Task<IActionResult> Index()
            {
            return View(await _context.Servicio.ToListAsync());
            }

        // GET: Servicios/Details/5
        public async Task<IActionResult> Details(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var servicio = await _context.Servicio
                .FirstOrDefaultAsync(m => m.ServicioId == id);
            if (servicio == null)
                {
                return NotFound();
                }

            return View(servicio);
            }

        // GET: Servicios/Create
        public IActionResult Create()
            {
            return View();
            }

        // POST: Servicios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicioId,Nombre,Precio,Descripcion")] Servicio servicio)
            {
            try
                {
                _context.Add(servicio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            catch (Exception) { }

            return View(servicio);
            }

        // GET: Servicios/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
                {
                return NotFound();
                }
            return View(servicio);
            }

        // POST: Servicios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servicio servicio)
            {
            if (id != servicio.ServicioId)
                {
                return NotFound();
                }

            try
                {
                var dbServico = await _context.Servicio.FindAsync(id);
                if (dbServico == null)
                    {
                    return NotFound();
                    }

                dbServico.Nombre = servicio.Nombre;
                dbServico.Precio = servicio.Precio;
                dbServico.Descripcion = servicio.Descripcion;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            catch (Exception)
                {
                return View(servicio);
                }

            }

        // GET: Servicios/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var servicio = await _context.Servicio
                .FirstOrDefaultAsync(m => m.ServicioId == id);
            if (servicio == null)
                {
                return NotFound();
                }

            return View(servicio);
            }

        // POST: Servicios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var servicio = await _context.Servicio.FindAsync(id);
            if (servicio != null)
                {
                _context.Servicio.Remove(servicio);
                }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }

        private bool ServicioExists(int id)
            {
            return _context.Servicio.Any(e => e.ServicioId == id);
            }
        }
    }
