using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prograbarberia.Data;
using prograbarberia.Models;

namespace prograbarberia.Services
{
 public class EmpleadoService
 {
 private readonly BarberiaDbContext _db;
 public EmpleadoService(BarberiaDbContext db)
 {
 _db = db;
 }

 public async Task<Empleado> CrearAsync(Empleado empleado)
 {
 empleado.Id = _db.GetNextEmpleadoId();
 _db.Empleados.Add(empleado);
 await _db.SaveChangesAsync();
 return empleado;
 }

 public async Task<Empleado> ObtenerPorIdAsync(int id)
 {
 return await Task.FromResult(_db.Empleados.FirstOrDefault(e => e.Id == id));
 }

 public async Task<List<Empleado>> ListarAsync()
 {
 return await Task.FromResult(_db.Empleados.ToList());
 }

 public async Task<Empleado> ActualizarAsync(Empleado empleado)
 {
 var existing = _db.Empleados.FirstOrDefault(e => e.Id == empleado.Id);
 if (existing == null) return null;
 existing.Nombre = empleado.Nombre;
 existing.Especialidad = empleado.Especialidad;
 await _db.SaveChangesAsync();
 return existing;
 }

 public async Task<bool> EliminarAsync(int id)
 {
 var existing = _db.Empleados.FirstOrDefault(e => e.Id == id);
 if (existing == null) return false;
 _db.Empleados.Remove(existing);
 await _db.SaveChangesAsync();
 return true;
 }
 }
}