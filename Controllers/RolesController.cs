using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Data;
using ProyectoFinalDraft.Models;

namespace ProyectoFinalDraft.Controllers
    {
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
        {
        private readonly AppDbContext _context;

        public RolesController(AppDbContext context)
            {
            _context = context;
            }

        // GET: Roles
        public async Task<IActionResult> Index()
            {
            return View(await _context.Rol.ToListAsync());
            }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(int? id)
            {
            if (id == null)
                return NotFound();

            var rol = await _context.Rol
                .FirstOrDefaultAsync(m => m.RolId == id);

            if (rol == null)
                return NotFound();

            return View(rol);
            }

        // GET: Roles/Create
        public IActionResult Create()
            {
            return View();
            }

        // POST: Roles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RolId,Nombre")] Rol rol)
            {
            if (ModelState.IsValid)
                {
                _context.Add(rol);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
            return View(rol);
            }

        // GET: Roles/Edit/5  (BLOQUEADO)
        public async Task<IActionResult> Edit(int? id)
            {
            if (id == null)
                return NotFound();

            var rol = await _context.Rol.FindAsync(id);

            if (rol == null)
                return NotFound();

            return View(rol); // Vista mostrará datos readonly
            }

        // POST: Roles/Edit/5 (BLOQUEADO)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Rol rol)
            {
            var dbRol = await _context.Rol.FindAsync(id);

            if (dbRol == null)
                return NotFound();

            ModelState.AddModelError("", "La edición de roles está deshabilitada por motivos de seguridad.");

            return View(dbRol); // Mostrar el rol original
            }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
            if (id == null)
                return NotFound();

            var rol = await _context.Rol
                .FirstOrDefaultAsync(m => m.RolId == id);

            if (rol == null)
                return NotFound();

            // Conteo de usuarios asociados
            var usuariosAsociados = await _context.Usuario
                .CountAsync(u => u.RolId == id);

            ViewBag.UsuariosAsociados = usuariosAsociados;

            return View(rol);
            }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
            var rol = await _context.Rol.FindAsync(id);

            if (rol != null)
                {
                _context.Rol.Remove(rol);
                }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
            }

        private bool RolExists(int id)
            {
            return _context.Rol.Any(e => e.RolId == id);
            }
        }
    }
