using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestFacturacion.Models.Context
{
    public class FacturacionDbContext : DbContext
    {
        public FacturacionDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Producto>()
           .Property(p => p.Estado)
           .HasDefaultValue(true);

        }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<TasaCambio> TasaCambio { get; set; }
        public DbSet<Producto> Producto { get; set; }

    }
}
