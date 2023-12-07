using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkDataLayer.Model
{
    public class ParkBeheerContext : DbContext
    {
        private string _connectionString;

        public ParkBeheerContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<ParkEF> Park { get; set; }
        public DbSet<HuisEF> Huis { get; set; }
        public DbSet<HuurderEF> Huurder { get; set; }
        public DbSet<HuurContractEF> HuurContract { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ParkEF>()
                .HasMany(p => p.Huizen)
                .WithOne(h => h.Park);

            modelBuilder.Entity<HuisEF>()
                .HasMany(h => h.HuurContracten)
                .WithOne(hC => hC.Huis);

            modelBuilder.Entity<HuurderEF>()
                .HasMany(h => h.HuurContracten)
                .WithOne(hC => hC.Huurder);

            modelBuilder.Entity<HuurContractEF>()
                .HasOne(hC => hC.Huurder)
                .WithMany(h => h.HuurContracten);
        }
    }
}
