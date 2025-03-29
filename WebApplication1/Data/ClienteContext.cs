
using Microsoft.EntityFrameworkCore;


namespace CadastroClientes.Models
{
    public class ClienteContext : DbContext
    {
        public ClienteContext(DbContextOptions<ClienteContext> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("TB_CLIENTES");
            modelBuilder.Entity<Cliente>().HasKey(c => c.Id);
            modelBuilder.Entity<Cliente>().Property(c => c.Id).HasColumnName("idCliente");
            modelBuilder.Entity<Cliente>().Property(c => c.Nome).HasColumnName("nmCliente").IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.Tipo).HasColumnName("flTipo").IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.Documento).HasColumnName("nrDocumento").IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.DataCadastro).HasColumnName("dtCadastro").IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.Telefone).HasColumnName("nrTelefone").IsRequired();
            modelBuilder.Entity<Cliente>().Property(c => c.Excluido).HasColumnName("flExcluido").HasDefaultValue(false);
        }
    }
}