using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Servcom.SGA.Domain.Atendimentos;
using Servcom.SGA.Domain.Usuarios;
using Servcom.SGA.Infra.Data.Mappings;
using System.IO;


namespace Servcom.SGA.Infra.Data.Context
{
    public class ServcomSGAContext : DbContext
    {
        IConfigurationRoot config = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();

        public DbSet<Atendimento> Atendimentos {get;set;}
        public DbSet<TipoAtendimento> TipoAtendimentos {get;set;}
        public DbSet<Usuario> usuarios { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {


            optionsBuilder.UseMySql(config.GetConnectionString("Servcom"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration<Atendimento>(new AtendimentoMapping());
            modelBuilder.ApplyConfiguration<TipoAtendimento>(new TipoAtendimentoMapping());
            modelBuilder.ApplyConfiguration<Usuario>(new UsuarioMapping());
            modelBuilder.Entity<Atendimento>().HasAlternateKey(a =>new { a.TipoId,a.DataCriacao,a.Sequencia });
       
           
            base.OnModelCreating(modelBuilder);
        }

    }
}
