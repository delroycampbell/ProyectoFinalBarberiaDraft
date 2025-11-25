using Microsoft.AspNetCore.Identity;

namespace ProyectoFinalDraft.Models
    {
    public class ApplicationUser : IdentityUser
        {
        public string NombreCompleto { get; set; } = null!;

        }

    }