using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVCBasico.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MVCBasico.Context
{
    public class RecitalDatabaseContext : DbContext
    {
        public RecitalDatabaseContext(DbContextOptions<RecitalDatabaseContext>
       options) : base(options)
        {
        }
        public DbSet<Recital> Recital { get; set; }
        public DbSet<Banda> Banda { get; set; }
        public DbSet<Establecimiento> Establecimiento { get; set; }
        public DbSet<Productora> Productora { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<VentaEntradas> VentaEntradas { get; set; }
        public DbSet<Entrada> Entrada { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entrada>()
                .HasOne(e => e.Recital)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VentaEntradas>()
                .HasOne(v => v.Recital)
                .WithMany()  
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<VentaEntradas>()
                .HasOne(v => v.Usuario)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Productora>()
                .HasOne(v => v.VentaEntradas)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);
        }

    }

}

