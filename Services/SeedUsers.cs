namespace ProyectoFinalDraft.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Data;
using ProyectoFinalDraft.Models;

public class SeedUsers
    {
    public static async Task SeedAsync(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        AppDbContext context)
        {
        // Roles requeridos
        string[] roles = { "Admin", "Barbero", "Cliente" };

        foreach (var role in roles)
            {
            if (!await roleManager.RoleExistsAsync(role))
                {
                await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

        // ======================
        // ADMIN
        // ======================
        await CrearUsuarioAsync(
            userManager,
            context,
            email: "admin@barberia.com",
            password: "Admin123!",
            nombre: "Administrador General",
            telefono: "88888888",
            rolIdentity: "Admin",
            rolIdSistema: 1
        );

        // ======================
        // BARBERO
        // ======================
        await CrearUsuarioAsync(
            userManager,
            context,
            email: "barbero@barberia.com",
            password: "Barbero123!",
            nombre: "Barbero Principal",
            telefono: "77777777",
            rolIdentity: "Barbero",
            rolIdSistema: 2
        );
        }

    private static async Task CrearUsuarioAsync(
        UserManager<ApplicationUser> userManager,
        AppDbContext context,
        string email,
        string password,
        string nombre,
        string telefono,
        string rolIdentity,
        int rolIdSistema)
        {
        var user = await userManager.FindByEmailAsync(email);

        if (user != null)
            return;

        user = new ApplicationUser
            {
            UserName = email,
            Email = email,
            EmailConfirmed = true,
            NombreCompleto = nombre,
            PhoneNumber = telefono
            };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            return;

        await userManager.AddToRoleAsync(user, rolIdentity);

        // Tabla Usuario 
        var usuario = new Usuario
            {
            NombreCompleto = nombre,
            Correo = email,
            Telefono = telefono,
            IdentityUserId = user.Id,
            RolId = rolIdSistema,
            EstaSuscritoPromociones = false
            };

        context.Usuario.Add(usuario);
        await context.SaveChangesAsync();
        }
    }

