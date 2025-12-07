//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using prograbarberia.Data;
//using prograbarberia.Models;

//namespace prograbarberia.Services
//{
// public class ServicioService
// {
// private readonly BarberiaDbContext _db;
// public ServicioService(BarberiaDbContext db)
// {
// _db = db;
// }

// public async Task<Servicio> CrearAsync(Servicio servicio)
// {
// servicio.Id = _db.GetNextServicioId();
// _db.Servicios.Add(servicio);
// await _db.SaveChangesAsync();
// return servicio;
// }

// public async Task<Servicio> ObtenerPorIdAsync(int id)
// {
// return await Task.FromResult(_db.Servicios.FirstOrDefault(s => s.Id == id));
// }

// public async Task<List<Servicio>> ListarAsync()
// {
// return await Task.FromResult(_db.Servicios.ToList());
// }

// public async Task<Servicio> ActualizarAsync(Servicio servicio)
// {
// var existing = _db.Servicios.FirstOrDefault(s => s.Id == servicio.Id);
// if (existing == null) return null;
// existing.Nombre = servicio.Nombre;
// existing.Precio = servicio.Precio;
// existing.DuracionMinutos = servicio.DuracionMinutos;
// await _db.SaveChangesAsync();
// return existing;
// }

// public async Task<bool> EliminarAsync(int id)
// {
// var existing = _db.Servicios.FirstOrDefault(s => s.Id == id);
// if (existing == null) return false;
// _db.Servicios.Remove(existing);
// await _db.SaveChangesAsync();
// return true;
// }
// }
//}