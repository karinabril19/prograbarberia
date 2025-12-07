//using System.Collections.Generic;
//using System.Threading.Tasks;
//using System.Linq;
//using prograbarberia.Data;
//using prograbarberia.Models;

//namespace prograbarberia.Services
//{
// public class ClienteService
// {
// private readonly BarberiaDbContext _db;
// public ClienteService(BarberiaDbContext db)
// {
// _db = db;
// }

// public async Task<Cliente> CrearAsync(Cliente cliente)
// {
// cliente.Id = _db.GetNextClienteId();
// _db.Clientes.Add(cliente);
// await _db.SaveChangesAsync();
// return cliente;
// }

// public async Task<Cliente> ObtenerPorIdAsync(int id)
// {
// return await Task.FromResult(_db.Clientes.FirstOrDefault(c => c.Id == id));
// }

// public async Task<List<Cliente>> ListarAsync()
// {
// return await Task.FromResult(_db.Clientes.ToList());
// }

// public async Task<Cliente> ActualizarAsync(Cliente cliente)
// {
// var existing = _db.Clientes.FirstOrDefault(c => c.Id == cliente.Id);
// if (existing == null) return null;
// existing.Nombre = cliente.Nombre;
// existing.Telefono = cliente.Telefono;
// existing.Correo = cliente.Correo;
// await _db.SaveChangesAsync();
// return existing;
// }

// public async Task<bool> EliminarAsync(int id)
// {
// var existing = _db.Clientes.FirstOrDefault(c => c.Id == id);
// if (existing == null) return false;
// _db.Clientes.Remove(existing);
// await _db.SaveChangesAsync();
// return true;
// }
// }
//}