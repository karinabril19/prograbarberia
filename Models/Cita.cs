using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prograbarberia.Models
{


 public class Cita
 {

         [Key]
         public int CitaId { get; set; }

         [Required]
           public Empleado Empleado { get; set; }

         [Required]
         public DateTime FechaHora { get; set; }

        // Clave foránea para la relación uno a muchos

        [Required]
        [ForeignKey("ClienteId")]
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        // Propiedad de navegación para la relación uno a muchos
        public ICollection<Servicio> ListaServicios { get; set; } = new List<Servicio>();

  }
}

