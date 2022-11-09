using Microsoft.EntityFrameworkCore;
using projetoCsharp.Model;

namespace projetoCsharp.Database
{
    public class BaseDbContext : DbContext
    {
        //construtor
        public BaseDbContext(DbContextOptions<BaseDbContext>
        options) : base(options) { 

        }

        //referenciando os modelos
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        //definido alterações - o formato de criação da tabela no bd
        protected override void OnModelCreating(ModelBuilder modelBuilder) { 
            var cliente = modelBuilder.Entity<Cliente>();
            cliente.ToTable("cliente");
            cliente.HasKey(x => x.Id);
            cliente.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            cliente.Property(x => x.Nome).HasColumnName("nome").IsRequired();
            cliente.Property(x => x.DataNascimento).HasColumnName("data_nascimento");
            cliente.Property(x => x.Endereco).HasColumnName("endereco").IsRequired();
            cliente.Property(x => x.Email).HasColumnName("email").IsRequired();
        
            var produto = modelBuilder.Entity<Produto>();
            produto.ToTable("Produto");
            produto.HasKey(x => x.Id);
            produto.Property(x => x.Id).HasColumnName("id").ValueGeneratedOnAdd();
            produto.Property(x => x.NomeProduto).HasColumnName("nome_produto").IsRequired();
        }
    }
}