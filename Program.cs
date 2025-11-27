using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Data;
using Microsoft.AspNetCore.Identity;
using ProyectoFinalDraft.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AppDbContext")
        ?? throw new InvalidOperationException("Connection string 'AppDbContext' not found.")
    )
);

// ACTIVAR IDENTITY CON ROLES
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddRoles<IdentityRole>() 
.AddDefaultTokenProviders();

//La ruta de loing, logout y register

builder.Services.ConfigureApplicationCookie(options =>

{

    options.LoginPath = "/Identity/Account/Login";

    options.LogoutPath = "/Identity/Account/Logout";

    options.AccessDeniedPath = "/Identity/Account/Register"; //**Crear vista de Accesso Denegado**

});

//Container's Services

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
//No enviar correos  
builder.Services.AddTransient<IEmailSender>(provider => new NoOpEmailSender());

var app = builder.Build();
app.MapRazorPages();


// SEED DE ROLES
using (var scope = app.Services.CreateScope())
    {
    var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = { "Admin", "Barbero", "Cliente" };

    foreach (var rol in roles)
        {
        if (!await RoleManager.RoleExistsAsync(rol))
            {
            await RoleManager.CreateAsync(new IdentityRole(rol));
            }
        }
    }

if (!app.Environment.IsDevelopment())
    {
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);



app.Run();
