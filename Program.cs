using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using ProyectoFinalDraft.Data;
using ProyectoFinalDraft.Interfaces;
using ProyectoFinalDraft.Models;
using ProyectoFinalDraft.Services;
using static System.Formats.Asn1.AsnWriter;

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
builder.Services.AddSingleton<ISubjectPromotion, NotificadorPromocion>();
builder.Services.AddSingleton<IObserverPromotion, FakeEmailPromotionObserver>();

var app = builder.Build();
app.MapRazorPages();

// Conectar Subject con sus Observadores
using (var scope = app.Services.CreateScope())
    {
    var subject = scope.ServiceProvider.GetRequiredService<ISubjectPromotion>();
    var observer = scope.ServiceProvider.GetRequiredService<IObserverPromotion>();

    subject.Attach(observer);
    }



// SEED DE ROLES

using (var scope = app.Services.CreateScope())
    {
    var services = scope.ServiceProvider;

    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var context = services.GetRequiredService<AppDbContext>();

    await SeedUsers.SeedAsync(userManager, roleManager, context);
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
