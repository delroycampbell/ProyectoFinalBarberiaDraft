using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDraft.Models
    {
    public class Servicio
        {

        [Key]
        public int ServicioId { get; set; }

        [Required, StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        public decimal Precio { get; set; }

        [Required, StringLength(200)]
        public string Descripcion { get; set; }

        //Muchos a muchos con CitaServicios 
        public ICollection<CitaServicio> CitaServicios { get; set; }

        }
    }
