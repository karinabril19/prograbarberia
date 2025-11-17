using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prograbarberia.Data;
using prograbarberia.Models;

namespace prograbarberia.Services
{
 public class CitaService
 {
 private readonly BarberiaDbContext _db;
 public CitaService(BarberiaDbContext db)
 {
 _db = db;
 }

 public async Task<(bool Ok, string Error, Cita Cita)> CrearAsync(Cita cita)
 {
 // Validar fechas
 var now = DateTime.UtcNow;
 var fechaUtc = cita.FechaHora.ToUniversalTime();
 if (fechaUtc <= now)
 return (false, "No se permiten fechas pasadas.", null);
 if (fechaUtc > now.AddDays(30))
 return (false, "No se permiten fechas con más de30 días de anticipación.", null);

 // Validar existencia de entidades
 var cliente = _db.Clientes.FirstOrDefault(c => c.Id == cita.ClienteId);
 if (cliente == null) return (false, "Cliente no existe.", null);
 var servicio = _db.Servicios.FirstOrDefault(s => s.Id == cita.ServicioId);
 if (servicio == null) return (false, "Servicio no existe.", null);
 var empleado = _db.Empleados.FirstOrDefault(e => e.Id == cita.EmpleadoId);
 if (empleado == null) return (false, "Empleado no existe.", null);

 // Validar disponibilidad del empleado
 var duracion = TimeSpan.FromMinutes(servicio.DuracionMinutos);
 var inicio = fechaUtc;
 var fin = inicio.Add(duracion);

 var conflictos = _db.Citas
 .Where(c => c.EmpleadoId == cita.EmpleadoId)
 .Where(c =>
 {
 var cInicio = c.FechaHora.ToUniversalTime();
 var cFin = cInicio.AddMinutes((_db.Servicios.FirstOrDefault(s => s.Id == c.ServicioId)?.DuracionMinutos) ??0);
 return !(cFin <= inicio || cInicio >= fin);
 })
 .ToList();

 if (conflictos.Any())
 return (false, "El empleado no está disponible en ese horario (conflicto con otra cita).", null);

 // Si pasa todo, crear cita
 cita.Id = _db.GetNextCitaId();
 _db.Citas.Add(cita);
 await _db.SaveChangesAsync();
 return (true, null, cita);
 }

 public async Task<Cita> ObtenerPorIdAsync(int id)
 {
 var cita = _db.Citas.FirstOrDefault(c => c.Id == id);
 if (cita == null) return null;
 cita.Cliente = _db.Clientes.FirstOrDefault(cl => cl.Id == cita.ClienteId);
 cita.Empleado = _db.Empleados.FirstOrDefault(em => em.Id == cita.EmpleadoId);
 cita.Servicio = _db.Servicios.FirstOrDefault(s => s.Id == cita.ServicioId);
 return await Task.FromResult(cita);
 }

 public async Task<List<Cita>> ListarAsync()
 {
 var lista = _db.Citas.Select(c =>
 {
 c.Cliente = _db.Clientes.FirstOrDefault(cl => cl.Id == c.ClienteId);
 c.Empleado = _db.Empleados.FirstOrDefault(em => em.Id == c.EmpleadoId);
 c.Servicio = _db.Servicios.FirstOrDefault(s => s.Id == c.ServicioId);
 return c;
 }).ToList();

 return await Task.FromResult(lista);
 }

 public async Task<(bool Ok, string Error, Cita Cita)> ActualizarAsync(Cita cita)
 {
 var existing = _db.Citas.FirstOrDefault(c => c.Id == cita.Id);
 if (existing == null) return (false, "Cita no encontrada.", null);

 // Validar fechas y entidades similar a crear
 var now = DateTime.UtcNow;
 var fechaUtc = cita.FechaHora.ToUniversalTime();
 if (fechaUtc <= now)
 return (false, "No se permiten fechas pasadas.", null);
 if (fechaUtc > now.AddDays(30))
 return (false, "No se permiten fechas con más de30 días de anticipación.", null);

 var cliente = _db.Clientes.FirstOrDefault(c => c.Id == cita.ClienteId);
 if (cliente == null) return (false, "Cliente no existe.", null);
 var servicio = _db.Servicios.FirstOrDefault(s => s.Id == cita.ServicioId);
 if (servicio == null) return (false, "Servicio no existe.", null);
 var empleado = _db.Empleados.FirstOrDefault(e => e.Id == cita.EmpleadoId);
 if (empleado == null) return (false, "Empleado no existe.", null);

 // disponibilidad
 var duracion = TimeSpan.FromMinutes(servicio.DuracionMinutos);
 var inicio = fechaUtc;
 var fin = inicio.Add(duracion);

 var conflictos = _db.Citas
 .Where(c => c.EmpleadoId == cita.EmpleadoId && c.Id != cita.Id)
 .ToList();

 var hayConflicto = conflictos.Any(c =>
 {
 var cInicio = c.FechaHora.ToUniversalTime();
 var cFin = cInicio.AddMinutes((_db.Servicios.FirstOrDefault(s => s.Id == c.ServicioId)?.DuracionMinutos) ??0);
 return !(cFin <= inicio || cInicio >= fin);
 });

 if (hayConflicto) return (false, "El empleado no está disponible en ese horario (conflicto con otra cita).", null);

 // actualizar
 existing.ClienteId = cita.ClienteId;
 existing.ServicioId = cita.ServicioId;
 existing.EmpleadoId = cita.EmpleadoId;
 existing.FechaHora = cita.FechaHora;
 await _db.SaveChangesAsync();
 return (true, null, existing);
 }

 public async Task<bool> EliminarAsync(int id)
 {
 var existing = _db.Citas.FirstOrDefault(c => c.Id == id);
 if (existing == null) return false;
 _db.Citas.Remove(existing);
 await _db.SaveChangesAsync();
 return true;
 }
 }
}