using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Servcom.SGA.Domain.Atendimentos;

namespace Servcom.SGA.Infra.Data.Mappings
{
    public class AtendimentoMapping : IEntityTypeConfiguration<Atendimento>
    {
        public void Configure(EntityTypeBuilder<Atendimento> builder)
        {
            builder.Property(a => a.DataCriacao).HasColumnType("date");
            builder.Property(a => a.TipoId).HasColumnType("CHAR(36)");
            builder.Property(a => a.UsuarioId).HasColumnType("CHAR(36)");
            builder.Property(a => a.TimeStamp)
                .IsConcurrencyToken(true)
                .ValueGeneratedOnAddOrUpdate();
            builder.Ignore(a => a.ValidationResult);
            builder.Ignore(a => a.CascadeMode);
            
            builder.HasOne(a => a.TipoAtendimento)
                .WithMany(t => t.Atendimentos)
                .HasForeignKey(a => a.TipoId)
                .IsRequired(false);
            
            builder.ToTable("Atendimentos");

        }

        
    }
}
