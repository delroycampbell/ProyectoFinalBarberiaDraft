using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoFinalDraft.Models
    {
    public class Cita
        {
        [Key]
        public int CitaId { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [StringLength(200)]
        public string? Detalle { get; set; }

        //Estado de cita: "Agendada","Cancelada", "Completada"
        [Required]
        [ForeignKey("EstadoCitaId")]
        public int EstadoCitaId { get; set; }

        // Relación uno a uno con EstadoCita, 1:1 
        public EstadoCita EstadoCita { get; set; } = null!;
        // Relación uno a uno con Factura, es nula porque una cita puede agendarse y pagar hasta ser completada
        public Factura? Factura { get; set; }

        // Relación muchos a uno con Usuario (Cliente)
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        //Muchos a muchos con CitaServicios 
        public ICollection<CitaServicio> CitaServicios { get; set; } = new List<CitaServicio>();
        }


    }
