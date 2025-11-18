using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prograbarberia.Models
{
 public class Cita
 {
 public int Id { get; set; }

 [Required]
 public int ClienteId { get; set; }
 public Cliente Cliente { get; set; }

 [Required]
 public int ServicioId { get; set; }
 public Servicio Servicio { get; set; }

 [Required]
 public int EmpleadoId { get; set; }
 public Empleado Empleado { get; set; }

 [Required]
 public DateTime FechaHora { get; set; }
 }

}