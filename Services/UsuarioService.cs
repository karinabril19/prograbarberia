using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using prograbarberia.Data;
using prograbarberia.Models;

namespace prograbarberia.Services
{
 public class UsuarioService
 {
 private readonly BarberiaDbContext _db;
 public UsuarioService(BarberiaDbContext db)
 {
 _db = db;
 }

 public async Task<Usuario> CrearAsync(Usuario usuario, string passwordPlain)
 {
 usuario.Id = _db.GetNextUsuarioId();
 usuario.PasswordHash = HashPassword(passwordPlain);
 _db.Usuarios.Add(usuario);
 await _db.SaveChangesAsync();
 return usuario;
 }

 public async Task<Usuario> ObtenerPorIdAsync(int id)
 {
 return await Task.FromResult(_db.Usuarios.FirstOrDefault(u => u.Id == id));
 }

 public async Task<List<Usuario>> ListarAsync()
 {
 return await Task.FromResult(_db.Usuarios.ToList());
 }

 public async Task<Usuario> ActualizarAsync(Usuario usuario)
 {
 var existing = _db.Usuarios.FirstOrDefault(u => u.Id == usuario.Id);
 if (existing == null) return null;
 existing.Nombre = usuario.Nombre;
 existing.Email = usuario.Email;
 existing.Rol = usuario.Rol;
 await _db.SaveChangesAsync();
 return existing;
 }

 public async Task<bool> EliminarAsync(int id)
 {
 var existing = _db.Usuarios.FirstOrDefault(u => u.Id == id);
 if (existing == null) return false;
 _db.Usuarios.Remove(existing);
 await _db.SaveChangesAsync();
 return true;
 }

 public async Task<Usuario> AutenticarAsync(string email, string passwordPlain)
 {
 var user = _db.Usuarios.FirstOrDefault(u => u.Email == email);
 if (user == null) return null;
 var hash = HashPassword(passwordPlain);
 if (user.PasswordHash == hash) return await Task.FromResult(user);
 return null;
 }

 private string HashPassword(string password)
 {
 using var sha = SHA256.Create();
 var bytes = Encoding.UTF8.GetBytes(password);
 var hash = sha.ComputeHash(bytes);
 return Convert.ToBase64String(hash);
 }
 }
}