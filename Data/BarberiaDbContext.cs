using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prograbarberia.Models;
using System;

namespace prograbarberia.Data
{
 // Implementación simple en memoria para permitir compilación y pruebas básicas.
 public class BarberiaDbContext
 {
 public BarberiaDbContext()
 {
 Clientes = new List<Cliente>();
 Empleados = new List<Empleado>();
 Servicios = new List<Servicio>();
 Citas = new List<Cita>();
 Usuarios = new List<Usuario>();
 }

 public List<Cliente> Clientes { get; set; }
 public List<Empleado> Empleados { get; set; }
 public List<Servicio> Servicios { get; set; }
 public List<Cita> Citas { get; set; }
 public List<Usuario> Usuarios { get; set; }

 public Task SaveChangesAsync()
 {
 // En esta implementación en memoria no es necesario nada especial.
 return Task.CompletedTask;
 }

 // Generadores sencillos de Ids
 public int GetNextClienteId() => Clientes.Any() ? Clientes.Max(c => c.Id) +1 :1;
 public int GetNextEmpleadoId() => Empleados.Any() ? Empleados.Max(e => e.Id) +1 :1;
 public int GetNextServicioId() => Servicios.Any() ? Servicios.Max(s => s.Id) +1 :1;
 public int GetNextCitaId() => Citas.Any() ? Citas.Max(c => c.Id) +1 :1;
 public int GetNextUsuarioId() => Usuarios.Any() ? Usuarios.Max(u => u.Id) +1 :1;
 }
}