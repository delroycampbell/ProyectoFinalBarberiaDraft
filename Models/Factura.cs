using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace ProyectoFinalDraft.Models
    {
    public class Factura
        {
        [Key]
        public int FacturaId { get; set; }

        [Required, DataType(DataType.Date)]
        public DateTime Fecha { get; set; }

        [StringLength(200)]
        public string? Detalle { get; set; }

        [Required]
        public decimal Total { get; set; }

        //Relacion con uno a uno con Cita
        public int CitaId { get; set; }
        public Cita Cita { get; set; } = null!;

        //Relacion con Usuario muchos a uno con Usuario
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        }
    }
