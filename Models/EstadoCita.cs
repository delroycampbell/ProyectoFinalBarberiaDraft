using System.ComponentModel.DataAnnotations;

namespace ProyectoFinalDraft.Models
    {
    public class EstadoCita
        {
        [Key]
        public int EstadoCitaId { get; set; }

        [Required, StringLength(50)]
        public string Nombre { get; set; }

        //Relacion 1:N Con Cita

        public virtual ICollection<Cita> Citas { get; set; } = new List<Cita>();

        }
    }