using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDraft.Models
    {
    public class Usuario
        {
        [Key]
        public int UsuarioId { get; set; }

        [Required, StringLength(100)]
        public string NombreCompleto { get; set; }

        [Required,StringLength(20)]
        [RegularExpression(@"^\d+$", ErrorMessage = "Solo se permiten números.")]
        public string Telefono { get; set; }
        [Required, EmailAddress, StringLength(100)]
        public string Correo { get; set; }

        //Relacion de 1 a muchos con Rol 
        [Required(ErrorMessage ="Seleccione un Rol.")]
        public int RolId { get; set; }

        public Rol Rol { get; set; } = null!;
        //Relacion con Cita y Facturas
        public ICollection<Cita> Citas { get; set; } = new List<Cita>();
        public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
        }


        }
