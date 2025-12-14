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
    [Authorize(Roles = "Admin")]
    public class UsuariosController : Controller
        {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
            {
            _context = context;
            }

        // GET: Usuarios
        public async Task<IActionResult> Index()
            {
            //Cambiar view para ver el nombre de rol en lugar del numero
            return View(await _context.Usuario
                .Include(u => u.Rol)
                .ToListAsync());
            }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
                {
                return NotFound();
                }

            return View(usuario);
            }

        // GET: Usuarios/Create
        public IActionResult Create()
            {
            ViewData["RolId"] = new SelectList(_context.Rol, "RolId", "Nombre");
            return View();
            }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,NombreCompleto,Telefono,Correo,RolId")] Usuario usuario)
            {

            try
                {
                _context.Add(usuario);
                //usuario.UsuarioId=
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            catch (Exception) { }

            //Esta linea recarga la lista de roles si hay error de validación
            ViewData["RolId"] = new SelectList(_context.Rol, "RolId", "Nombre", usuario.RolId);
            return View(usuario);
            }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario == null)
                {
                return NotFound();
                }

            ViewData["RolId"] = new SelectList(_context.Rol, "RolId", "Nombre", usuario.RolId);
            return View(usuario);
            }


        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, Usuario usuario)
            {
            if (id != usuario.UsuarioId)
                {
                return NotFound();
                }

            try
                {

                var dbUser = await _context.Usuario.FindAsync(id);
                if (dbUser == null)
                    {
                    return NotFound();
                    }

                dbUser.NombreCompleto = usuario.NombreCompleto;
                dbUser.Telefono = usuario.Telefono;
                dbUser.Correo = usuario.Correo;
                dbUser.RolId = usuario.RolId;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
                }
            catch (Exception)
                {
                ViewData["RolId"] = new SelectList(_context.Rol, "RolId", "Nombre", usuario.RolId);
                return View(usuario);
                }

            }


        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
            if (id == null)
                {
                return NotFound();
                }

            var usuario = await _context.Usuario
                .FirstOrDefaultAsync(m => m.UsuarioId == id);
            if (usuario == null)
                {
                return NotFound();
                }

            return View(usuario);
            }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var usuario = await _context.Usuario.FindAsync(id);
            if (usuario != null)
                {
                _context.Usuario.Remove(usuario);
                }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            }

        private bool UsuarioExists(int id)
            {
            return _context.Usuario.Any(e => e.UsuarioId == id);
            }
        }
    }
