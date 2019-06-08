using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Servcom.SGA.Domain.Atendimentos;

namespace Servcom.SGA.Infra.Data.Mappings
{
    public class TipoAtendimentoMapping : IEntityTypeConfiguration<TipoAtendimento>
    {
        public void Configure(EntityTypeBuilder<TipoAtendimento> builder)
        {
            builder.Ignore(a => a.ValidationResult);
            builder.Ignore(a => a.CascadeMode);
            builder.ToTable("TipoAtendimentos");

        }

        
    }
}
