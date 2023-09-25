using Domain.Models.Clientes;
using Domain.Models.Marcas;
using Domain.Models.Pedidos;
using Domain.Models.Produtos;
using Domain.Models.Vendedores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) 
        {
        }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-UJH1H7R; database=CrudVendasDB; Integrated Security=True; TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Vendedor>().ToTable("Vendedor");
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Marca>().ToTable("Marca");
            modelBuilder.Entity<Produto>().ToTable("Produto");
            modelBuilder.Entity<Pedido>().ToTable("Pedido");
        }

    }
}
