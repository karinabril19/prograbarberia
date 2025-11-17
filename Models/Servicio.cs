using System.ComponentModel.DataAnnotations;

namespace prograbarberia.Models
{
 public class Servicio
 {
 public int Id { get; set; }

 [Required]
 [MaxLength(100)]
 public string Nombre { get; set; }

 [Range(0,10000)]
 public decimal Precio { get; set; }

 [Range(1,1440)]
 public int DuracionMinutos { get; set; }
 }
}