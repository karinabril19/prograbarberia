using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using prograbarberia.Models;

namespace prograbarberia.Data
{
    public class BarberiaDbContext : DbContext
    {
        public BarberiaDbContext(DbContextOptions<BarberiaDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Empleado> Empleados { get; set; } = null!;
        public DbSet<Servicio> Servicios { get; set; } = null!;
        public DbSet<Cita> Citas { get; set; } = null!;
        public DbSet<Usuario> Usuarios { get; set; } = null!;
    }
}
