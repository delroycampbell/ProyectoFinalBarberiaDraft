using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Data;
using ProyectoFinalDraft.Interfaces;
using ProyectoFinalDraft.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoFinalDraft.Controllers
    {
    public class PromocionesController : Controller
        {
        private readonly AppDbContext _context;
        private readonly ISubjectPromotion _notificador;
        private readonly UserManager<ApplicationUser> _UserManager;


        public PromocionesController(AppDbContext context, ISubjectPromotion notificador, UserManager<ApplicationUser> userManager)
            {
            _context = context;
            _notificador = notificador;
            _UserManager = userManager;
            }

        [Authorize] //Solo visible para usuarios autenticados

        //Promociones/Subscribe

        public async Task<IActionResult> MisPromociones()
            {

            //Buscar IdenityUserId del usuario autenticado
            var user = await _UserManager.GetUserAsync(User);

            //Vincular con la tabla de Usuario

            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.IdentityUserId == user.Id);

            if (usuario == null)
                {
                return RedirectToAction("Index", "Home");
                }

            //Verificar si el usuario está suscrito
            if (!usuario.EstaSuscritoPromociones)
                {
                //Si no está suscrito, redirigir a la vista de no suscrito
                return View("NoSuscrito");
                }

            //Filtrar promociones vigentes

            var promocionesVigentes = await _context.Promocion
                .Where(p => p.FechaFin >= DateTime.Now)
                .ToListAsync();
            return View("Subscrito", promocionesVigentes);

            }


        // GET: Promociones
        public async Task<IActionResult> Index()
            {
            return View(await _context.Promocion.ToListAsync());
            }

        // GET: Promociones/Details/5
        public async Task<IActionResult> Details(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var promocion = await _context.Promocion
                .FirstOrDefaultAsync(m => m.PromocionId == id);
            if (promocion == null)
                {
                return NotFound();
                }

            return View(promocion);
            }

        // GET: Promociones/Create
        public IActionResult Create()
            {
            return View();
            }

        // POST: Promociones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PromocionId,Titulo,Descripcion,FechaInicio,FechaFin,Descuento,ServicioId")] Promocion promocion)
            {
            if (ModelState.IsValid)
                {
                _context.Add(promocion);
                await _context.SaveChangesAsync();
                _notificador.Notify(promocion);
                return RedirectToAction(nameof(Index));
                }
            return View(promocion);
            }

        // GET: Promociones/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var promocion = await _context.Promocion.FindAsync(id);
            if (promocion == null)
                {
                return NotFound();
                }
            return View(promocion);
            }

        // POST: Promociones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PromocionId,Titulo,Descripcion,FechaInicio,FechaFin,Descuento,ServicioId")] Promocion promocion)
            {
            if (id != promocion.PromocionId)
                {
                return NotFound();
                }

            if (ModelState.IsValid)
                {
                try
                    {
                    _context.Update(promocion);
                    await _context.SaveChangesAsync();
                    }
                catch (DbUpdateConcurrencyException)
                    {
                    if (!PromocionExists(promocion.PromocionId))
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
            return View(promocion);
            }

        // GET: Promociones/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var promocion = await _context.Promocion
                .FirstOrDefaultAsync(m => m.PromocionId == id);
            if (promocion == null)
                {
                return NotFound();
                }

            return View(promocion);
            }

        // POST: Promociones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var promocion = await _context.Promocion.FindAsync(id);
            if (promocion != null)
                {
                _context.Promocion.Remove(promocion);
                }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }

        private bool PromocionExists(int id)
            {
            return _context.Promocion.Any(e => e.PromocionId == id);
            }
        }
    }
