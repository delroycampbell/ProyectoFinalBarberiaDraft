using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDraft.Models
    {
    public class Rol
        {

        [Key]
        public int RolId { get; set; }

        [Required,StringLength(50)]
        public string Nombre { get; set; }

        //Relacion muchos a 1 con Usuario 

        public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();

        }
    }
