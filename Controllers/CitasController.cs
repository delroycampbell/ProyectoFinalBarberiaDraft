using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Data;
using ProyectoFinalDraft.Models;

namespace ProyectoFinalDraft.Controllers
    {
    public class CitasController : Controller
        {
        private readonly AppDbContext _context;

        public CitasController(AppDbContext context)
            {
            _context = context;
            }
        //Solo visible para rol de "Administrador" y "Recepcionista"

      //  [Authorize(Roles = "Administrador, Barbero, Clinte")]


        // GET: Citas
        public async Task<IActionResult> Index()
            {
            var appDbContext = _context.Cita
                .Include(c => c.EstadoCita)
                .Include(c => c.Usuario)
                .Include(C => C.CitaServicios)
                    .ThenInclude(cs => cs.Servicio);
            return View(await appDbContext.ToListAsync());
            }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var cita = await _context.Cita
                .Include(c => c.EstadoCita)
                .Include(c => c.Usuario)
                .Include(c => c.Factura)
                .FirstOrDefaultAsync(m => m.CitaId == id);

            if (cita == null)
                {
                return NotFound();
                }

            return View(cita);
            }

        // GET: Citas/Create
        public IActionResult Create()
            {
            ViewData["EstadoCitaId"] = new SelectList(_context.EstadoCita, "EstadoCitaId", "Nombre");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "UsuarioId", "NombreCompleto");
            //Agregar la lista de servicios en el formulario 
            ViewData["Servicios"] = _context.Servicio.ToList();
            return View();
            }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
         [Bind("CitaId,Fecha,Detalle,EstadoCitaId,UsuarioId")] Cita cita,
         string ServiciosSeleccionados)
            {
            // VALIDACIÓN: debe seleccionar al menos un servicio
            if (string.IsNullOrEmpty(ServiciosSeleccionados))
                {
                ModelState.AddModelError("", "Debe seleccionar al menos un servicio.");

                ViewData["EstadoCitaId"] = new SelectList(_context.EstadoCita, "EstadoCitaId", "Nombre", cita.EstadoCitaId);
                ViewData["UsuarioId"] = new SelectList(_context.Usuario, "UsuarioId", "NombreCompleto", cita.UsuarioId);
                ViewData["Servicios"] = _context.Servicio.ToList();

                return View(cita);
                }

            try
                {
                // Guardar cita primero
                _context.Add(cita);
                await _context.SaveChangesAsync();

                // Guardar servicio asociado
                var servicioId = int.Parse(ServiciosSeleccionados);

                _context.CitaServicio.Add(new CitaServicio
                    {
                    CitaId = cita.CitaId,
                    ServicioId = servicioId
                    });

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                }
            catch
                {
                ViewData["EstadoCitaId"] = new SelectList(_context.EstadoCita, "EstadoCitaId", "Nombre", cita.EstadoCitaId);
                ViewData["UsuarioId"] = new SelectList(_context.Usuario, "UsuarioId", "NombreCompleto", cita.UsuarioId);
                ViewData["Servicios"] = _context.Servicio.ToList();
                return View(cita);
                }
            }



        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }
            var cita = await _context.Cita.FindAsync(id);
            if (cita == null)
                {
                return NotFound();
                }
            ViewData["EstadoCitaId"] = new SelectList(_context.EstadoCita, "EstadoCitaId", "Nombre", cita.EstadoCitaId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "UsuarioId", "NombreCompleto", cita.UsuarioId);
            ViewData["Servicios"] = _context.Servicio.ToList();

            return View(cita);
            }




        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cita cita)
            {
            if (id != cita.CitaId)
                {
                return NotFound();
                }

            try
                {
                var dbCita = await _context.Cita.FindAsync(id);
                if (dbCita == null)
                    {
                    return NotFound();
                    }
                //el metodo funciona pero no deberia pasar el id, no se deberian editar bajo nigun esceario.
                dbCita.Fecha = cita.Fecha;
                dbCita.Detalle = cita.Detalle;
                dbCita.EstadoCitaId = cita.EstadoCitaId;
                dbCita.UsuarioId = cita.UsuarioId;

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            catch (Exception)
                {
                ViewData["EstadoCitaId"] = new SelectList(_context.EstadoCita, "EstadoCitaId", "Nombre", cita.EstadoCitaId);
                ViewData["UsuarioId"] = new SelectList(_context.Usuario, "UsuarioId", "NombreCompleto", cita.UsuarioId);
                ViewData["Servicios"] = _context.Servicio.ToList();
                return View(cita);
                }
            }


        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var cita = await _context.Cita
                .Include(c => c.EstadoCita)
                .Include(c => c.Usuario)
                .FirstOrDefaultAsync(m => m.CitaId == id);

            if (cita == null)
                {
                return NotFound();
                }

            return View(cita);
            }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var cita = await _context.Cita.FindAsync(id);

            if (cita != null)
                {
                _context.Cita.Remove(cita);
                }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }

        private bool CitaExists(int id)
            {
            return _context.Cita.Any(e => e.CitaId == id);
            }
        }
    }
