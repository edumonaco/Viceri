
using Microsoft.EntityFrameworkCore;
using System;

namespace CadastorHeroisTeste.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Heroi> Herois { get; set; }
        public DbSet<SuperPoder> SuperPoderes { get; set; }

        public Contexto(DbContextOptions<Contexto> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Heroi>()
          .HasMany(h => h.SuperPoderes)
          .WithMany()
          .UsingEntity<Dictionary<string, object>>(
                "HeroisSuperpoderes",
                j => j.HasOne<SuperPoder>().WithMany().HasForeignKey("SuperPoderId"),
                j => j.HasOne<Heroi>().WithMany().HasForeignKey("HeroiId"));

            base.OnModelCreating(modelBuilder);

        }
    }
}
