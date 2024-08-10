
using Microsoft.EntityFrameworkCore;
using System;

namespace CadastorHeroisTeste.Models
{
    public class Contexto : DbContext
    {
        public DbSet<Heroi> Herois { get; set; }
        public Contexto(DbContextOptions<Contexto> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
