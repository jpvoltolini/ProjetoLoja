using Microsoft.EntityFrameworkCore;
using ProjetoLoja.Mdl;

namespace ProjetoLoja.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; } = null!;
        public DbSet<Produto> Produtos { get; set; } = null!;
        public DbSet<Compra> Compras { get; set; } = null!;
        public DbSet<CompraItem> CompraItems { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>().ToTable("Clientes");
            modelBuilder.Entity<Produto>().ToTable("Produtos");
            modelBuilder.Entity<Compra>().ToTable("Compras");
            modelBuilder.Entity<CompraItem>().ToTable("CompraItems");

        }
    }
}
