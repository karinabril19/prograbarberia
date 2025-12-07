using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prograbarberia.Models
{
 public class Empleado
 {
 public int EmpleadoId { get; set; }

 [Required]
 [MaxLength(100)]
 public string Nombre { get; set; }

 [Required]
 [MaxLength(100)]
 public string Especialidad { get; set; }

 public List<Cita> ListaCitas { get; set; } = new List<Cita>();
 }
}