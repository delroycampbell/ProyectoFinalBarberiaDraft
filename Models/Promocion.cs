using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDraft.Models
    {
    public class Promocion
        {
        [Key]
        public int PromocionId { get; set; }

        [Required]
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = string.Empty;

        [Required,DataType(DataType.Date)]
        public DateTime FechaInicio { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime FechaFin { get; set; }
        public decimal Descuento { get; set; }

        public int? ServicioId { get; set; }
        }
    }