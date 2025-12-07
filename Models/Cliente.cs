using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prograbarberia.Models
{
 public class Cliente
 {
 public int Id { get; set; }

 [Required]
 [MaxLength(100)]
 public string Nombre { get; set; }

 [Required]
 [Phone]
 [MaxLength(20)]
 public string Telefono { get; set; }

 [Required]
 [EmailAddress]
 [MaxLength(200)]
 public string Correo { get; set; }

 public ICollection<Cita> ListaCitas { get; set; } = new List<Cita>();
 }
}