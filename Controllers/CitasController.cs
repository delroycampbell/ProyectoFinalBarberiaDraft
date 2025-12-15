using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Data;
using ProyectoFinalDraft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProyectoFinalDraft.Controllers
    {
    public class CitasController : Controller
        {
        private readonly AppDbContext _context;

        public CitasController(AppDbContext context)
            {
            _context = context;
            }

        [Authorize(Roles = "Admin, Barbero, Cliente")]

        // GET: Citas
        public async Task<IActionResult> Index()
            {
            var citas = _context.Cita
                .Include(c => c.EstadoCita)
                .Include(c => c.Usuario)
                .Include(c => c.CitaServicios)
                    .ThenInclude(cs => cs.Servicio)
                .AsQueryable();

            if (User.IsInRole("Cliente"))
                {
                var identityUserId = User
                    .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
                    .Value;

                var clienteId = _context.Usuario
                    .Where(u => u.IdentityUserId == identityUserId)
                    .Select(u => u.UsuarioId)
                    .FirstOrDefault();

                citas = citas.Where(c => c.UsuarioId == clienteId);
                }

            return View(await citas.ToListAsync());
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
            //Agregar la lista de servicios en el formulario 
            ViewData["Servicios"] = _context.Servicio.ToList();

            if (User.IsInRole("Cliente"))
                {
                //El cliente solo puede ver agendar citas para el
                var identityUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

                var cliente = _context.Usuario
                    .FirstOrDefault(u => u.IdentityUserId == identityUserId);
                ViewData["UsuarioId"] = new SelectList(new List<Usuario> { cliente }, "UsuarioId", "NombreCompleto");
                }
            else
                {
                ViewData["UsuarioId"] = new SelectList(_context.Usuario, "UsuarioId", "NombreCompleto");

                }

            return View();
            }

        // POST: Citas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
         [Bind("CitaId,Fecha,Detalle,EstadoCitaId,UsuarioId")] Cita cita,
         string ServiciosSeleccionados)
            {

            if (string.IsNullOrWhiteSpace(ServiciosSeleccionados))
                {
                ModelState.AddModelError("", "Debe seleccionar un servicio.");

                ViewData["Servicios"] = _context.Servicio.ToList();
                ViewData["EstadoCitaId"] = new SelectList(
                    _context.EstadoCita, "EstadoCitaId", "Nombre", cita.EstadoCitaId
                );

                if (User.IsInRole("Cliente"))
                    {
                    var identityUserId = User
                        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
                        .Value;

                    var cliente = _context.Usuario
                        .FirstOrDefault(u => u.IdentityUserId == identityUserId);

                    ViewData["UsuarioId"] = new SelectList(
                        new List<Usuario> { cliente },
                        "UsuarioId",
                        "NombreCompleto",
                        cliente.UsuarioId
                    );
                    }
                else
                    {
                    ViewData["UsuarioId"] = new SelectList(
                        _context.Usuario,
                        "UsuarioId",
                        "NombreCompleto",
                        cita.UsuarioId
                    );
                    }

                return View(cita);
                }



            try
                {
                if (User.IsInRole("Cliente"))
                    {
                    var identityUserId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                    var cliente = _context.Usuario
                        .FirstOrDefault(u => u.IdentityUserId == identityUserId);
                    cita.UsuarioId = cliente.UsuarioId;
                    }
                // Guardar cita primero
                cita.EstadoCitaId = 2;
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
                ViewData["EstadoCitaId"] = new SelectList(
                    _context.EstadoCita,
                    "EstadoCitaId",
                    "Nombre",
                    cita.EstadoCitaId
                );

                ViewData["Servicios"] = _context.Servicio.ToList();

                if (User.IsInRole("Cliente"))
                    {
                    var identityUserId = User
                        .FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?
                        .Value;

                    var cliente = _context.Usuario
                        .FirstOrDefault(u => u.IdentityUserId == identityUserId);

                    ViewData["UsuarioId"] = new SelectList(
                        new List<Usuario> { cliente },
                        "UsuarioId",
                        "NombreCompleto",
                        cliente.UsuarioId
                    );
                    }
                else
                    {
                    ViewData["UsuarioId"] = new SelectList(
                        _context.Usuario,
                        "UsuarioId",
                        "NombreCompleto",
                        cita.UsuarioId
                    );
                    }

                return View(cita);
                }
            }


        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
            if (id == null)
                return NotFound();

            if (User.IsInRole("Cliente"))
                return Forbid();

            var cita = await _context.Cita.FindAsync(id);
            if (cita == null)
                return NotFound();

            ViewData["EstadoCitaId"] = new SelectList(
                _context.EstadoCita,
                "EstadoCitaId",
                "Nombre",
                cita.EstadoCitaId
            );

            ViewData["UsuarioId"] = new SelectList(
                _context.Usuario,
                "UsuarioId",
                "NombreCompleto",
                cita.UsuarioId
            );

            ViewData["Servicios"] = _context.Servicio.ToList();
            return View(cita);
            }




        // POST: Citas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Barbero")]
        public async Task<IActionResult> Edit(int id, Cita cita)
            {
            if (id != cita.CitaId)
                return NotFound();

            var dbCita = await _context.Cita.FindAsync(id);
            if (dbCita == null)
                return NotFound();

            dbCita.Fecha = cita.Fecha;
            dbCita.Detalle = cita.Detalle;
            dbCita.EstadoCitaId = cita.EstadoCitaId;
            dbCita.UsuarioId = cita.UsuarioId;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }



        [Authorize(Roles = "Admin, Barbero")]
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
        [Authorize(Roles = "Admin, Barbero")]
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
